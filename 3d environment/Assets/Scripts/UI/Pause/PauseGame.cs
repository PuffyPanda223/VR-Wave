
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


// Gets the is paused bool from the player (DreawLine.cs) and determines which state the game is. When the pause button is clicked reverse the state and either play or pause the video texture
public class PauseGame : MonoBehaviour
{
    // will hold all the components of the sphere object (which is what has the 3d video) 
    private GameObject sphere;
    // Will store the video player component of the sphere in this variable. The videoplayer data type contains functions to pause and play the video footage and video audio
    private VideoPlayer videoPlayer ;  

    private void Start()
    {
        // get the entire sphere object which once we have we can use to find the video player component
        sphere = GameObject.Find("Sphere");
        // get the video player component containing the 3d footage we are using. 
        videoPlayer = sphere.GetComponent<VideoPlayer>();
    }

    public void pauseGame() 
    {
        Debug.Log("Pause Button pressed");
         
        if (!DrawLine.isGamePaused)
        {
            // set the bool value to the opposite of what it is currently. If it true than it will become false and vice vera
            DrawLine.isGamePaused = !DrawLine.isGamePaused;
            videoPlayer.Pause(); 

        } else
        {
            DrawLine.isGamePaused = !DrawLine.isGamePaused;
            videoPlayer.Play(); 
        }
    }
}
