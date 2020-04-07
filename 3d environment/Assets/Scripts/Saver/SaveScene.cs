using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary; 


// this class will save an incoming list of Gameobjects (hitboxes) into a file. When we load a game all that is happening is the game master will get the list and instantiate all the hitboxess
public class SaveScene : MonoBehaviour
{
    

    // take in a list of gameObjects to save into a binary formatted file
    public static void Save (GameObject[] objects)
    {
        // this is a persistant dir. We don't have to test what device we are on, unity automatically handles that
        string path = Application.persistentDataPath + "/saves/";
        // the name of the save file
        string save_file = "saveFile/";

        // if the directory does not exist create it
        Directory.CreateDirectory(path);
        // this formatter object will handle the serialization into bindary
        BinaryFormatter formatter = new BinaryFormatter();

        // once we are done using the file stream we need to dispose of it. This using statement automatically releases the stream from memory
        using (FileStream fileStream = new FileStream(path + save_file + ".txt" , FileMode.Create))
        {
            formatter.Serialize(fileStream, objects);
        }
        Debug.Log("should have saved");

    }

    // find all the hitboxes with the hitbox script attached and save them
    public void GetHitBox()
    {

        // at runtime whenever a hitbox is created it is assigned to the hitbox tag. This allows us to retrieve a list of them here
        GameObject[] data = GameObject.FindGameObjectsWithTag("hitBox");
        Save(data);





    }
}
