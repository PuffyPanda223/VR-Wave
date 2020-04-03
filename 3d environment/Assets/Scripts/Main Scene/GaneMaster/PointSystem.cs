using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static float playerScore = 0;

    public static void addScore(int score)
    {
        playerScore += score;
        //Debug.Log(playerScore);
    }

    
}
