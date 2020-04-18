using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{

    public float gameTime = 0f;


    private void Update()
    {
        if (gameTime >20f)
        {
            SaveData.Save(); 
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
