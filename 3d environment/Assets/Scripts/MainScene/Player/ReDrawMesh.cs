using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReDrawMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshFilter meshFilter;
    public MeshCollider meshCollider; 
    void Start()
    {
        Vector3[] normals = meshFilter.mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        meshFilter.mesh.normals = normals;


        int[] tris = meshFilter.mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            int t = tris[i];
            tris[i] = tris[i + 2];
            tris[i + 2] = t;
        }

        meshFilter.mesh.triangles = tris;

        DestroyImmediate(meshCollider);
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    
}
