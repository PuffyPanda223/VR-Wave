using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{


    private void Update()
    {
     
        if (GlobalTimer.timer > 30f)
        {
            if (SaveData.container.actors.Count > 1)
            {
                SaveData.Save();
            }
            SceneManager.LoadScene(1);
        }
    }
}
