using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class is 
public class CountDownTimer : MonoBehaviour
{
    // Static makes it so the variable becomes a member of the class and not an instance of the class, meaning we can access the variable from the class
    static public float timer = 0;
    public GameObject score; 



    // Update is called once per frame
    void Update()
    {
        //if game is not paused increment the countdown. If it is paused don't increment the countdown 
        if (!DrawLine.isGamePaused)
        {
            // increment the timer based on the time between frames and not on each frame. This means the timer works the exact same independent on the hardware of the machine it is used on
            timer += 1 * Time.deltaTime;
        }
            
    }







}
