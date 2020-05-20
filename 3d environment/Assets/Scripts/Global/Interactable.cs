
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;


// attached to an object. When pressed by the pointer it will pass through itself
public class Interactable : MonoBehaviour
{
    FloatingText FloatingText;
    public static bool drawActive = false;
    private GameObject sphere;
    private VideoPlayer videoPlayer;
    Scene currentScene;
        private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        // the game master game object does not exist in the menu scenes or intro scenes, we only want to display floating text when a user clicks on wave which is only done in the main scene so we check if its the main scene/ 
        // if we don't do this check unity will freak out because the game object it is searching for doesn't exist and stop the rest of the script from complining and running
        if (sceneName == "VR main Scene")
        {
            GameObject master = GameObject.Find("Game Master");
            FloatingText = master.GetComponent<FloatingText>();
        }
       
    }

    

    public void Pressed(GameObject currentObject)
    {
        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        bool isHitBox = false;
        if (sceneName == "VR main Scene")
        {

            string name = currentObject.transform.GetComponent<MeshRenderer>().material.name;
            switch (name.Substring(0, 4))
            {
                case "safe":
                    DrawStart();
                    Results.addData(CountDownTimer.timer, 5);
                    FloatingText.showFloatingText(5, currentObject);
                    PointSystem.addScore(5);
                    Destroy(currentObject);
                    isHitBox = true;
                    break;
                case "medi":
                    DrawStart();
                    Results.addData(CountDownTimer.timer, 3);
                    FloatingText.showFloatingText(3, currentObject);
                    PointSystem.addScore(3);
                    Destroy(currentObject);
                    isHitBox = true;
                    break;
                case "hard":
                    DrawStart();
                    Results.addData(CountDownTimer.timer, 2);
                    FloatingText.showFloatingText(2, currentObject);
                    PointSystem.addScore(2);
                    Destroy(currentObject);
                    isHitBox = true;
                    break;
                default:
                    isHitBox = true;
                    break;
            }
        }

       
        // if the object wasn't a hitbox this will false which will then proceed to execute the rest of the code
        if (!isHitBox)
        {
            switch (currentObject.gameObject.name)
            {
                case "Play":
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.INTRO);
                    break;
                case "Options":
                    MainToOptions();
                    break;
                case "Back":
                    OptionsToMain();
                    break;
                case "Credits":
                    OptionsToCredits();
                    break;
                case "Quit":               
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
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.MAIN_SCENE);
                    break;
                // back from results to main menu 
                case "BackR":
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.MAIN_MENU);
                    break;
                // to the level editor
                case "level":
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.LEVEL_CREATOR);
                    break;
                // from main menu to the input scene
                case "Change":
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.INPUT);
                    break;
                default:
                    break;
            }
        }
    }


    void MainToOptions()
    {
        try
        {
            GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("level").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Change").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("changeText").GetComponent<MeshRenderer>().enabled = false;

            GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = true;

            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("level").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Change").GetComponent<BoxCollider>().enabled = false;


            GameObject.Find("Back").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Credits").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Sample Options").GetComponent<BoxCollider>().enabled = true;
        } catch (Exception e)
        {
            Debug.Log("MainToOptions function stopped working");
            Debug.LogError(e);
        }

    }

    void MainToQuit()
    {
        try
        {
            GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("level").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Change").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("changeText").GetComponent<MeshRenderer>().enabled = false;



            GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("No").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = true;

            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("level").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Change").GetComponent<BoxCollider>().enabled = false;

            GameObject.Find("Yes").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("No").GetComponent<BoxCollider>().enabled = true;
        } catch (Exception e)
        {
            Debug.Log("something went wrong in the MainToQuit function");
            Debug.LogError(e);

        }
    }

    void QuitToMain()
    {
        try
        {
            GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("level").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Change").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("changeText").GetComponent<MeshRenderer>().enabled = true;




            GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("No").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = false;

            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("level").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Change").GetComponent<BoxCollider>().enabled = true;

            GameObject.Find("Yes").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("No").GetComponent<BoxCollider>().enabled = false;
        } catch (Exception e )
        {
            Debug.Log("something went in the QuitToMain function");
            Debug.LogError(e); 
        }
    }

    void OptionsToMain()
    {
        try
        {
            GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("level").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Change").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("changeText").GetComponent<MeshRenderer>().enabled = true;

            GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = false;

            GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("level").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Change").GetComponent<BoxCollider>().enabled = true;


            GameObject.Find("Back").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Credits").GetComponent<BoxCollider>().enabled = false;

            GameObject.Find("FullCredits").GetComponent<MeshRenderer>().enabled = false;
        } catch (Exception e )
        {
            Debug.Log("something went wrong in the OptionsToMain function");
            Debug.LogError(e); 
        }
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

        // get the video player component containing the 3d footage we are using. 
        sphere = GameObject.Find("Sphere");
        videoPlayer = sphere.GetComponent<VideoPlayer>();
        DrawLine.isGamePaused = true;
        drawActive = true;
        videoPlayer.Pause();
    }
}
