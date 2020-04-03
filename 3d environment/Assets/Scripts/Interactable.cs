using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


// attached to an object. When pressed by the pointer it will pass through itself
public class Interactable : MonoBehaviour
{

  
    public GameObject floatingText;
  

    public void Pressed(GameObject currentObject)
    {
        bool isHitBox = false;
       

        if (currentObject.name.Substring(0, 4) == "Safe")
        {
            DrawLine.showFloatingText(5, currentObject, floatingText);
            PointSystem.addScore(5);
            Destroy(currentObject);
            Debug.Log("safe");
            isHitBox = true;

        }
        else if (currentObject.name.Substring(0, 4) == "hard")
        {
            DrawLine.showFloatingText(1, currentObject, floatingText);
            PointSystem.addScore(1);
            Destroy(currentObject);
            Debug.Log("hard");
            isHitBox = true;
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

}
