using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class Pointer : MonoBehaviour
{

    public int m_Distance = 270;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask  ;
    public LayerMask m_InteractableMask  ;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    public GameObject DND_Pointer;
    public GameObject DND_Reticule;
    public GameObject Dot;

    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    public  GameObject sphere;
    private VideoPlayer videoPlayer;

    public Camera camera; 
    private FloatingText FloatingText;

    // values for the waves 

    private int safeWave = 5;
    private int mediumWave = 4;
    private int hardWave = 3;

    private void Awake()
    {
        VRController.OnControllerSource += UpdateOrigin;
        //VRController.OnTouchpadDown += ProcessTouchpadDown;

        GameObject gameMaster = GameObject.Find("Game Master");
        FloatingText = gameMaster.GetComponent<FloatingText>();
        /*
        DontDestroyOnLoad(DND_Pointer);
        DontDestroyOnLoad(DND_Reticule);
        */


    }

    private void Start()
    {
        Time.timeScale = 1;
        SetLineColor();
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
        {
            OnPointerUpdate(hitPoint, m_CurrentObject);
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && Interactable.drawActive == true)
        {
            Instantiate(Dot, hitPoint, m_CurrentOrigin.rotation);
        }

        if (OVRInput.Get(OVRInput.Button.Back) && Interactable.drawActive == true)
        {
            GameObject[] Dots = GameObject.FindGameObjectsWithTag("Draw");

            for (var i = 0; i < Dots.Length; i++)
            {
                Destroy(Dots[i]);
            }

            Interactable.drawActive = false;
            DrawLine.isGamePaused = false;
            // get the video player component containing the 3d footage we are using. 
            videoPlayer = sphere.GetComponent<VideoPlayer>();
            videoPlayer.Play();
        }

        // Checks if item is hitbox and then if not checks to see if it is another object
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            bool isHitBox = false;
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
           // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_InteractableMask))
            {
                isHitBox = true; 
                string waveDifficulty = hit.transform.GetComponent<MeshRenderer>().material.name.Substring(0, 4);

                // check to see which level of difficulty of wave the player hit
                switch (waveDifficulty)
                {
                    case "safe":
                        PointSystem.addScore(5);
                        FloatingText.showFloatingText(safeWave, hit.transform.gameObject);
                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case "mediu":
                        PointSystem.addScore(mediumWave);
                        FloatingText.showFloatingText(mediumWave, hit.transform.gameObject);
                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case "hard":
                        FloatingText.showFloatingText(hardWave, hit.transform.gameObject);
                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        PointSystem.addScore(hardWave);
                        break;
                    default :
                        Debug.Log("hit a hitbox but couldn't tell which one it was ");
                        break;

                }
                Destroy(hit.transform.gameObject);

            }
            if (isHitBox == false)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_EverythingMask))
                {
                    Interactable interact = hit.transform.GetComponent<Interactable>();
                    interact.Pressed(hit.transform.gameObject);
                }
            }
        }



    }

    private Vector3 UpdateLine()
    {
        // Create ray
        if (m_CurrentOrigin != null)
        {
            RaycastHit hit = CreateRaycast(m_EverythingMask);

            //Default end
            Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

            //Check hit
            if (hit.collider != null)
                endPosition = hit.point;

            //Set Position
            m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
            m_LineRenderer.SetPosition(1, endPosition);

            return endPosition;
        }

        return Vector3.zero;
    }

    private void OnDestroy()
    {
        VRController.OnControllerSource -= UpdateOrigin;
        //VRController.OnTouchpadDown -= ProcessTouchpadDown;

    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //Set origin of pointer

        m_CurrentOrigin = controllerObject.transform;
        Debug.Log(m_CurrentOrigin.position + " is the current position of the cursor" );
        //Is the laser visible?
        if(controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }

        //
    }

    private GameObject UpdatePointerStatus()
    {
        //Create Ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        //Check Hit
        if (hit.collider)
        {
           
            // return a game object to the variable m_ CurrentObject
            return hit.collider.gameObject;
            
        }
           
        return null;     
   
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
       // int hit_layer = LayerMask.GetMask("hitBox");
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit; 
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;
    }

    private RaycastHit getCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        // int hit_layer = LayerMask.GetMask("hitBox");
        Physics.Raycast(ray, out hit);

        return hit;
    }

    private void ProcessTouchpadDown()
    {
        // m_CurrentObject is an object that is gotten from a raycast hit
        // RaycastHit position = CreateRaycast(m_EverythingMask);
        RaycastHit position = CreateRaycast(m_EverythingMask);

        if (position.transform != null)
        {
            Debug.Log("Position is not clear" );
            return;
        }

        Debug.Log("there was a hitbox at when you clicekd ");
        // each object has an interactable script attached to it, when the raycast hit gets a game object we get the script and activate the pressed function
        //Interactable interactable = m_CurrentObject.GetComponent<Interactable>();

        if (position.transform.GetComponent<Interactable>() )
        {
            Interactable interactable = position.transform.GetComponent<Interactable>();
            interactable.Pressed(position.transform.gameObject);
            return;
        }

        // send the object to another function in the interactable script that will process what was pressed and what to do when it is pressed
        //interactable.Pressed(m_CurrentObject);
        // The color tells us which level of difficult of wave was selected
        if (position.transform.GetComponent<MeshRenderer>())
        {
            string waveDifficulty = position.transform.GetComponent<MeshRenderer>().material.name.Substring(0, 4);

            // check to see which level of difficulty of wave the player hit
            switch (waveDifficulty)
            {
                case "safe":
                    PointSystem.addScore(5);
                    FloatingText.showFloatingText(safeWave, position.transform.gameObject);
                    position.transform.GetComponent<MeshRenderer>().enabled = false;
                    break;
                case "mediu":
                    PointSystem.addScore(mediumWave);
                    FloatingText.showFloatingText(mediumWave, position.transform.gameObject);
                    position.transform.GetComponent<MeshRenderer>().enabled = false;
                    break;
                case "hard":
                    FloatingText.showFloatingText(hardWave, position.transform.gameObject);
                    position.transform.GetComponent<MeshRenderer>().enabled = false;
                    PointSystem.addScore(hardWave);
                    break;

            }

        }

    }
}
