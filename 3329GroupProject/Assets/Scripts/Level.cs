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
    public float tTurnDelay, tTurnPeriod;
    public int displayTime;
    private float counter=0.0f;
    // Start is called before the first frame update

    public Level(string teacher, float timeLimit, int lMarks, float tTurnDelay, float tTurnPeriod) {
        teacherName=teacher;
        teacherPf=Resources.Load<GameObject>("Prefabs/"+teacher);
        this.timeLimit=timeLeft=timeLimit;
        displayTime=(int)timeLimit;
        this.lMarks=lMarks;
        this.tTurnDelay=tTurnDelay;
        this.tTurnPeriod=tTurnPeriod;
    }
    public void start() {
        teacherObj=Instantiate(teacherPf, new Vector3(4.91f, 1.06f, 0f), Quaternion.identity);
        this.teacher=teacherObj.GetComponent<TeacherMovement>();
        Time.timeScale = 1f;  // For restarting game
    }
    public void end() {
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
