using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    // Need to have the user enter their user name
    private GameManager gameManager;
    public TMP_InputField playerNameInputField;
    public string playerName;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start a new game by clicking the start button
    public void StartNew()
    {
        playerName = playerNameInputField.text;
        gameManager.GetPlayerName(playerName);

        // Set User name

        SceneManager.LoadScene(1); // Load the game scene
    }

    public void HighScore()
    {
        SceneManager.LoadScene(2); // Load the game scene
    }
    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
