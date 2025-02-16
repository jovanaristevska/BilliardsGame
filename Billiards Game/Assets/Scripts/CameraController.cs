using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset; //the distance from the cueball, that we want the camera to be
    [SerializeField] float downAngle; //za clicking issues
    [SerializeField] float power; //how hard we are hitting the cue ball
    [SerializeField] GameObject cueStick;
    private float horizontalInput;


    //OTPOSLE
    private bool isTakingShot = false;
    [SerializeField] float maxDrawDistance;
    private float savedMousePosition; 





    Transform cueBall;
    GameManager gameManager;

    [SerializeField] TextMeshProUGUI powerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

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
        if (cueBall != null && !isTakingShot)
        {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
        //Shoot();

        //Temp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
        //End
        Shoot(); 

        //OVA DA SE IZBRISHI TREBA
        //if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<Camera>().enabled)
        //{
        //    Vector3 hitDirection = transform.forward;
        //    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

        //    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power, ForceMode.Impulse);
        //    cueStick.SetActive(false);
        //    gameManager.SwitchCameras();
        //}
    }


    public void ResetCamera()
    {
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset; //reset the camera's position back to the cueball's position and add offset
        transform.LookAt(cueBall.position); //is going to look athe the cue ball
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    void Shoot()
    {
        if (gameObject.GetComponent<Camera>().enabled)
        {
            if (Input.GetButtonDown("Fire1") && !isTakingShot)
            {
                isTakingShot = true;
                savedMousePosition = 0f;
            }
            else if (isTakingShot)
            {
                if (savedMousePosition + Input.GetAxis("Mouse Y") <= 0)
                {
                    savedMousePosition += Input.GetAxis("Mouse Y");
                    if (savedMousePosition <= maxDrawDistance)
                    {
                        savedMousePosition = maxDrawDistance;
                    }
                    float powerValueNumber = ((savedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerValueInt = Mathf.RoundToInt(powerValueNumber);
                    powerText.text = "Power: " + powerValueInt + "%";
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

                    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power * Mathf.Abs(savedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    gameManager.SwitchCameras();
                    isTakingShot = false;

                 
                    powerText.text = "Power: 0%"; // Update UI accordingly
                }
            }
        }
    }
}