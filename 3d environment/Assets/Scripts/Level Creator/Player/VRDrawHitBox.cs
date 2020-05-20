using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
// this script is attahced to the VR pointer in the level creator scene, it records two positions one at the start of the touchpad and one at the release of the touchpad and draws a hitbox in the game world with those dimensions
public class VRDrawHitBox : MonoBehaviour
{

    float m_Distance = 250.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask;
    public LayerMask m_InteractableMask;
    // this action is used by a script on our reticle gameobject to correctly update where in the game world it should be
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    public GameObject DND_Pointer;
    //currentOrigin is the updated position of the pointer in the game, we uses it position to correctly draw and renderer the rest of the vr associated gear like the line renderer and the reticle
    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    // our two positions in the world we need to construct a hitbox
    private Vector3 startPos;
    private Vector3 endPos;

    // Shadow box is a temporory plane object whose dimensions will continiously change as the user draws a hitbox. It will show the user how big the hitbox is and where in the game world the hitbox will be drawn
    private GameObject shadowBox;
    private MeshFilter shadowFilter;
    private MeshRenderer shadowRenderer;
    private Mesh shadowMesh;


    // materials used to correctly display what difficulty of hitbox the user is drawing. Which material is used is determined by a global static string variable with the up to date difficulty 
    public Material safe;
    public Material medium;
    public Material hard;

    public GameObject floatingText;


    public Canvas UICanvas;
    public GameObject videoSphere;
    public LayerMask buttonMask;

    private bool lockFrame = false;
    private void Awake()
    {
        // subscribe the update origin script to the on controller source delegate method, this method is called when the pointer 
         VRController.OnControllerSource += UpdateOrigin;
    }

    private void OnDestroy()
    {
        VRController.OnControllerSource -= UpdateOrigin;
       

    }


    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
        {
            OnPointerUpdate(hitPoint, m_CurrentObject);
        }


        if (!GameState.gameState)
        {
            if (!lockFrame)
            {
                startOfDraw();
                continueToDraw();
                release();
                checkChange();
                checkPause();
            } else
            {
                if(OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
                {
                    lockFrame = false;
                }
            }
        } else
        { 
            checkRayCast();
            //checkUnPause();
        }

    }


    /// <summary>
    /// sends the UI canvas way away
    /// </summary>
    private void disableBoxCollider()
    {
        UICanvas.transform.position = UICanvas.transform.position * 1000;
    }

