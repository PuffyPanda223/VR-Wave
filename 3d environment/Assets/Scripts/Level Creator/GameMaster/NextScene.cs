﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{


    private void Update()
    {
     
        if (GlobalTimer.timer > 15f)
        {

            SaveData.Save();
            SceneManager.LoadScene(1);
        }
    }
}
