using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherWalk : MonoBehaviour
{
    float speed = 1f;
    Vector3 rightBoundary = new Vector3(4.91f, 1.06f, 0);
    Vector3 leftBoundary = new Vector3(0.44f, 3.59f, 0);
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = rightBoundary;
    }

    // Update is called once per frame
    void Update()
    {
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
