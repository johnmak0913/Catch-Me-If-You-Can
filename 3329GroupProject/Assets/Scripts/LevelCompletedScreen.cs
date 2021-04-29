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

    public void setUp(int score)  // score not used yet
    {
        gameObject.SetActive(true);
        // scoreText.text = "Seems Like You're Good At\nBeing A Bad Student ( ͡° ͜ʖ ͡°)";
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
