using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWave : MonoBehaviour
{

    // this is a global variable that will tell our vr hitbox drawing function which material to apply to the hitbox 
    public static string difficulty = "safe";

    public static void Change()
    {
        // we use the reticle to inform the player which difficulty of hitbox you are using. When you change the wave we want to change the color of the reticle as well
        GameObject gamemaster = GameObject.Find("PR_Reticule");
        SpriteRenderer reticle = gamemaster.GetComponent<SpriteRenderer>(); 

        switch (difficulty)
        {
            case "safe":
                difficulty = "medium";
                reticle.color = Color.yellow; 
                break;
            case "medium":
                difficulty = "hard";
                reticle.color = Color.red;
                break;
            case "hard":
                difficulty = "safe";
                reticle.color = Color.green; 
                break;
   
        }

    }


}
