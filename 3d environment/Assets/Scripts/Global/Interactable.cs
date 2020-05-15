
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;


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
                case "BackR":
                    //ResultsToMain();
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.MAIN_MENU);
                    break;
                case "level":
                    SceneManager.LoadScene((int)sceneEnum.loadSceneEnum.LEVEL_CREATOR);
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
        GameObject.Find("level").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("High").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("highText").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("level").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("High").GetComponent<BoxCollider>().enabled = false;


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
        GameObject.Find("level").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("High").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("highText").GetComponent<MeshRenderer>().enabled = false;



        GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("No").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("level").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("High").GetComponent<BoxCollider>().enabled = false;

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
        GameObject.Find("level").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("High").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("highText").GetComponent<MeshRenderer>().enabled = true;




        GameObject.Find("ConfirmationText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Yes").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("YesText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("No").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("NoText").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("level").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("High").GetComponent<BoxCollider>().enabled = true;

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
        GameObject.Find("level").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("levelText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("High").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("highText").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Back").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BackText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Credits").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("CreditsText").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Sample Options").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("level").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("High").GetComponent<BoxCollider>().enabled = true;


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

    void ResultsToMain()
    {
        GameObject.Find("WaveColumn").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("TimeColumn").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("ScoreColumn").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("TotalColumn").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("WaveTitle").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("TimeTitle").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("ScoreTitle").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("TotalTitle").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BackR").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("BackRText").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("WaveColumn").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("TimeColumn").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("ScoreColumn").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("TotalColumn").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("BackR").GetComponent<BoxCollider>().enabled = false;

        GameObject.Find("Play").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("PlayText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Options").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("OptionsText").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Quit").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("QuitText").GetComponent<MeshRenderer>().enabled = true;

        GameObject.Find("Play").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Options").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("Quit").GetComponent<BoxCollider>().enabled = true;

        GameObject[] resultData = GameObject.FindGameObjectsWithTag("Result");

        for (var i = 0; i < resultData.Length; i++)
        {
            Destroy(resultData[i]);
        }

        Results.scoreList.Clear();
        Results.timesList.Clear();

        GameObject.Find("Title").GetComponent<TextMeshPro>().text = "Surf VR Project";
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
