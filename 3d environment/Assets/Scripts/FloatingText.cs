using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public GameObject floatingText; 
    public void showFloatingText(int score, GameObject enemy)
    {

        var display = Instantiate(floatingText, enemy.transform.position, Quaternion.LookRotation(enemy.transform.position));
        display.GetComponent<TextMesh>().text = "+" + score.ToString();
    }

}
