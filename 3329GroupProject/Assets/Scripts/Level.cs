using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float timeLimit, timeLeft;
    public int lMarks,  marks=0;
    public TeacherMovement teacher;
    public string teacherName;
    private GameObject teacherObj, teacherPf;
    public float tTurnDelay, tTurnPeriodStart, tTurnPeriodEnd;
    public int displayTime;
    private float counter=0.0f;
    public bool started=false;
    public Level(string teacher, float timeLimit, int lMarks, float tTurnDelay, float tTurnPeriodStart, float tTurnPeriodEnd) {
        teacherName=teacher;
        teacherPf=Resources.Load<GameObject>("Prefabs/"+teacher);
        this.timeLimit=timeLeft=timeLimit;
        displayTime=(int)timeLimit;
        this.lMarks=lMarks;
        this.tTurnDelay=tTurnDelay;
        this.tTurnPeriodStart = tTurnPeriodStart;
        this.tTurnPeriodEnd = tTurnPeriodEnd;
    }
    public void prepareTeacher() {
        teacherObj=Instantiate(teacherPf, new Vector3(4.91f, 1.06f, 0f), Quaternion.identity);
        this.teacher=teacherObj.GetComponent<TeacherMovement>();
    }
    public void start() {
        Time.timeScale = 1f;  // For restarting game
        started=true;
    }
    public void removeTeacher() {
        Destroy(teacherObj);
    }
    public bool updateTimer() {
        timeLeft-=Time.deltaTime;
        counter+=Time.deltaTime;
        if(counter>=1.0f) {
            counter--;
            displayTime--;
        }
        return displayTime>=0;
    }
}
