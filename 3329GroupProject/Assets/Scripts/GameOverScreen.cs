using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text scoreText;
    public static bool gameIsOver = false;

    public void setUp(int score)
    {
        gameObject.SetActive(true);
        if (MainControl.caught)
        {
            scoreText.text = "You Got Caught!!\nDetention After Class!!";  // "Score: " + score.ToString();
        }
        else
        {
            scoreText.text = "Times Up!!\nNot Enough Points!!";
        }
            
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
