using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameSetup : MonoBehaviour
{
    int redBallsRemaining = 7;
    int yellowBallsRemaining = 7;
    float ballRadius;
    float ballDiametar;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    //
    private List<GameObject> balls = new List<GameObject>();

    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 150f;
        ballDiametar = ballRadius * 2f;
        PlaceAllBalls();
        ////
        //DisableBallPhysics(); // Disable physics initially
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
        ////
        //balls.Add(ball); // Add the ball to the list
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
        ////
        //balls.Add(ball); // Add the ball to the list
    }

    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        ////
        //// Add a small spacing offset (e.g., 5% of the ball diameter)
        //float spacingOffset = ballDiametar * 0.05f; // Adjust this value to control the spacing
        //float totalSpacing = ballDiametar + spacingOffset;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
            ////
            //balls.Add(ball); // Add the ball to the list
        }

        void PlaceYellowBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            yellowBallsRemaining--;
            ////
            //balls.Add(ball); // Add the ball to the list
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < NumInThisRow; j++)
            {
                //Check to see if it's the middle spot where the 8 ball goes
                if (i == 2 && j == 1)
                {
                    PlaceEightBall(currentPosition);
                }
                //If there are red and yellow balls remaining, randomly choose one and place it
                else if (redBallsRemaining > 0 && yellowBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPosition);
                    }
                    else
                    {
                        PlaceYellowBall(currentPosition);
                    }
                }
                //If only red balls remaining are left, place one
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPosition);
                }
                //Otherwise, place a yellow ball
                else
                {
                    PlaceYellowBall(currentPosition);
                }

                //Move the current position for the next ball in tis row to the right
                //currentPosition += new Vector3(totalSpacing, 0, 0); // No need to normalize here
                currentPosition += new Vector3(1, 0, 0).normalized * ballDiametar;

            }

            // Once all the balls in the row have been placed, move to the next row
            //firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) / 2f * totalSpacing); // Vertical spacing
            //firstInRowPosition += Vector3.left * (totalSpacing / 2f); // Offset for the next row
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPosition = firstInRowPosition;
            NumInThisRow++;

        }

    }

    //void DisableBallPhysics()
    //{
    //    foreach (var ball in balls)
    //    {
    //        Rigidbody rb = ball.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.isKinematic = true; // Disable physics
    //        }
    //    }
    //}

    //public void EnableBallPhysics()
    //{
    //    foreach (var ball in balls)
    //    {
    //        Rigidbody rb = ball.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.isKinematic = false; // Enable physics
    //        }
    //    }
    //}
}

