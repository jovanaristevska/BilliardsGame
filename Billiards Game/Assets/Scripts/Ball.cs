using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool is8Ball = false;
    private bool isCueBall = false;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y > 0)
        {
            Vector3 newVelocity = rb.linearVelocity;
            newVelocity.y = 0f;
            rb.linearVelocity = newVelocity;
        }
    }

    public bool IsBallRed()
    {
        return isRed;
    }

    public bool IsCueBall()
    {
        return isCueBall;
    }

    public bool IsEightBall()
    {
        return is8Ball;
    }

    public void BallSetup(bool red)
    {
        isRed = red;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void MakeCueBall()
    {
        isCueBall = true;
    }

    public void MakeEightBall()
    {
        is8Ball = true;
        GetComponent<Renderer>().material.color = Color.black;
    }
}
