using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator, cAnim;
    Transform classmateBR;
    Vector3 original, cOriginal, cOffset;

    // Start is called before the first frame update
    void Start() {
        animator=GetComponent<Animator>();
        classmateBR=GameObject.Find("ClassmateBR").transform;
        cAnim=GameObject.Find("ClassmateBR").GetComponent<Animator>();
        original=transform.position;
        cOriginal=GameObject.Find("ClassmateBR").transform.position;
        cOffset=new Vector3(0.11f, 0.24f, 0);
    }
    public void act(string clip="Player_Writing", float xOffset=0f, float yOffset=0f) {
        animator.Play(clip);
        if(clip=="Player_Writing") {
            transform.position=original;
            cAnim.Play("ClassmateBR_Writing");
            classmateBR.position=cOriginal;
        }
        else {
            transform.position+=new Vector3(xOffset, yOffset, 0);
            if(clip=="Player_Talking") {
                cAnim.Play("ClassmateBR_Talking");
                classmateBR.position+=cOffset;
            }
        }
    }
}
