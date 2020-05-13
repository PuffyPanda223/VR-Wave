using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using System;
public class Pointer : MonoBehaviour
{

    int m_Distance = 250;
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

    private FloatingText FloatingText;



    private Vector3 isNull;
    private void Awake()
    {
        VRController.OnControllerSource += UpdateOrigin;

        // we use this Vector3 to compare with the hitPoint variable which is used for determining the length of the line renderer
        isNull = new Vector3(0, 0, 0);

       


    }

    private void Start()
    {
        Time.timeScale = 1;
        SetLineColor();
    }

    private void Update()
    {
        Vector3 hitPoint = new Vector3(0,0,0); 
        if (m_CurrentOrigin != null)
        {
             hitPoint = UpdateLine();
        }

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
        {
            // is null is a vector3 of 0,0,0 basically meaning null, but unity doesn't support null types on vector3 objects
            if (hitPoint != isNull)
            {
                OnPointerUpdate(hitPoint, m_CurrentObject);
            }

        }

        drawDots(hitPoint);
        destroyDots();
        takeScreenshot();
        findObject(); 
        




    }
    private void drawDots(Vector3 hitPoint)
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && Interactable.drawActive)
        {
            if (hitPoint != isNull)
            {
                Instantiate(Dot, hitPoint, m_CurrentOrigin.rotation);
            }
        }
    }

    /// <summary>
    /// Destroy any dots drawn by the player whilst the game was paused
    /// </summary>
    private void destroyDots()
    {
        if (OVRInput.Get(OVRInput.Button.Back) && Interactable.drawActive)
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
    }

    /// <summary>
    /// Project a raycast from the pointer object in the game and if a valid object is found use the interactable script attached to it
    /// </summary>
    private void findObject()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && !Interactable.drawActive)
        {
            //bool isHitBox = false; 
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, m_Distance, m_InteractableMask))
            {
                Interactable interact = hit.transform.GetComponent<Interactable>();
                interact.Pressed(hit.transform.gameObject);

            }



        }
    }

    /// <summary>
    /// if the game is paused and the touchpad is clicked take a screenshot of the current game and save it to the screenshots folder
    /// </summary>
     private void takeScreenshot()
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && Interactable.drawActive)
        {
            //disable the line renderer and reticle so it doesn't appear in the screenshot
            m_LineRenderer.enabled = false;
            
            screenShot.TakeScreenShot(Screen.width, Screen.height);
            Debug.Log("screenshot should have been taken");
            m_LineRenderer.enabled = true;

        }

    }


    private Vector3 UpdateLine()
    {
        RaycastHit hit;
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);
        endPosition.x = endPosition.x * 0.9f;
        endPosition.y = endPosition.x * 0.9f;
        endPosition.z = endPosition.x * 0.9f;
        // if the raycast doesnt happen return the defaul endPosition
        try
        {
           hit = CreateRaycast(m_EverythingMask);

        } catch (Exception e )
        {
            Debug.LogError(e);
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

    private void OnDestroy()
    {
        VRController.OnControllerSource -= UpdateOrigin;
        //VRController.OnTouchpadDown -= ProcessTouchpadDown;

    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //Set origin of pointer

        m_CurrentOrigin = controllerObject.transform;
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
        RaycastHit hit;
        try
        {
            hit = CreateRaycast(m_InteractableMask);
        } catch 
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

}
