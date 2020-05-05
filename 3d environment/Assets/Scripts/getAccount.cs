using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using TMPro;
// get the current logged in user from the oculus go and display it
public class getAccount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Users.GetLoggedInUser().OnComplete(GetLoggedInUser);
    }

    void GetLoggedInUser(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error is " + msg.GetError());
        } else
        {
            if(msg.Type == Message.MessageType.User_GetLoggedInUser)
            {
                transform.GetComponent<TextMeshPro>().text = "Hello " + msg.GetUser().OculusID;
            }
        }
    }

}
