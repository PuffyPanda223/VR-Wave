using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    
    private GameObject shadowBox;
    private MeshFilter shadowFilter;
    private MeshRenderer shadowRenderer;
    private Mesh shadowMesh;
    private HitBox shadowScript;
    public Material safe;
    public Material medium;
    public Material hard;
    // Start is called before the first frame update
    void Start()
    {
        List<HitboxData> data = new List<HitboxData>();
        data = SaveData.load();
        for(int i = 0; i < data.Count; i++)
        {
            shadowBox = new GameObject("plane");
            shadowFilter = shadowBox.AddComponent(typeof(MeshFilter)) as MeshFilter;
            shadowRenderer = shadowBox.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            shadowScript = shadowBox.AddComponent(typeof(HitBox)) as HitBox;
            Mesh shadowMesh = new Mesh();
            // data stores the triangles, uvs and vertices, give the mesh these values and then add them to the mesh filter


            shadowMesh.SetVertices(GenerateVertices(data[i].vertices));
            shadowMesh.triangles = data[i].triangles;
            shadowMesh.SetUVs(0, GenerateUV(data[i].uv));
            shadowMesh.SetNormals(GenerateNormals(data[i].normals));

            shadowFilter.mesh = shadowMesh;

            shadowScript.startTime = data[i].startTime;
            shadowScript.endTime = data[i].endTime;

            switch (data[i].difficulty)
            {
                case "safe":
                    shadowRenderer.material = safe;
                    break;
                case "medium":
                    shadowRenderer.material = medium;
                    break;
                case "hard":
                    shadowRenderer.material = hard;
                    break;
                default:
                    shadowRenderer.material = safe;
                    break;
            }


           Debug.Log("sjahudfdiuaSHDifuasyhfiu7syhefiusyfuisdft7ui8sdtf67sdtrf");
        }
    }

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

}
