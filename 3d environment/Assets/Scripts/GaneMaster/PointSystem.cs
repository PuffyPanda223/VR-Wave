using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static float playerScore;


    private float timer;
    public void addScore(int score)
    {
        playerScore += score;
        Debug.Log(playerScore);
    }

    private void Update()
    {
        timer = CountDownTimer.timer;

        if (timer > 20 && timer < 22)
        {
            Debug.Log(playerScore);
        }
    }
}
