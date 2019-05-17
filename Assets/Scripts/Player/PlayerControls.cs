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
    [SerializeField] float _mouseSensivity = 250.0f;
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

    int footstepTimer = 0;
    int FOOTSTEP_RATE = 60;

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
    }
    #endregion

    private void Start()
    {
        SetModeLightOn();
        if (_camera == null) Debug.LogError("NO CAMERA ATTACHED TO PLAYER");
        if (_lightSpot == null) Debug.LogError("NO LIGHT POINT ATTACHED TO PLAYER");
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

        if (_moveVector.x != 0.0f || _moveVector.y != 0.0f) Move();
        else
        {
            if (_isFinishedWalk)
            {
                //CORENTIN ARRETE DE MARCHER
                footstepTimer = 0;
                _isFinishedWalk = false;
            }

        }

        Rotation();
    }

    void Move()
    {
        if (!_isFinishedWalk)
        {
            _isFinishedWalk = true;
        }

        transform.position += transform.forward * Time.deltaTime * _speed * _moveVector.y;
        transform.position += transform.right * Time.deltaTime * _speed * _moveVector.x;

        FootStepSound();
    }

    void Rotation()
    {
        rotX += Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, CLAMP_ANGLE_MIN_Y, CLAMP_ANGLE_MAX_Y);

        if(_camera != null) _camera.transform.rotation = Quaternion.Euler(-rotY, rotX, 0f);
        transform.rotation = Quaternion.Euler(0f, rotX, 0f);
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
        _lightSpot.enabled = false;
    }

    public void SetModeLightOff()
    {
        _lightSpot.enabled = true;
    }

    void FootStepSound()
    {
        footstepTimer++;

        if(footstepTimer >= FOOTSTEP_RATE)
        {
            //CORENTIN FOOTSTEPS
            footstepTimer = 0;
        }
    }
}
