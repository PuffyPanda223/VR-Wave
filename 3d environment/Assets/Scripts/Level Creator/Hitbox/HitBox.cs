﻿using System.Collections;
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

    
    private void Awake()
    {
        
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
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        
        
    }






}
