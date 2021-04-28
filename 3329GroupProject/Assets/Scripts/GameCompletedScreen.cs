using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameCompletedScreen : MonoBehaviour
{
    public TMP_Text scoreText, rankText;
    public static bool gameIsCompleted = false;

    public void setUp(int score, string rank)
    {
        gameObject.SetActive(true);
        scoreText.text = score.ToString();
        rankText.text = rank;
        gameIsCompleted = true;
    }

    public void exitButton()
    {
        SceneManager.LoadScene("MainMenu");
        gameIsCompleted = false;
    }
}
