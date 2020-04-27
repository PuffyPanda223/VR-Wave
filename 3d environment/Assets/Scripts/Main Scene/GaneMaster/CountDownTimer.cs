using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this class is 
public class CountDownTimer : MonoBehaviour
{
    // Static makes it so the variable becomes a member of the class and not an instance of the class, meaning we can access the variable from the class
    public static  float timer = 0f;
    public float timeLimit = 30f;



    // Update is called once per frame
    void Update()
    {
        //if game is not paused increment the countdown. If it is paused don't increment the countdown 
        if (!DrawLine.isGamePaused)
        {
            // increment the timer based on the time between frames and not on each frame. This means the timer works the exact same independent on the hardware of the machine it is used on
            timer += 1 * Time.deltaTime;
        }

        //If the timer exceeds the time limit, the user is returned to the main menu. (This will soon be replaced by a score screen.
        if (timer > timeLimit)
        {
            timer = 0;
            PointSystem.playerScore = 0;
            SceneManager.LoadScene(0);
        }
            
    }







}
