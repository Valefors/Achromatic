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
    private float _mouseSensivity = 250.0f;
    [SerializeField] float CLAMP_ANGLE_Y = 30f;

    //Position for rotating objects
    public Transform manipulationPosition;
    [HideInInspector] public bool _isManipulating = false;

    //Position for movable objects
    public Transform holdingPoint;

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

    void Start()
    {
        CursorLock();
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
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
        if (UIManager.instance.onUI)
        {
            CursorUnlock();
            return;
        }

        else CursorLock();

        if (_isManipulating) return;

        if (_moveVector.x != 0.0f || _moveVector.y != 0.0f) Move();
        Rotation();
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * _speed * _moveVector.y;
        transform.position += transform.right * Time.deltaTime * _speed * _moveVector.x;
    }

    void Rotation()
    {
        rotX += Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -CLAMP_ANGLE_Y, CLAMP_ANGLE_Y);

        transform.rotation = Quaternion.Euler(-rotY, rotX, 0f);
    }

    public void SetManipulationMode()
    {
        _isManipulating = true;
    }

    public void SetNormalMode()
    {
        _isManipulating = false;
    }
}
