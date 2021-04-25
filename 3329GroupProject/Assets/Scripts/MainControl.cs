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
    private bool caught=false, playerResume=false;
    public Level[] levels;
    public Level level;
    public TMP_Text score, time;
    public GameOverScreen gameOverScreen;
    private bool waitGameOver=false;

    public void gameOver()
    {
        gameOverScreen.setUp(level.marks);
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
        return teacher.animator.GetCurrentAnimatorStateInfo(0).IsName(level.teacherName+"_Walk");
    }
    void teacherRandomlyTurnAround() {
        if(teacherIsWalking()) {
            if(!tTurnWaiting) {
                tTurnWaiting=true;
                tBackWaiting=false;
                tTurnTimeLeft=UnityEngine.Random.Range(5.0f, level.tTurnPeriod);  // Adjust turn frequency
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
            if(caught) {
                tBackTimeLeft=2f;
                caught=false;  // ***
                playerResume=true;  // ***
                return;
            }
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

    void updateUI() {
        score.text="Score "+level.marks.ToString();
        int min=level.displayTime/60;
        int sec=level.displayTime%60;
        time.text="Time "+min.ToString("00")+":"+sec.ToString("00");
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("OldTeacher_old").SetActive(false);
        //teacher=GameObject.Find("OldTeacher").GetComponent<TeacherMovement>();
        pAnim=GameObject.Find("Player").GetComponent<PlayerAnimation>();
        
        // Action: (clip, audio, cdIcon, actionKey, holdFor, coolDown, marks, xOffset, yOffset)
        actions.Add(new Action("Player_Eating", "Eating", "EatCD", KeyCode.A, 1.5f, 2, 10, -0.08f, 0.01f));
        actions.Add(new Action("Player_Sleeping", "Sleeping", "SleepCD", KeyCode.S, 3, 3, 20, 0.04f, -0.05f));
        actions.Add(new Action("Player_Talking", "Talking", "TalkCD", KeyCode.D, 4, 4, 30, 0.07f, 0.08f));
        
        // Level: (teacher, timeLimit, lMarks, tTurnDelay, tTurnPeriod)
        levels = new Level[] {
            new Level("MaleTeacher", 30, 60, 1f, 10f),
            new Level("FemaleTeacher", 40, 60, 1f, 10f),
            new Level("OldTeacher", 60, 60, 1f, 10f)
        };
        level=levels[0];
        level.start();
        teacher=level.teacher;
    }

    // Update is called once per frame
    void Update()
    {   
        if(teacher==null) {
            return;
        }
        // Disable player actions under these conditions
        if (MainMenu.mainMenuOpened || PauseMenu.gameIsPaused || waitGameOver) {
            return;
        }
        if (caught) {
            Invoke("gameOver", 2f);  // Wait for 2s then invoke game over screen
            waitGameOver = true;
            caught = false;
            return;
        }
        if(!level.updateTimer()) {
            // if level.marks < target score
            Time.timeScale = 0f;
            gameOver();
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
