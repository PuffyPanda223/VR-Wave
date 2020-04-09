using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

// this class will save an incoming list of Gameobjects (hitboxes) into a file. When we load a game all that is happening is the game master will get the list and instantiate all the hitboxess
public class SaveScene : MonoBehaviour
{
    
    
    // take in a list of gameObjects to save into a binary formatted file
    public static void Save (GameObject[] objects)
    {
        // add all our hitboxes to a global list to then be stored
        for(int i =0; i < objects.Length; i++)
        {
            HitboxActor.StoreData(objects[i]);
        }

        // because the list which we updated in the for loop above is stored in the same script we don't have to pass anything, jsut call the method
        SaveData.Save(); 
    }

    // find all the hitboxes with the hitbox script attached and save them
    public void GetHitBox()
    {

        // at runtime whenever a hitbox is created it is assigned to the hitbox tag. This allows us to retrieve a list of them here
        GameObject[] data = GameObject.FindGameObjectsWithTag("hitBox");
        Save(data);
    }
}
