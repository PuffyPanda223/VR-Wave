using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSpawner : MonoBehaviour
{   
    
    // this variable will reference the static float variable in the CountDownTimer script. When we get to a certain time we will spawn in a hitbox
    private float time; 
    // Start is called before the first frame update

        // the prefab hitbox mesh we will spawn in 
    public GameObject hitBox;
    // the player camera
    public Camera cam; 
    private bool activated;
    // all the object flags used to spawn in the hit boxes
    public GameObject spawnLocation_1;
    public GameObject spawnLocation_2;
    public GameObject spawnLocation_3;
    public GameObject spawnLocation_4;
    public GameObject spawn2HitBox; 
    // stores an instance of the hitbox object, if you destroy the actual gameobject it destroys the prefab as well for some stupid reason
    private GameObject _instance; 
    void Start()
    {

        activated = false;

    }

    // Update is called once per frame
    void Update()
    {
        time = CountDownTimer.timer;
     
        if (time >= 8 && activated == false && time < 12)
        {
            Vector3 position = spawnLocation_3.transform.position;
            
            // the rotation 
            Quaternion rotation = Quaternion.LookRotation(-cam.transform.up, -cam.transform.forward);
            _instance = Instantiate(spawn2HitBox, position, rotation);

            activated = true; 
        } else if (time>20)
        {
            if (_instance)
            {
               Destroy(_instance);
            }
          
        }

    }
}
