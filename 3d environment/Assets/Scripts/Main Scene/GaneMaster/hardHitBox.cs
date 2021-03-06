﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardHitBox : MonoBehaviour
{

    // this variable will reference the static float variable in the CountDownTimer script. When we get to a certain time we will spawn in a hitbox
    private float time;
    // Start is called before the first frame update

    // the prefab hitbox we will spawn in. there is three per spawn location. That way we can indivdually customize each spawn locations hit box
   

    private bool activated;
    // all the object flags used to spawn in the hit boxes
    public GameObject spawnLocation_3;
    public GameObject spawn2HitBox;
    // stores an instance of the hitbox object, if you destroy the actual gameobject it destroys the prefab as well for some stupid reason
    private GameObject _instance;

    void Start()
    {
        // 
        activated = false;

    }

    // Update is called once per frame
    void Update()
    {
        time = CountDownTimer.timer;

        if (time >= 4 && activated == false && time < 8)
        {
            Vector3 position = spawnLocation_3.transform.position;

            // the rotation 
            Quaternion rotation = Quaternion.LookRotation(spawnLocation_3.transform.up, -spawnLocation_3.transform.forward);
            _instance = Instantiate(spawn2HitBox, position, rotation);

            activated = true;
            // the time is individualised per script
        }
        else if (time > 9)
        {
            // its possible that the hitbox is already been destroyed because of the player so we check to see if the instance is still their or not to avoid errors
            if (_instance)
            {
                Destroy(_instance);

            }
            // Becuase the hit box is either already destroyed or the player ran out of time their is no need to keep the script running and wasting resources, so we disable it
            GetComponent<hardHitBox>().enabled = false;
            Debug.Log("Script should not print this more than once");

        }

    }
}
