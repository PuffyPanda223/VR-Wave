using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// this script is attached to the centre eye anchor in the over camera rig, it will be taking screenshots from that perspective
public class screenShot : MonoBehaviour
{
  
    private bool takeScreenShot = false;


    private static screenShot instance;

    private void Awake()
    {
        instance = this;

    }


    // Returns the number of filess in the screenshots directory
    private int GetNumber()
    {
        int fileNum = 0; 
        string path = Application.persistentDataPath + "/screenshots/";
        DirectoryInfo info =  new DirectoryInfo(path);
        if ( Directory.Exists(path))
        {

            FileInfo[] fileInfo = info.GetFiles();
            // increment the fileNum for each file in the screenshot directory
            for (int i = 0; i < fileInfo.Length; i++) fileNum++;

            return fileNum;
        } else
        {
            Directory.CreateDirectory(path);
            return fileNum;
        }

       


    }


    /// <summary>
    ///  save a PNG image to the screenshots folder in the persistent data path
    /// </summary>
    private void captureScreenShot()
    {
        int numberOfFiles = GetNumber() + 1;
        try
        {
            Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
            byte[] byteArray = texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/screenshots/wave(" + numberOfFiles + ").png", byteArray);
        } catch (Exception e )
        {
            Debug.Log("error when capturing screen shot");
            Debug.LogError(e);
        }
    }


    public static void TakeScreenShot(int width, int height)
    {
        instance.captureScreenShot(); 
    }

}
