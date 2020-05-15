using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.Video;


// pointer script attached to the vr pointer in the create user screen.
public class keyPress : MonoBehaviour
{
    int m_Distance = 270;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask;
    public LayerMask m_InteractableMask;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    public GameObject DND_Pointer;
    public GameObject DND_Reticule;

    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;



    private Vector3 isNull;
    private void Awake()
    {
        VRController.OnControllerSource += UpdateOrigin;
        isNull = new Vector3(0, 0, 0);



    }

    private void Start()
    {
        Time.timeScale = 1;
        SetLineColor();
    }

    private void Update()
    {
        Vector3 hitPoint = new Vector3(0, 0, 0);
        if (m_CurrentOrigin != null)
        {
            hitPoint = UpdateLine();
        }

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
        {
            // is null is a vector3 of 0,0,0 basically meaning null, but unity doesn't support null types on non booleans
            if (hitPoint != isNull)
            {
                OnPointerUpdate(hitPoint, m_CurrentObject);
            }

        }


   
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            //bool isHitBox = false; 
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);

            RaycastHit hit;
            // do a raycast and see if it hits a key object
            if (Physics.Raycast(ray, out hit, m_Distance, m_InteractableMask))
            {
                hit.transform.GetComponent<keyFeedBack>().keyHit = true;
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

}
