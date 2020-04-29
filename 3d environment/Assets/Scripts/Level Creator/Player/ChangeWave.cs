using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWave
{

    public static string difficulty = "safe";
   

    public static void Change()
    {
        switch (difficulty)
        {
            case "safe":
                difficulty = "medium";
                break;
            case "medium":
                difficulty = "hard";
                break;
            case "hard":
                difficulty = "safe";
                break;
   
        }

    
    }


}
