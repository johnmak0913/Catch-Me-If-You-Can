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
    private MainControl control;

    void Start() {
        control=GameObject.Find("MainControl").GetComponent<MainControl>();
    }

    public void setUp(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        levelIsCompleted = true;
    }

    public void nextLevelButton()
    {
        control.level.removeTeacher();
        control.prepareNextLevel();
        gameObject.SetActive(false);
        levelIsCompleted = false;
        control.level.start();
    }
}
