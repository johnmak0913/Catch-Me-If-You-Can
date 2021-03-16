using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherMovement : MonoBehaviour
{
    float speed = 1f;
    Vector3 rightBoundary = new Vector3(4.91f, 1.06f, 0);
    Vector3 leftBoundary = new Vector3(0.44f, 3.59f, 0);
    Vector3 targetPosition;
    public Animator animator;
    public bool preTurnAround=false;
    // Start is called before the first frame update

    void turnAround() {
        animator.SetBool("preTurnAround", false);
        animator.SetBool("turnAround", true);
    }

    void Start()
    {
        targetPosition = rightBoundary;
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("turnAround") || !animator.GetCurrentAnimatorStateInfo(0).IsName("OldTeacher_Walk") || preTurnAround) {
            return;
        }
        if(transform.position == rightBoundary)
        {
            targetPosition = leftBoundary;
        }
        else if (transform.position == leftBoundary)
        {
            targetPosition = rightBoundary;
        }
        float step = speed * Time.deltaTime;
        //Walk
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
