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


    private void Awake()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 150f;
        ballDiametar = ballRadius * 2f;
        PlaceAllBalls();

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
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;


        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }

        void PlaceYellowBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            yellowBallsRemaining--;
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < NumInThisRow; j++)
            {
               
                if (i == 2 && j == 1)
                {
                    PlaceEightBall(currentPosition);
                }
                
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
               
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPosition);
                }
                
                else
                {
                    PlaceYellowBall(currentPosition);
                }

                currentPosition += new Vector3(1, 0, 0).normalized * ballDiametar;

            }

           
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPosition = firstInRowPosition;
            NumInThisRow++;

        }

    }
}

