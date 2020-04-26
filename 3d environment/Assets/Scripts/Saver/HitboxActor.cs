﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // allows access to the serializable field

// the data about the hitbox we want to store including everything the renderer needs to display the hitbox as well as the modified script attached to it
[Serializable]
public class HitboxData
{
    // these four fields are mesh data of the plane, canot store the entire mesh so we get the parts we need and strip them down to their barest form
    public int[] triangles;
    public float[] vertices;
    public float[] uv;
    public float[] normals; 


    // the data we modify the scripts with
    public float startTime;
    public float endTime;

    // the name corresponds to the difficulty
    public string difficulty;

    // this function seperates the parts of the mesh will will need to save in order to feed to the mesh filter on load
    public void GetMeshDetails(Mesh m)
    {
        
        vertices = new float[m.vertexCount * 3];
        for(int i = 0; i < m.vertexCount; i++)
        {
            vertices[i * 3] = m.vertices[i].x;
            vertices[i * 3 + 1 ] = m.vertices[i].y;
            vertices[i * 3 + 2 ] = m.vertices[i].z;

        }


        triangles = new int[m.triangles.Length];

        for(int i =0; i < m.triangles.Length; i++)
        {
            triangles[i] = m.triangles[i];
        }

        uv = new float[m.uv.Length * 2];

        for(int i =0; i < m.uv.Length; i++)
        {
            uv[i * 2] = m.uv[i].x;
            uv[i * 2 + 1] = m.uv[i].y; 
        }

        normals = new float[m.normals.Length *3 ]; 

        for(int i =0; i< m.normals.Length; i++)
        {
            normals[i * 3] = m.normals[i].x;
            normals[i * 3 + 1 ] = m.normals[i].y;
            normals[i * 3 + 2 ] = m.normals[i].z;
        }


    }
}
