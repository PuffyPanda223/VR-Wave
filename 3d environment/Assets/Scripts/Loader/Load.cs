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
            shadowMesh = new Mesh();
            shadowScript = shadowBox.AddComponent(typeof(HitBox)) as HitBox;


            shadowMesh = data[i].mesh; 
            shadowFilter = data[i].meshFilter;
            shadowRenderer = data[i].meshRenderer;
            shadowBox.transform.position = data[i].position;
            shadowScript = data[i].script; 
        }
    }

}
