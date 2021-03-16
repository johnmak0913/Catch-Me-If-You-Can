using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action : MonoBehaviour {
    MainControl control;
    KeyCode actionKey;
    float holdFor;
    float cd;
    int marks;
    bool isCooling=false;
    bool finish=true;
    bool acting=false;
    DateTime startTime;
    DateTime cdStartTime;

    public Action(KeyCode actionKey, float holdFor, float cd, int marks) {
        this.actionKey=actionKey;
        this.holdFor=holdFor;
        this.cd=cd;
        this.marks=marks;
        control=GameObject.Find("MainControl").GetComponent<MainControl>();
    }

    private void startCooldown() {
        isCooling=true;
        cdStartTime=DateTime.Now;
    }

    private bool teacherTurnedAround() {
        return control.teacherTurnedAround();
    }

    public bool check() {
        if(Input.GetKey(actionKey) && !isCooling) {
            finish=false;
            acting=true;
            if(control.teacherTurnedAround()) {
                Debug.Log("Get caught: "+actionKey);
                control.getCaught();
                startCooldown();
                startTime=DateTime.Now;
                finish=true;
            }
            else if((DateTime.Now-startTime).TotalSeconds>=holdFor) {
                Debug.Log("Action finished: "+actionKey);
                control.marks+=marks;
                Debug.Log("Marks: "+control.marks);
                startCooldown();
                startTime=DateTime.Now;
                finish=true;
            }
        }
        else {
            acting=false;
            startTime=DateTime.Now;
            if(!finish) {
                Debug.Log("Action unfinshed: "+actionKey);
                startCooldown();
                finish=true;
            }
        }
        if(isCooling) {
            if((DateTime.Now-cdStartTime).TotalSeconds>=cd) {
                Debug.Log("Cooled down: "+actionKey);
                isCooling=false;
            }
        }
        return acting;
    }
}
