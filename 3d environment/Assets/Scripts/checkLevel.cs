using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class checkLevel : MonoBehaviour
{
   
    public GameObject playButton;
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(SaveData.path))
        {
            // by default the interactable script is not attached to the play button, it is through the interactable script that we decide what we want to do in the game world
            playButton.AddComponent<Interactable>();
            transform.GetComponent<TextMeshPro>().color = Color.white;
        }
    }


}
