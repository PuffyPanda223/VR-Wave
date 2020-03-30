using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static float playerScore;
    public TextMeshPro scoreDisplay;


    private float timer;
    public void addScore(int score)
    {
        playerScore += score;
        //Debug.Log(playerScore);
    }

    private void Update()
    {
        scoreDisplay.GetComponent<TextMeshPro>().text = "Score: " + playerScore.ToString();
    }


}
