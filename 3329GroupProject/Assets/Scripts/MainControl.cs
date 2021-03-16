using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainControl : MonoBehaviour
{
    private Action eating, sleeping;
    public int marks=0;
    public int timeLeft=60;
    public float turnAroundDelay=1.0f;
    public TeacherMovement teacher;

    void startTimer(int timeLimit) {
        timeLeft=timeLimit;
        InvokeRepeating("countdown", 0.1f, timeLimit);
    }
    void countdown() {
        timeLeft--;
    }
    void teacherTurnAround() {
        teacher.preTurnAround=true;
        teacher.animator.SetBool("preTurnAround", true);
        teacher.Invoke("turnAround", turnAroundDelay);
    }

    public bool teacherTurnedAround() {
        return teacher.animator.GetBool("turnAround");
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
        startTimer(60);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKey(KeyCode.T)) {
            teacherTurnAround();
        }
        if(Input.GetKey(KeyCode.R)) {
            teacher.animator.SetBool("turnAround", false);
            teacher.animator.SetBool("caught", false);
            teacher.preTurnAround=false;
        }
        if(timeLeft<=0) {
            Debug.Log("Time's up");
            return;
        }
        if(eating.check())
            return;
        if(sleeping.check())
            return;
    }
}