    private void checkRayCast()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
          
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                // the UI layer is where the buttons in the UI canvas live
                if (hit.transform.tag == "UI")
                {
                   

                    string name = hit.transform.gameObject.name;
                    Debug.Log(name);
                    switch (name)
                    {
                        case "Quit":
                            SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.MAIN_MENU);
                            break;
                        case "Restart":
                            SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.LEVEL_CREATOR);
                            break;
                        case "Resume":
                            GameState.changeGameState();
                            videoSphere.GetComponent<VideoPlayer>().Play();
                            UICanvas.enabled = false;
                            UICanvas.GetComponent<OverlayMenu>().enabled = false;
                            disableBoxCollider();
                            lockFrame = true;
                            break;
                    }
                }
            }
        }
    }

    private void checkUnPause()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            try
            {

                // disable the renderer for the canvas and the follow script so it doesnt eat up processing power when it isn't activate
                UICanvas.enabled = false;
                UICanvas.GetComponent<OverlayMenu>().enabled = false;
                GameState.changeGameState();
                videoSphere.GetComponent<VideoPlayer>().Play();
                disableBoxCollider(); 
            } catch(Exception e )
            {
                Debug.LogError(e);
            }
        }
    }

    private void checkPause()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            // not really necessary at the moment because we always have the one video but later on when implementing online connectivity it will be important to make sure the game doesn't break if something goes wrong with 
            // the video player
            try
            {
                GameState.changeGameState();
                UICanvas.enabled = true;
                UICanvas.GetComponent<OverlayMenu>().enabled = true;
                videoSphere.GetComponent<VideoPlayer>().Pause();
            } catch (Exception e )
            {
                Debug.LogError(e);
            }
        }
    }

    // check to see if the player intention is draw on the current frame, if so setup all the necessary components for drawing the hitbox
    private void startOfDraw()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            // clear the startPos, as we are creating a new hitbox

            startPos = Vector3.zero;
            endPos = Vector3.zero;
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);

            RaycastHit hit = new RaycastHit();

            // Create the shadowbox object which will show the user how the box is being drawn. It will continiously be updated while the user is holding down the fire button
            // this object will be destroyed when the user releases the fire button and when the user relicks down the primary button be created again
            shadowBox = new GameObject("plane");
            shadowFilter = shadowBox.AddComponent(typeof(MeshFilter)) as MeshFilter;
            shadowRenderer = shadowBox.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            shadowMesh = new Mesh();
            // the wave difficulty will not change once the new shadow box has been made so set the render material here
            switch (ChangeWave.difficulty)
            {
                case "safe":
                    shadowRenderer.material = safe;
                    break;
                case "medium":
                    shadowRenderer.material = medium;
                    break;
                case "hard":
                    shadowRenderer.material = hard;
                    break;
                default:
                    shadowRenderer.material = safe;
                    break;
            }


            // get the pos of the start point and log them into the startPos variable. This will allow us calculate the distance we need to render the hitbox
            if (Physics.Raycast(ray, out hit))
            { 
                startPos = hit.point;
            }
        }
    }


    // check to see if 
    private void continueToDraw()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {

            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
            RaycastHit hit;

            // get the pos of the start point and log them into the startPos variable. This will allow us calculate the distance we need to render the hitbox
            if (Physics.Raycast(ray, out hit))
            {
                // Make the start position = inital set of Vector3 coords that the user clicks on
                endPos = hit.point;
                //Debug.Log("end position is " + endPos); 
                // show the user how the hitbox is being drawn in real time 
                DrawShadowBox();
            }
        }
    }

    private void release()
    {
        // if the button is released, generate the plane and destroy the shadow box
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            // after the user has released the key create the hitbox
            generatePlane();
            destroyShadow();

        }

    }

    // checks to see if the back button was pressed and if so change the color of the wave
    private void checkChange()
    {
        // change wave will go to the next wave in the selection pool safe -> medium -> hard - > safe
        if (OVRInput.Get(OVRInput.Button.Back) )
        {
            ChangeWave.Change();

        }
    }



    private Vector3 UpdateLine()
    {
        RaycastHit hit;
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);
        // if the raycast doesnt happen return the default endPosition
        try
        {
            hit = CreateRaycast(m_EverythingMask);

        }
        catch 
        {
    
            m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
            m_LineRenderer.SetPosition(1, endPosition);
            return endPosition;
        }



        //Check hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }
        //Set Position

        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }


    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        if (m_LineRenderer != null)
        {
            //Set origin of pointer
            m_CurrentOrigin = controllerObject.transform;

            //Is the laser visible?
            if (controller == OVRInput.Controller.Touchpad)
            {
                m_LineRenderer.enabled = false;
            }
            else
            {
                m_LineRenderer.enabled = true;
            }

            //
        }
    }


    private GameObject UpdatePointerStatus()
    {

        //Create Ray
        RaycastHit hit;
        try
        {
            hit = CreateRaycast(m_InteractableMask);
        }
        catch 
        {
            return null;

        }
        //Check Hit
        if (hit.collider)
        {

            // return a game object to the variable m_ CurrentObject
            return hit.collider.gameObject;

        }

        return null;



    }


    // different raycasts are cast for different layers so we made a function that takes a particular layer/layers and returns the hit if any 
    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
    
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
 
    }


    private void destroyShadow()
    {
        Destroy(shadowBox);
    }
    // creates a plane object given points from the user and adds the hitbox to a global list for saving
    public void generatePlane()
    {
        Mesh m = new Mesh();


        m = CreateHitBox.calculateMesh(startPos, endPos, m);

        // the material is how we both visually and programmatically differentiate between the level of difficult the wave is


        // now that all the details of the object have been finialised we populate this custom data class with the necessary information so that the mesh renderer and filter can generate the hitbox when loading from a save file
        HitboxData actor = new HitboxData();

        // generate mesh details takes a mesh object and deconstructs it into a primative list of float arrays that can be reassembled on load. The reason we need to deconstruct the mesh is because unity specific classes are not serialiazble
        actor.GetMeshDetails(m);
        actor.startTime = GlobalTimer.timer;
        actor.endTime = GlobalTimer.timer + 2f;


        // we only need to store the name of the material rather than the material itself. When loading in the loader has the necessary materials and checks against the name to determine which material to attach to the hitbox
        switch (ChangeWave.difficulty)
        {
            case "safe":
               actor.difficulty = "safe";
                break;
            case "medium":
                actor.difficulty = "medi";
                break;
            case "hard":
                actor.difficulty = "hard";
                break;
            default:
                Debug.Log("unable to determine the wave difficulty");
                actor.difficulty = "safe";
                break;
        }

        //we have a static global list that is the same type as the above custom class. that makes sure there is one certialized location where all the list data of our hitboxes are being kept. this makes saving and loading a lot easier
        SaveData.AddToList(actor);

        //display a prompt informing the user they have successfully created a hitbox
        if(startPos.y > endPos.y)
        {
            var display = Instantiate(floatingText, startPos, Quaternion.LookRotation(startPos));
            display.GetComponent<TextMesh>().text = "Hitbox created!!!!!";
        } else
        {
            var display = Instantiate(floatingText, endPos, Quaternion.LookRotation(endPos));
            display.GetComponent<TextMesh>().text = "Hitbox created!!!!!";
        }
       
    }

    // for each frame the user is holding down the draw button this method is being called, continiously updating and resizing the temporary hitbox
    private void DrawShadowBox()
    {

        shadowMesh = CreateHitBox.calculateMesh(startPos, endPos, shadowMesh);
        shadowFilter.mesh = shadowMesh;
  
   
    }
}
