using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


/// <summary>
/// saves and loads from a static container a list of hitboxes. 
/// </summary>
public class SaveData 
{

    public static HitBoxList container = new HitBoxList();
    public static string path = Application.persistentDataPath + "'\'save'\'hitbox.txt";
    public delegate void SerializeAction();



  

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


    /// <summary>
    /// Save the data currently in the static container
    /// </summary>
    public static void Save()
    {
        string json = JsonUtility.ToJson(container);
        try
        {
            StreamWriter sw = File.CreateText(path);
            sw.Close();
            File.WriteAllText(path, json);
        } catch (Exception e )
        {
            Debug.Log(e); 
        }

       
       
    }

    /// <summary>
    /// When a hitbox is created, this function is invoked. Adding the generated hitbox data to a list
    /// </summary>
    /// <param name="data"></param>
    public static void AddToList(HitboxData data)
    {
        container.actors.Add(data);
       
    }

    /// <summary>
    /// clear the current contains of the static container
    /// </summary>
    public static void clearList()
    {
        container.actors.Clear();
    }

}
