using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainControl : MonoBehaviour
{
    private List<Action> actions=new List<Action>();
    public float tTurnTimeLeft=-10.0f, tBackTimeLeft=-10.0f;
    private bool tTurnWaiting=false, tBackWaiting=false;
    public TeacherMovement teacher;
    PlayerAnimation pAnim;
    private bool playerResume=false;
    public static bool caught=false;
    public int playerActing=0;
    public Level[] levels;
    public Level level;
    private int currentLevel=-1;
    public TMP_Text score, time;
    public TextMeshProUGUI plusScore;
    public GameOverScreen gameOverScreen;
    private bool waitGameOver=false;
    public LevelCompletedScreen levelCompletedScreen;
    public GameCompletedScreen gameCompletedScreen;
    AudioSource audioSource;
    AudioClip audioClip;
    private string rank = "";
    public static string[] rank_name = {"Notorious", "Troublesome", "Disobedient", "Naughty"};
    public static int[] rank_score = {800, 600, 500, 400};

    public void prepareNextLevel() {
        if(currentLevel>=2) {
            return;
        }
        if(currentLevel!=-1) {
            levels[currentLevel+1].marks=level.marks;
        }
        tTurnWaiting=tBackWaiting=false;
        foreach(Action action in actions) {
            action.resetAction();
            action.resetCooldown();
        }
        level=levels[++currentLevel];
        level.prepareTeacher();
        teacher=level.teacher;
    }
    public void gameOver()
    {
        setHighestScore();
        gameOverScreen.setUp(level.marks);
    }

    public void levelCompleted()
    {
        levelCompletedScreen.setUp(level.marks);
    }

    public void gameCompleted()
    {
        setHighestScore();
        gameCompletedScreen.setUp(level.marks, rank);
    }

    void teacherTurnAround() {
        teacher.preTurnAround=true;
        teacher.animator.SetBool("preTurnAround", true);
        teacher.Invoke("turnAround", level.tTurnDelay);
    }
    void teacherTurnBack() {
        teacher.animator.SetBool("turnAround", false);
        teacher.animator.SetBool("caught", false);
        teacher.preTurnAround=false;
    }

    public bool teacherTurnedAround() {
        return teacher.animator.GetBool("turnAround");
    }
    public bool teacherIsWalking() {
        if(teacher.animator==null) {
            return true;
        }
        return teacher.animator.GetCurrentAnimatorStateInfo(0).IsName(level.teacherName+"_Walk");
    }
    void teacherRandomlyTurnAround() {
        if(teacherIsWalking()) {
            if(!tTurnWaiting) {
                tTurnWaiting=true;
                tBackWaiting=false;
                tTurnTimeLeft=UnityEngine.Random.Range(level.tTurnPeriodStart, level.tTurnPeriodEnd);  // Adjust turn frequency
                return;
            }
            if(tTurnTimeLeft<=0) {
                teacherTurnAround();
            }
            else {
                tTurnTimeLeft-=Time.deltaTime;
            }
        }
        else if(teacherTurnedAround()) {
            /*
            if(caught) {
                tBackTimeLeft=2f;
                caught=false;  // ***
                playerResume=true;  // ***
                return;
            }
            */
            if(!tBackWaiting) {
                tBackWaiting=true;
                //tBackTimeLeft=UnityEngine.Random.Range(3.0f, 5.0f);
                tBackTimeLeft=3f;
                return;
            }
            if(tBackTimeLeft<=0f) {
                if(playerResume) {
                    playerResume=false;
                    pAnim.act();
                }
                teacherTurnBack();
                tTurnWaiting=false;
            }
            else {
                tBackTimeLeft-=Time.deltaTime;
            }
        }
    }
    public void getCaught() {
        caught=true;
        teacher.animator.SetBool("caught", true);
    }

    public void plusScoreUI(int score)
    {
        Debug.Log("Score +" + score.ToString());
        plusScore.enabled = true;
        plusScore.gameObject.SetActive(true);
        plusScore.text = "+" + score.ToString();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Audio/Scoring");
        audioSource.PlayOneShot(audioClip);
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.8f);
        plusScore.text = "";
        plusScore.enabled = false;
        plusScore.gameObject.SetActive(false);
    }

    void updateUI() {
        score.text="Score "+level.marks.ToString();
        if (currentLevel < 2) {
            int min = level.displayTime / 60;
            int sec = level.displayTime % 60;
            time.text = "Time " + min.ToString("00") + ":" + sec.ToString("00");
        } else {
            time.text = "Time   ∞";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pAnim=GameObject.Find("Player").GetComponent<PlayerAnimation>();
        plusScore.enabled = false;
        plusScore.gameObject.SetActive(false);

        // Action: (clip, audio, cdIcon, actionKey, holdFor, coolDown, marks, xOffset, yOffset)
        actions.Add(new Action("Player_Eating", "Eating", "EatCD", KeyCode.A, 2.5f, 3, 10, -0.08f, 0.01f));
        actions.Add(new Action("Player_Sleeping", "Sleeping", "SleepCD", KeyCode.S, 3, 4, 20, 0.04f, -0.05f));
        actions.Add(new Action("Player_Talking", "Talking", "TalkCD", KeyCode.D, 4, 5, 30, 0.07f, 0.08f));

        // Level: (teacher, timeLimit, lMarks, tTurnDelay, tTurnPeriodStart, tTurnPeriodEnd)
        levels = new Level[] {
            new Level("MaleTeacher", 60, 160, 0.8f, 3.8f, 10f),
            new Level("FemaleTeacher", 40, 280, 0.52f, 2.6f, 5.7f),
            new Level("OldTeacher", 5, 400, 0.42f, 1.3f, 3.5f)
        };
        prepareNextLevel();
        caught = false;
        level.start();
    }

    private void setHighestScore()
    {
        if (level.marks > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", level.marks);
        }
        for (int i = 0; i < 4; i++)
        {
            if (level.marks >= rank_score[i])
            {
                rank = rank_name[i];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // Disable player actions under these conditions
        if(MainMenu.mainMenuOpened || PauseMenu.gameIsPaused || waitGameOver 
            || LevelCompletedScreen.levelIsCompleted || !level.started || GameCompletedScreen.gameIsCompleted) {
            return;
        }
        if(caught) {
            if (currentLevel == 2) {
                if (level.marks >= level.lMarks) {  // Won
                    Invoke("gameCompleted", 4f);
                    return;
                }
            }
            Invoke("gameOver", 4f);  // Wait for 4s(caught audio) then invoke game over screen
            waitGameOver = true;
            return;
        }
        if(!level.updateTimer() && currentLevel!=2) {
            Time.timeScale = 0f;
            if (level.marks >= level.lMarks) {
                levelCompleted();
            } else {
                gameOver();
            }
            return;
        }
        updateUI();
        teacherRandomlyTurnAround();
        foreach(Action action in actions) {
            if(action.check()) {
                break;
            }
        }
    }
}
