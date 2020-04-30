using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Results : MonoBehaviour
{
    public static List<float> timesList = new List<float>();
    public static List<float> scoreList = new List<float>();
    public GameObject resultText;
    public GameObject totalText;

    // Start is called before the first frame update
    void Start()
    {
        createResults();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static void addData(float time, int score)
    {
        timesList.Add(time);
        scoreList.Add(score);
    }

    public void createResults()
    {
        int waveCount = 1;
        for (var i = 0; i < timesList.Count; i++)
        {
            var newText = Instantiate(resultText, new Vector3(-2.2f, i * -0.5f +0.7f, 7), Quaternion.identity);
            newText.GetComponent<TextMeshPro>().text = waveCount.ToString();
            newText.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            waveCount += 1;
        }

        for (var i = 0; i < timesList.Count; i++)
        {
            var newText = Instantiate(resultText, new Vector3(0, i*-0.5f + 0.7f, 7), Quaternion.identity);
            newText.GetComponent<TextMeshPro>().text = timesList[i].ToString("F1");
            newText.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        for (var i = 0; i < scoreList.Count; i++)
        {
            var newText = Instantiate(resultText, new Vector3(2.2f, i * -0.5f + 0.7f, 7), Quaternion.identity);
            newText.GetComponent<TextMeshPro>().text = scoreList[i].ToString();
            newText.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        float totalScore = 0;
        for (var i = 0; i < scoreList.Count; i++)
        {
            totalScore += scoreList[i];
        }
        totalText.GetComponent<TextMeshPro>().text = "Total Score: " + totalScore.ToString();
    }
}
