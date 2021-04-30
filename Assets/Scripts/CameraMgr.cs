using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public GameObject ball;
    Vector3 offsetFromBall;

    // Start is called before the first frame update
    void Start()
    {
        offsetFromBall = new Vector3(0, 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //points forward vector of the camera at the ball at an offset
        transform.LookAt(ball.transform.position + offsetFromBall);
        //NOTE FOR VR: won't allow you to look left and right, only up and down
    }
}
