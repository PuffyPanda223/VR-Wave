using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoRaycaster : MonoBehaviour
{

    public VideoPlayer vp;
    

    // Start is called before the first frame update
    void Start()
    {
        vp.loopPointReached += Vp_loopPointReached;
    }

    private void Vp_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene(2);
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
                        case "Skip":
                            print("Skip");
                            SceneManager.LoadScene(2);
                            break;
                        default:
                            print(hit.transform.gameObject.name);
                            break; 
                    }
                }
            }
        }    
    }
}

