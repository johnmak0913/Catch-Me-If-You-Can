using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainControl : MonoBehaviour
{
    private Action eating, sleeping;
    public int marks=0;
    public float timeLeft=60.0f;
    public float turnAroundDelay=1.0f;
    public float tTurnTimeLeft=-10.0f, tBackTimeLeft=-10.0f;
    public bool tTurnWaiting=false, tBackWaiting=false;
    public TeacherMovement teacher;

    bool updateTimer() {
        timeLeft-=Time.deltaTime;
        return timeLeft>=0;
    }
    void teacherTurnAround() {
        teacher.preTurnAround=true;
        teacher.animator.SetBool("preTurnAround", true);
        teacher.Invoke("turnAround", turnAroundDelay);
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
        return teacher.animator.GetCurrentAnimatorStateInfo(0).IsName("OldTeacher_Walk");
    }
    void teacherRandomlyTurnAround() {
        if(teacherIsWalking()) {
            if(!tTurnWaiting) {
                tTurnWaiting=true;
                tBackWaiting=false;
                tTurnTimeLeft=UnityEngine.Random.Range(7.0f, 10.0f);
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
            if(!tBackWaiting) {
                tBackWaiting=true;
                tBackTimeLeft=UnityEngine.Random.Range(3.0f, 5.0f);
                return;
            }
            if(tBackTimeLeft<=0f) {
                teacherTurnBack();
                tTurnWaiting=false;
            }
            else {
                tBackTimeLeft-=Time.deltaTime;
            }
        }
    }
    public void getCaught() {
        teacher.animator.SetBool("caught", true);
    }
    // Start is called before the first frame update
    void Start()
    {
        teacher=GameObject.Find("OldTeacher").GetComponent<TeacherMovement>();
        eating=new Action(KeyCode.E, 3, 3, 1);
        sleeping=new Action(KeyCode.Z, 5, 2, 2);
        timeLeft=60.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        teacherRandomlyTurnAround();
        if(!updateTimer()) {
            Debug.Log("Time's up");
            return;
        }
        if(Input.GetKey(KeyCode.R)) {
            teacherTurnBack();
        }
        if(Input.GetKey(KeyCode.T)) {
            teacherTurnAround();
        }
        if(eating.check())
            return;
        if(sleeping.check())
            return;
    }
}
