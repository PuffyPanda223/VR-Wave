
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;


// attached to an object. When pressed by the pointer it will pass through itself
public class Interactable : MonoBehaviour
{
    FloatingText FloatingText;
    public static bool drawActive = false;
    private GameObject sphere;
    private VideoPlayer videoPlayer;
    
        private void Awake()
    {
        GameObject master = GameObject.Find("Game Master");
        FloatingText = master.GetComponent<FloatingText>();
    }

    

    public void Pressed(GameObject currentObject)
    {
        bool isHitBox = false;
       

        switch(currentObject.name.Substring(0,4))
        {
            case "safe":
                DrawStart();
                FloatingText.showFloatingText(5, currentObject);
                PointSystem.addScore(5);
                Destroy(currentObject);
                Debug.Log("safe");
                isHitBox = true;
                break;
            case "medi":
                FloatingText.showFloatingText(3, currentObject);
                PointSystem.addScore(3);
                Destroy(currentObject);
                Debug.Log("medium");
                isHitBox = true;
                break;
            case "hard":
                FloatingText.showFloatingText(2, currentObject);
                PointSystem.addScore(2);
                Destroy(currentObject);
                Debug.Log("hard");
                isHitBox = true;
                break;
            case "default":
                FloatingText.showFloatingText(5, currentObject);
                PointSystem.addScore(5);
                Destroy(currentObject);
                Debug.Log("safe");
                isHitBox = true;
                break;
        }

       
        // if the object wasn't a hitbox this will false which will then proceed to execute the rest of the code
        if (!isHitBox)
        {
            switch (currentObject.gameObject.name)
            {
                case "Play":
                    print("Play");
                    SceneManager.LoadScene(1);
                    break;
                case "Options":
                    print("Options");
                    MainToOptions();
                    break;
                case "Back":
                    OptionsToMain();
                    break;
                case "Credits":
                    OptionsToCredits();
                    break;
                case "Quit":
                    print("Quit");
                    MainToQuit();
                    break;
                case "No":
                    QuitToMain();
                    break;
                case "Yes":
                    print("Application Closes");
                    Application.Quit();
                    break;
                case "Skip":
                    SceneManager.LoadScene(2);
                    break;
                default:
                    break;
            }
        }
    }

    void MainToOptions()
    {
        GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = false;


        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;


        GameObject.Find("Back").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Credits").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Sample Options").GetComponent<BoxCollider>().enabled = true;
    }

    void MainToQuit()
    {
        GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("No").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;

        GameObject.Find("Yes").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("No").GetComponent<BoxCollider>().enabled = true;
    }

    void QuitToMain()
    {
        GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("No").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;

        GameObject.Find("Yes").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("No").GetComponent<BoxCollider>().enabled = false;
    }

    void OptionsToMain()
    {
        GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = true;


        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;


        GameObject.Find("Back").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Credits").GetComponent<BoxCollider>().enabled = false;

        GameObject.Find("FullCredits").GetComponent<MeshRenderer>().enabled = false;
    }

    void OptionsToCredits()
    {
        GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Credits").GetComponent<BoxCollider>().enabled = false;

        GameObject.Find("FullCredits").GetComponent<MeshRenderer>().enabled = true;
    }

    void DrawStart()
    {
        // get the entire sphere object which once we have we can use to find the video player component
        sphere = GameObject.Find("Sphere");
        // get the video player component containing the 3d footage we are using. 
        videoPlayer = sphere.GetComponent<VideoPlayer>();
        DrawLine.isGamePaused = true;
        drawActive = true;
        videoPlayer.Pause();
    }
}
