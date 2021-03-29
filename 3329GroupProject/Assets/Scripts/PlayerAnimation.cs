using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Vector3 original;
    // Start is called before the first frame update
    void Start() {
        animator=GetComponent<Animator>();
        original=transform.position;
    }
    public void act(string clip="Player_Writing", float xOffset=0f, float yOffset=0f) {
        animator.Play(clip);
        if(clip=="Player_Writing") {
            transform.position=original;
        }
        else {
            transform.position+=new Vector3(xOffset, yOffset, 0);
        }
    }
}
