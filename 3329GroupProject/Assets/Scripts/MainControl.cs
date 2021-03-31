using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainControl : MonoBehaviour
{
    private List<Action> actions=new List<Action>();
    public float tTurnTimeLeft=-10.0f, tBackTimeLeft=-10.0f;
    private bool tTurnWaiting=false, tBackWaiting=false;
    public TeacherMovement teacher;
    PlayerAnimation pAnim;
    private bool caught=false, playerResume=false;
    public Level levels;
    public Level level;

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
                tTurnTimeLeft=UnityEngine.Random.Range(7.0f, level.tTurnPeriod);
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
                caught=false;
                playerResume=true;
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
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("OldTeacher_old").SetActive(false);
        //teacher=GameObject.Find("OldTeacher").GetComponent<TeacherMovement>();
        pAnim=GameObject.Find("Player").GetComponent<PlayerAnimation>();
        actions.Add(new Action("Player_Eating", KeyCode.E, 3, 3, 1, -0.08f, 0.01f));
        actions.Add(new Action("Player_Sleeping",KeyCode.S, 3, 2, 2, 0.04f, -0.05f));
        actions.Add(new Action("Player_Talking",KeyCode.T, 3, 2, 3, 0.07f, 0.08f));
        level=new Level("OldTeacher", 60, 60, 1f, 10f);
        level.start();
        teacher=level.teacher;
    }

    // Update is called once per frame
    void Update()
    {   
        if(teacher==null) {
            return;
        }
        teacherRandomlyTurnAround();
        /*if(!level.updateTimer()) {
            Debug.Log("Time's up");
            return;
        }*/
        foreach(Action action in actions) {
            if(action.check()) {
                break;
            }
        }
    }
}
