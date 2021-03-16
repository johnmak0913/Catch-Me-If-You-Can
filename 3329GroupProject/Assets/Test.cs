using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        animator.SetBool("turnAround", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T)) {
            animator.SetBool("turnAround", true);
        }
        if(Input.GetKey(KeyCode.R)) {
            animator.SetBool("turnAround", false);
        }
    }
}
