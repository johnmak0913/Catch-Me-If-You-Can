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

    public void setUp(int score)  // score not used yet
    {
        gameObject.SetActive(true);
        if (MainControl.caught)
        {
            // scoreText.text = "You Got Caught!!\nDetention After Class \\( @o@)/";  // "Score: " + score.ToString();
        }
        else
        {
            scoreText.text = "Times Up!!\nNot Enough Points... ( TaT)";
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
