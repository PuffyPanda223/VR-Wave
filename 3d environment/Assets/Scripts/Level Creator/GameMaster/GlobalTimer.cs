using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks how much time has elpased since the start of the game. Is attached to the game master object. The timer updates every frame with deltatime and the value of which can be accessed by any script in the level editor scene
public class GlobalTimer : MonoBehaviour
{
    public static float timer = 0f; 

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
    }
}
