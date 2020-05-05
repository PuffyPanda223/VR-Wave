using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


// simply check to see if a highscores save file exists, if so attach the interactable script to it and make the text white indicating to the player you can interact with it
public class checkScores : MonoBehaviour
{
    private string path;
    public GameObject highScoreButton; 
    void Start()
    {
        path = Application.persistentDataPath + "/highscores/highscore.txt";
        if (File.Exists(path))
        {
            // by default the interactable script is not attached to the play button, it is through the interactable script that we decide what we want to do in the game world
            highScoreButton.AddComponent<Interactable>();
            transform.GetComponent<TextMeshPro>().color = Color.white;
        }
    }


}
