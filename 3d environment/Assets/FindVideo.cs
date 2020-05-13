using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using System.IO;

public class FindVideo : MonoBehaviour
{

    public VideoPlayer video;
    private string rootPath;
    private string path;
    private string fileName = "https://youtu.be/GafZur3U4-4";

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            /*
            rootPath = Application.persistentDataPath;
            path = Path.Combine(rootPath, fileName);
            */ 

            try
            {
                video.url = fileName;
                video.Play();
               
            } catch (Exception e)
            {
                Debug.Log("some error occured " + e);
            }
        }
    }

  


}
