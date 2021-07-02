using UnityEngine;

[RequireComponent(typeof(Player_Motor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 6f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring setting:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;



    private Player_Motor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        motor = GetComponent<Player_Motor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSetting(jointSpring);
    }
    void Update()
    {
        //caculate the movement as a 3D vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;
        //final movement vector
        Vector3 _veloctiy = (_moveHorizontal + _moveVertical).normalized * speed;

        //apply movement
        motor.Move(_veloctiy);


        //caculate rotaion as a 3D vector(for looking around)
        float _yRotaion = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRotaion, 0f) * lookSensitivity;

        //apply rotation
        motor.Rotate(_rotation);




        //caculate camaera rotaion as a 3D vector(for looking around)
        float _xRotaion = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX =_xRotaion * lookSensitivity;

        //apply rotation
        motor.RotateCamera(_cameraRotationX);

        //caculate the thrusther force based on player input
        Vector3 _thrustherForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrustherForce = Vector3.up * thrusterForce;
            SetJointSetting(0f);
        }
        else
        {
            SetJointSetting(jointSpring);
        }

        //apply the trusther force
        motor.ApplyThruster(_thrustherForce);

    }

    private void SetJointSetting(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce 
        };

    }
}
