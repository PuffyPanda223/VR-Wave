using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Tracks how much time has elpased since the start of the game. Is attached to the game master object. The timer updates every frame with deltatime and the value of which can be accessed by any script in the level editor scene
public class GlobalTimer : MonoBehaviour
{
    public static float timer = 0f;

    // when the menu is initiated, this variable will control whether we pause the game or not
    public static bool paused = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timer += 1 * Time.deltaTime;
        }
        
    }

    // static variables do not reset on scene loading so we need to manually reset the timer. This is a delegate method that is triggered whenever the scene loaded callback is triggered
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
   
        if (scene.name == "Save")
        {
           
            SaveData.clearList();
            Debug.Log("container has been emptied");
            timer = 0f; 
        }
    }
}
