using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // allows access to the serializable field
public class HitboxActor : MonoBehaviour
{

    public static HitboxData hit = new HitboxData();
    // get each component of a hitbox we want and add it to the global list container stored in the script Hitboxlist
    public static void StoreData(GameObject obj)
    {
        hit.mesh = obj.GetComponent<Mesh>();
        hit.position = obj.transform.position;
        hit.meshFilter = obj.GetComponent<MeshFilter>();
        hit.meshRenderer = obj.GetComponent<MeshRenderer>();
        hit.script = obj.GetComponent<HitBox>();
        SaveData.AddToList(hit);
    }
}


// the data about the hitbox we want to store including everything the renderer needs to display the hitbox as well as the modified script attached to it
[Serializable]
public class HitboxData
{
    public Mesh mesh;
    public Vector3 position;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public HitBox script; 
}

