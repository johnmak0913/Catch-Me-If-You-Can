using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static bool mainMenuOpened = false;
    public TMP_Text highestScore, highestRank;
    int hScore = 0;

    void Start()
    {
        mainMenuOpened = true;
        hScore = PlayerPrefs.GetInt("HighestScore", 0);
        highestScore.text = hScore.ToString();
        for (int i = 0; i < 4; i++)
        {
            if (hScore >= MainControl.rank_score[i])
            {
                highestRank.text = MainControl.rank_name[i];
                break;
            }
        }
    }

    public void PlayGame()
    {
        mainMenuOpened = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
