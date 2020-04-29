using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    /*
    private GameObject shadowBox;
    private MeshFilter shadowFilter;
    private MeshRenderer shadowRenderer;
    private Mesh shadowMesh;
    private HitBox shadowScript;
    private MeshCollider shadowCollider;
    */
    public Material safe;
    public Material medium;
    public Material hard;

   
    private void Awake()
    {
        // attaching a delegate method to the scene loaded callback, it comes with the scene and scene mode
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }


    // very important we unsubscribe our delegate methods once the scene is no longer used otherwise multiple functions will fire at the same time
    private void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    // when 
    private void loadHitBoxes()
    {
        Debug.Log("Loading in hitboxes now");
        SaveData.clearList();
        List<HitboxData> data = new List<HitboxData>();
        data = SaveData.load();

        for (int i = 0; i < data.Count; i++)
        {
            GameObject shadowBox = new GameObject("plane" + i);
            MeshFilter shadowFilter = shadowBox.AddComponent(typeof(MeshFilter)) as MeshFilter;
            MeshRenderer shadowRenderer = shadowBox.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            HitBox shadowScript = shadowBox.AddComponent(typeof(HitBox)) as HitBox;
            MeshCollider shadowCollider = shadowBox.AddComponent(typeof(MeshCollider)) as MeshCollider;
           // Interactable shadowInteract = shadowBox.AddComponent<Interactable>() as Interactable;

            Mesh shadowMesh = new Mesh();
            // data stores the triangles, uvs and vertices, give the mesh these values and then add them to the mesh filter
            // data stores the triangles, uvs and vertices, give the mesh these values and then add them to the mesh filter

            // the mesh of an object is split into different aspects, the vertices are the points in the world, the triangles are the way in which those points connect, the uvs tell the renderer where to put materials and normals 
            // make the mesh collider possible
            List<Vector3> verts = GenerateVertices(data[i].vertices);
            shadowMesh.SetVertices(verts);
            shadowMesh.triangles = data[i].triangles;
            shadowMesh.SetUVs(0, GenerateUV(data[i].uv));
            shadowMesh.SetNormals(GenerateNormals(data[i].normals));


            shadowFilter.mesh = shadowMesh;
            shadowCollider.sharedMesh = shadowFilter.mesh;
            shadowScript.startTime = data[i].startTime;
            shadowScript.endTime = data[i].endTime;


            // layer 9 is the hitbox layer which the gameobject has to be in order for the game to detect if the user has hit it or not
            shadowBox.layer = 9;
            shadowBox.tag = "hitBox";

            // figure out which difficulty of wave was saved and set the material to the corresponding difficulty 
            switch (data[i].difficulty)
            {
                case "safe":
                    shadowRenderer.material = safe;
                    break;
                case "mediu":
                    shadowRenderer.material = medium;
                    break;
                case "hard":
                    shadowRenderer.material = hard;
                    break;
                default:
                    shadowRenderer.material = safe;
                    break;
            }

        }
    }


    // The mesh dataclass takes a list of vector3s not an array of floats. But becasue we had to break it up to save it to a simple array we need to reasemble it. 
    private List<Vector3> GenerateNormals(float[] normals)
    {
        List<Vector3> normal = new List<Vector3>(); 

        for(int i = 0; i < normals.Length/3; i++)
        {
            normal.Add(new Vector3(
                   normals[i*3], normals[i*3 + 1 ], normals[i*3 + 2 ]
                ));
        }
        return normal;
    }



    // The mesh dataclass takes a list of vector3s not an array of floats. But becasue we had to break it up to save it to a simple array we need to reasemble it. 
    private List<Vector2> GenerateUV(float[] uv)
    {
        List<Vector2> listUV = new List<Vector2>(); 

        for(int i = 0; i < uv.Length / 2; i++)
        {
            listUV.Add(new Vector2(
                    uv[i*2], uv[i*2 + 1]
                ));
        }
        return listUV;
    }


    // The mesh dataclass takes a list of vector3s not an array of floats. But becasue we had to break it up to save it to a simple array we need to reasemble it back into a list class
    private List<Vector3> GenerateVertices(float[] verts)
    {
        List<Vector3> vertices = new List<Vector3>(); 

        for(int i = 0; i < verts.Length / 3; i++)
        {
            vertices.Add(new Vector3(
                    verts[i*3], verts[i*3 + 1 ], verts[i * 3 + 2]
                ));
        }

        return vertices;
    }

  
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
  
        // static fields do not reset when loading from one scene to another, so if the scene is the main scene reset everything that needs to be reset for the game to be replayed 
        if(scene.name == "VR main Scene")
        {
                PointSystem.playerScore = 0;
                loadHitBoxes();
            
        }
    }

}
