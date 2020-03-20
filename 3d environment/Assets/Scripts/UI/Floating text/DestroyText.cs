using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when floating text is spawned set a lifecycle for it
public class DestroyText : MonoBehaviour
{
    private float destroyTime = 2f; 
    void Start()
    {
        // destroy the instance of the object after the specified amount of time
        Destroy(transform.gameObject, destroyTime);
    }

}   
