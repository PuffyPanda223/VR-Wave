using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class Pointer : MonoBehaviour
{

    int m_Distance = 270;
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

    // values for the waves 

    private int safeWave = 5;
    private int mediumWave = 4;
    private int hardWave = 3;

    private void Awake()
    {
        VRController.OnControllerSource += UpdateOrigin;
        //VRController.OnTouchpadDown += ProcessTouchpadDown;
        
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
            sphere = GameObject.Find("Sphere");
            // get the video player component containing the 3d footage we are using. 
            videoPlayer = sphere.GetComponent<VideoPlayer>();
            videoPlayer.Play();
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
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

    private Vector3 UpdateLine()
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


    private void ProcessTouchpadDown()
    {
       if (m_CurrentObject == null )
        {
            return;
        }
        Debug.Log(m_CurrentObject);
        Interactable interact = m_CurrentObject.GetComponent<Interactable>();
        interact.Pressed(m_CurrentObject.transform.gameObject);
    }
}
