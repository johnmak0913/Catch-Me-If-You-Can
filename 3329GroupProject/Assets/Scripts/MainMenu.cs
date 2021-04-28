using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static bool mainMenuOpened = false;
    public TMP_Text highestScore;

    void Start()
    {
        mainMenuOpened = true;
        highestScore.text = PlayerPrefs.GetInt("HighestScore", 0).ToString();
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
