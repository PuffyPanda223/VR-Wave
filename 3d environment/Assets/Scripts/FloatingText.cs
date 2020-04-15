using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public GameObject floatingText; 
    public void showFloatingText(int score, GameObject enemy)
    {
        Vector3 pos = gameObject.GetComponent<MeshFilter>().mesh.vertices[0];
        var display = Instantiate(floatingText, pos, Quaternion.LookRotation(pos));
        display.GetComponent<TextMesh>().text = "+" + score.ToString();
    }

}
