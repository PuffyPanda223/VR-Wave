using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// get the current logged in user from the oculus go and display it
public class getAccount : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerName.playerName == "")
        {
            // 5 is the create user name scene
            SceneManager.LoadScene(5);
        }   else
        {
            transform.GetComponentInChildren<TextMeshPro>().text = "Hello: "+ PlayerName.playerName;
        }
    }
}
