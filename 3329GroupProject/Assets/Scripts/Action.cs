using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Action : MonoBehaviour {
    MainControl control;
    PlayerAnimation pAnim;
    KeyCode actionKey;
    string clip;
    float holdFor;
    float cd;
    float xOffset, yOffset;
    int marks;
    bool isCooling=false;
    bool finish=true;
    bool acting=false;
    DateTime startTime;
    DateTime cdStartTime;
    Transform player;

    public Action(string clip, KeyCode actionKey, float holdFor, float cd, int marks, float xOffest=0f, float yOffset=0f) {
        this.clip=clip;
        this.actionKey=actionKey;
        this.holdFor=holdFor;
        this.cd=cd;
        this.marks=marks;
        this.xOffset=xOffest;
        this.yOffset=yOffset;
        pAnim=GameObject.Find("Player").GetComponent<PlayerAnimation>();
        control=GameObject.Find("MainControl").GetComponent<MainControl>();
        player=GameObject.Find("Player").transform;
    }

    private void startCooldown() {
        isCooling=true;
        cdStartTime=DateTime.Now;
    }

    private bool teacherTurnedAround() {
        return control.teacherTurnedAround();
    }
    private void act(string clip="Player_Writing") {
        if(clip!=this.clip) {
            pAnim.act(clip, -xOffset, -yOffset);
        }
        else {
            pAnim.act(clip, xOffset, yOffset);
        }
    }

    public bool check() {
        if(Input.GetKey(actionKey) && !isCooling) {
            finish=false;
            if(!acting) {
                act(clip);
            }
            acting=true;
            if(control.teacherTurnedAround()) {
                Debug.Log("Get caught: "+actionKey);
                control.getCaught();
                startCooldown();
                startTime=DateTime.Now;
                finish=true;
            }
            else if((DateTime.Now-startTime).TotalSeconds>=holdFor) {
                act();
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
                act();
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
