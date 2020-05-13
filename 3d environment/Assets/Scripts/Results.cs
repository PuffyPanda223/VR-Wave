using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System; 



public class Results : MonoBehaviour
{
    public static List<float> timesList = new List<float>();
    public static List<float> scoreList = new List<float>();
    public static HighScoreActor container = new HighScoreActor();
    public GameObject resultText;
    public GameObject totalText;

    // Start is called before the first frame update
    void Start()
    {
        createResults();
        saveResults(); 
    }

    private void saveResults()
    {
        Highscore saveResult = new Highscore();
        saveResult.name = PlayerName.playerName;
        saveResult.score = PointSystem.playerScore;
        


        string path = Application.persistentDataPath + "/highscores.txt";
        string json;
        // add this score to the highscores list
        // if data already exists add that to the score as well
        if (File.Exists(path))
        {
            try
            {
                json = File.ReadAllText(path);
                container = JsonUtility.FromJson<HighScoreActor>(json);
                container.actor[1].name = "Jake";
            }
            catch (Exception e)
            {
                Debug.LogError("could not get the highscores");
                Debug.LogError(e);
            }

        }
        container.actor.Add(saveResult);
        // save the final score list back to the highscores text file   

        string saveScore = JsonUtility.ToJson(container);

        try
        {

            //creating the path over writes the current data if it exists
          
            StreamWriter sw = File.CreateText(path);
            sw.Close();
            File.WriteAllText(path, saveScore);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }


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
