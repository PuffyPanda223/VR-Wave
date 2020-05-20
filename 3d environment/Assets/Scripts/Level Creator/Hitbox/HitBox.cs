using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attaches to a drawn hitbox at run time. Script handles spawning itself in given whatevber time the global timer is at and destroying itself after a set amount of time 
public class HitBox : MonoBehaviour
{
    // both the start and end time are editied by drawing script when it this script is attached to the hitbox
    public float startTime;
    public float endTime;
    private float lengthOfTime;
    // use this to determine when to activate the collider and renderer and when to deactive them after a certain amount of time elapses
    private bool activated = false;

    private Material safe;
    private Material medium;
    private Material hard;

    // time alive is the max amount of time the hitbox is spawned for
    private int timeAlive = 6; 
    private float timeElpased;
    private void Awake()
    {
 
        medium = Resources.Load("medium.mat", typeof(Material)) as Material;
        hard = Resources.Load("Material/hard.mat", typeof(Material)) as Material;
        Debug.Log(hard);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        
    }

    // determine when to enable the hitbox based on the start time that was provided by the DrawHitBox script
    private void Update()
    {
        // when the start time has been reached activate the mesh renderer of the hitbox, allowing it to be targetted by the player
        if(CountDownTimer.timer >= startTime && activated == false)
        {


            gameObject.GetComponent<MeshRenderer>().enabled = true; 

            activated = true;
        }
        // every hitbox is only up for a set amount of time. So when the time expires disable the renderer so the user cannot interact with it at all. 
        // We can't just destroy it because if we are creating a level we need the object to still be in the heirachy 
        
        if(CountDownTimer.timer >= endTime)
        {
           
            Destroy(this.transform.gameObject);
        }

        // get the current time passed
        if (activated == true && endTime > CountDownTimer.timer)
        {
            timeElpased = endTime - CountDownTimer.timer;
            //Debug.Log( (timeElpased/timeAlive) * 100);
        }




        if (this.GetComponent<MeshRenderer>().material.name.Substring(0, 4) == "medi" && activated == true)
        {
            if ( (timeElpased / timeAlive) * 100 < 33.333)
            {
                this.GetComponent<MeshRenderer>().material.name = "hard";
            }
        }

        // change from green to orange 
        if (this.GetComponent<MeshRenderer>().material.name.Substring(0,4) == "safe" && activated == true)
        {
            if ( (timeElpased/timeAlive) * 100 <= 50 )
            {
               
                this.GetComponent<MeshRenderer>().material.name = "medium"; 
            }
        }

        



    }






}
