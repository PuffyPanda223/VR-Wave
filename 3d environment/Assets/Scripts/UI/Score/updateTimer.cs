using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateTimer : MonoBehaviour
{
    // Start is called before the first frame update

    public Text timerUpdate; 
    void Start()
    {
        timerUpdate.text = 0.0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // get  the timer from our game master script CountDownTimer and constatly update the text on the screen
        float timer = CountDownTimer.timer;
        // the ToString F0 makes it so their is no decimal places. I have to make the timer it self a float because the user of delta time has to be used on a float so when making this 
        // a string just cut off any decimal places
        timerUpdate.text = timer.ToString("F0"); 
        
    }
}
