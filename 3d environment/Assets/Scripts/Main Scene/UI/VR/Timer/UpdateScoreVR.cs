using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UpdateScoreVR : MonoBehaviour
{


    public TextMeshPro score;


    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + PointSystem.playerScore.ToString(); 
    }
}
