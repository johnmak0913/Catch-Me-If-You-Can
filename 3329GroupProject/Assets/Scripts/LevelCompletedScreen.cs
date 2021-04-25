using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompletedScreen : MonoBehaviour
{
    public TMP_Text scoreText;
    public static bool levelIsCompleted = false;

    public void setUp(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        levelIsCompleted = true;
    }

    public void nextLevelButton()
    {
        // SceneManager.LoadScene("Game");
        // *Go to next level
        levelIsCompleted = false;
    }
}
