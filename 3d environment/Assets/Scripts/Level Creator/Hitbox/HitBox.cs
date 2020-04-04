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

    private void Start()
    {

        Debug.Log("start Time is " + startTime);
        Debug.Log("end Time is " + endTime);
        Debug.Log("timer is " + GlobalTimer.timer);
        Debug.Log(activated);
        // At beginning of game initlaize all the hitboxes to be spawned. Disable them until the correct start time elpases
   
        //Destroy(gameObject, startTime +1f); 
    }

    private void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    // determine when to enable the hitbox based on the start time that was provided by the DrawHitBox script
    private void Update()
    {
        // when the start time has been reached activate the mesh renderer of the hitbox, allowing it to be targetted by the player
        if(GlobalTimer.timer >= startTime && activated == false)
        {
            Debug.Log("should have enabled");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
          
            activated = true;
        }

        if(GlobalTimer.timer >= endTime)
        {
            Destroy(gameObject);
        }
        
    }






}
