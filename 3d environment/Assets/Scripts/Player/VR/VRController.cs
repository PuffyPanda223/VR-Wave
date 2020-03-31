using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRController : MonoBehaviour
{

    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion

    #region
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;

    #endregion

    #region VRInput
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }


    // Update is called once per frame
    void Update()
    {
        //Check for active input
        if (!m_InputActive)
            return;

        //Check if a controller exists
        CheckForController();

        //Check for input source
        CheckInputSource();

        //Check for actual input
        VRInput();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        //Right Remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        //Left Remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        //If no controller, use headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) &&
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;
        //Update
        m_Controller = UpdateSource(controllerCheck, m_Controller);

    }

    private void CheckInputSource()
    {
        //Update
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    private void VRInput()
    {
        //Touchpad Down
        if(OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadDown != null)
            {
                OnTouchpadDown();
            }
        }

        //Touchpad Up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadUp != null)
            {
                OnTouchpadUp();
            }
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        //If values are the same, return
        if (check == previous)
            return previous;

        //Get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        //If no controller object, set to headset
        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        //Send out event
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        //Return new value
        return check;
    }

    private void PlayerFound()
    {
        m_InputActive = true;
    }

    private void PlayerLost()
    {
        m_InputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote, m_LeftAnchor},
            {OVRInput.Controller.RTrackedRemote, m_RightAnchor},
            {OVRInput.Controller.Touchpad, m_HeadAnchor}
        };

        return newSets;
    }
}


