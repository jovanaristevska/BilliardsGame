using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset; //the distance from the cueball, that we want the camera to be
    [SerializeField] float downAngle; //za clicking issues
    [SerializeField] float power; //how hard we are hitting the cue ball
    private float horizontalInput;


    Transform cueBall;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<Ball>().IsCueBall())
            {
                cueBall = ball.transform;
                break;
            }
        }

        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (cueBall != null)
        {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }

        //Temporary 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
        //End Temporary

        if (Input.GetButtonDown("Fire1"))
        {

        }
    }


    public void ResetCamera()
    {
        transform.position = cueBall.position + offset; //reset the camera's position back to the cueball's position and add offset
        transform.LookAt(cueBall.position); //is going to look athe the cue ball
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }
}