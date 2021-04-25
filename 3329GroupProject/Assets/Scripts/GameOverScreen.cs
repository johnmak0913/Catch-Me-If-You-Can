using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public static bool gameIsOver = false;

    public void setUp(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        gameIsOver = true;
    }

    public void restartButton()
    {
        SceneManager.LoadScene("Game");
        gameIsOver = false;
    }

    public void exitButton()
    {
        SceneManager.LoadScene("MainMenu");
        gameIsOver = false;
    }
}
