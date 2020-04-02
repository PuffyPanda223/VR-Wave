using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{

    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask  ;
    public LayerMask m_InteractableMask  ;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    public GameObject DND_Pointer;


    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    private void Awake()
    {
        VRController.OnControllerSource += UpdateOrigin;
        VRController.OnTouchpadDown += ProcessTouchpadDown;

        DontDestroyOnLoad(DND_Pointer);

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
    }

    private Vector3 UpdateLine()
    {
        // Create ray
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
            // return a game object to the variable m_ CurrentObject
            return hit.collider.gameObject;

        //Return
        return null;

    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
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
        // m_CurrentObject is an object that is gotten from a raycast hit
        if (!m_CurrentObject)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        
        // send the object to another function in the interactable script that will process what was pressed and what to do when it is pressed
        interactable.Pressed(m_CurrentObject);

    }
}
