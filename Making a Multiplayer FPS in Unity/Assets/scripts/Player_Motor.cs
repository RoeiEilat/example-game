using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Motor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 Rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;


    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    //gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        Rotation = _rotation;
    }

    //gets a rotational vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Runs every physics iteration
    void FixedUpdate()
    {
        performMovement();
        PerformRotation();
    }

    //performs movement based on velocity
    void performMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }


    //perform rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Rotation));
        if(cam != null)
        {
            //set our rotaion and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            //apply our rotation to the transform of the camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    //gets a force for our thruster
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }
}
