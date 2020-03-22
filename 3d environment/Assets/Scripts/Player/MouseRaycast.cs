using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MouseRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    switch (hit.transform.gameObject.name)
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
                        default:
                            print(hit.transform.gameObject.name);
                            break; 
                    }
                }
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

