using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Player _player; // The Rewired Player

    //Movement
    Vector2 _moveVector;
    [SerializeField] float _speed = 5f;

    //Rotation
    float rotX = 0;
    float rotY = 0;
    //[SerializeField] float _mouseSensivity = Utils.MOUSE_SENSIBILITY;
    [SerializeField] float CLAMP_ANGLE_MIN_Y = -50f;
    [SerializeField] float CLAMP_ANGLE_MAX_Y = 50f;

    //Position for rotating objects
    public Transform manipulationPosition;
    [HideInInspector] public bool _isManipulating = false;

    //Position for movable objects
    public Transform holdingPoint;

    //Camera
    [SerializeField] Camera _camera;

    [SerializeField] Light _lightSpot;

    [SerializeField] Rigidbody _rb;

    int footstepTimer = 0;
    int FOOTSTEP_RATE = 60;
    float previousMouseX = 0;

    //Angular velocity
    Quaternion _lastRotation;
    float _rotationSpeed = 0;

    bool _isFinishedWalk = true;

    #region Singleton
    public static PlayerControls instance {
        get { return _instance; }
    }

    static PlayerControls _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");

        _player = ReInput.players.GetPlayer(0);
        _rb = transform.parent.GetComponent<Rigidbody>();
    }
    #endregion

    private void Start()
    {
        SetModeLightOn();
        if (_camera == null) Debug.LogError("NO CAMERA ATTACHED TO PLAYER");
        if (_lightSpot == null) Debug.LogError("NO LIGHT POINT ATTACHED TO PLAYER");
        if (_rb == null) Debug.LogError("NO Rigidbody ATTACHED TO PLAYER");

        _lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        _moveVector.x = _player.GetAxis(Utils.REWIRED_MOVE_HORIZONTAL_ACTION);
        _moveVector.y = _player.GetAxis(Utils.REWIRED_MOVE_VERTICAL_ACTION);
    }

    void ProcessInput()
    {
        if (UIManager.instance.onUI) return;

        if (_isManipulating) return;

        Rotation();

        if (_moveVector.x != 0.0f || _moveVector.y != 0.0f) Move();
        else
        {
            if (_isFinishedWalk)
            {
                //CORENTIN ARRETE DE MARCHER
                AkSoundEngine.PostEvent("Player_Stop", gameObject);
                footstepTimer = 0;
                _isFinishedWalk = false;
            }

        }
    }

    void Move()
    {
        if(TutorialManager.instance.indexStep == 0)
        {
            TutorialManager.instance.HideTutorielStep();
            TutorialManager.instance.UpdateIndex();
        }

        if (!_isFinishedWalk)
        {
            _isFinishedWalk = true;
            print("pas");
        }

        // transform.position += transform.forward * Time.deltaTime * _speed * _moveVector.y;
        // transform.position += transform.right * Time.deltaTime * _speed * _moveVector.x;

        Vector3 speed = new Vector3();
        speed += transform.forward * _speed * _moveVector.y;
        speed += transform.right * _speed * _moveVector.x;
        _rb.velocity = speed;
    }

    void HideFTUE()
    {
        TutorialManager.instance.HideTutorielStep();
        TutorialManager.instance.UpdateIndex();
    }

    void Rotation()
    {
        CalculateRotationSpeed();

        rotX += Input.GetAxis("Mouse X") * Utils.MOUSE_SENSIBILITY * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * Utils.MOUSE_SENSIBILITY * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, CLAMP_ANGLE_MIN_Y, CLAMP_ANGLE_MAX_Y);

        if(_camera != null)
            _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.Euler(-rotY,0f, 0f), 0.5f); 

        transform.localRotation =Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, rotX, 0f), 0.5f);
        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, rotX, 0f); 
    }

    void CalculateRotationSpeed()
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(_lastRotation);
        _lastRotation = transform.rotation;

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;

        deltaRotation.ToAngleAxis(out angle, out axis);
        angle *= Mathf.Deg2Rad;

        Vector3 lAngularVelocity = axis * angle * (1.0f / Time.deltaTime);
        _rotationSpeed = Mathf.Abs(lAngularVelocity.y);

        //CORENTIN LA VITESSE C EST _ROTATIONSPEED
        //print(_rotationSpeed);
    }

    public void SetManipulationMode()
    {
        _isManipulating = true;
    }

    public void SetNormalMode()
    {
        _isManipulating = false;
    }

    public void SetModeLightOn()
    {
        //_lightSpot.enabled = false;
        StartCoroutine(StaticFunctions.ChangeLightSettings(_lightSpot, Utils.lightOff, Utils.TURN_OFF_LIGHT_DELAY));
    }

    public void SetModeLightOff()
    {
        //_lightSpot.enabled = true;
        StartCoroutine(StaticFunctions.ChangeLightSettings(_lightSpot, Utils.lightColor, Utils.TURN_ON_LIGHT_DELAY - 10));
    }

}
