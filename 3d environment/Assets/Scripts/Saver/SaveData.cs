using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


// a static class that will act as an inbetween between the save and load functions and the game. This class contains a global list of hitboxes that it both and and get from and send the data to the appropriate functions
public class SaveData 
{

    public static HitBoxList container = new HitBoxList();
    public static string path = Application.persistentDataPath + "'\'save'\'hitbox.txt";
    public delegate void SerializeAction();
    public static event SerializeAction onLoaded;
    public static event SerializeAction onBeforeSave; 



  

    // get a list of from JSON and add it to the static hitbox list
    public static List<HitboxData> load()
    {
        container = LoadActors();
        //Debug.Log(container.actors);
        // return the list
        return container.actors;
    }


    // return in the format of the hitbox list class a list of objects stored in the save file
    private static HitBoxList LoadActors()
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<HitBoxList>(json); 
    }


    public static void Save()
    {
        string json = JsonUtility.ToJson(container);

        StreamWriter sw = File.CreateText(path);
        sw.Close();


        File.WriteAllText(path, json);
       
    }

    // called from the hitbox actor script. When a new actor is created this function will be called to add the hitbox to the global list of hitboxes
    public static void AddToList(HitboxData data)
    {
        container.actors.Add(data);
       
    }

    public static void clearList()
    {
        container.actors.Clear();
    }

}
