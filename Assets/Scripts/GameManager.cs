using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    private int maxHighScores = 10;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    public void GetPlayerName(string currentName)
    {
        playerName = currentName;
    }

    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
    }
    [System.Serializable]
    public class SaveData
    {
        public List<PlayerScore> highScores = new List<PlayerScore>();
    }
    // SaveScore saves the list of high scores
    public void SaveScore(int score)
    {
        // Save path string
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data = LoadScores();
        PlayerScore newScore = new PlayerScore();
        newScore.playerName = playerName;
        newScore.score = score;
        // add the latest score to the list
        data.highScores.Add(newScore);
        // sort by highest scores
        data.highScores.Sort((a, b) => b.score.CompareTo(a.score));
        // remove the lowest score on the new list
        Debug.Log(data.highScores.Count);
        while (data.highScores.Count > maxHighScores)
        {
            data.highScores.RemoveAt(data.highScores.Count - 1);
        }
        // remove duplicated scores: those are same name same score
        bool[] sameName = new bool[maxHighScores];
        bool[] sameScore = new bool[maxHighScores];

        for (int i = 0; i< data.highScores.Count; i++)
        {
            for (int j = 0; j< data.highScores.Count; j++)
            {
                if (i != j)
                {
                    if (data.highScores[i].playerName == data.highScores[j].playerName)
                    {
                        sameName[j] = true;
                    }
                    if (data.highScores[i].score == data.highScores[j].score)
                    {
                        sameScore[j] = true;
                    }
                }
            }
        }

        for (int i = 0; i < data.highScores.Count; i++)
        {
            if (sameName[i] && sameScore[i])
            {
                data.highScores.RemoveAt(i);
            }
        }

        // convert to json
        string json = JsonUtility.ToJson(data);
        // save the new list of high scores
        File.WriteAllText(path, json);
    }
    // LoadScores loads the list of high scores
    public SaveData LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        else
        {
            // creates new save data if none already exists in the persistent data path
            return new SaveData();
        }
    }
    public PlayerScore HighestScore()
    {
        // Want to find the highest score
        // Start by loading the current saved high scores
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data = LoadScores();
        // Sort the high scores highest to lowest
        data.highScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Highest score should be the first entry after sorting
        PlayerScore highestScore = data.highScores[0];

        // return the highest score
        return highestScore;
    }
}
