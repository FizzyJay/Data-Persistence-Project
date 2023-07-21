using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class HighScoreUIHandler : MonoBehaviour
{
    private GameManager gameManager;
    private GameManager.SaveData saveData;

    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        saveData = gameManager.LoadScores();
        SetHighScoreText();
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }

    public void SetHighScoreText()
    {
        scoreText.text = GetHighScoreText();
    }
    
    public string GetHighScoreText()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < saveData.highScores.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {saveData.highScores[i].playerName}: {saveData.highScores[i].score}");
        }
        return sb.ToString();
    }

}
