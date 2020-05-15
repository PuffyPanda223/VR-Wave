using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
// get the current logged in user from the oculus go and display it
public class getAccount : MonoBehaviour
{

    public soundHandler soundHandler; 
    // Start is called before the first frame update
    void Awake()
    {
        // no username has been created so send the player to username creation scene
        if (PlayerName.playerName == "")
        {
            // 5 is the create user name scene
            SceneManager.LoadScene(5);
        }   else
        {
            // find the sound handler in the scene and play the clip attached to it
            try
            {

                soundHandler.PlayClick();
            } catch
            {
                Debug.Log("sound effect unable to be played");
            }
           
            transform.GetComponentInChildren<TextMeshPro>().text = "Hello: "+ PlayerName.playerName;
        }
    }
}
