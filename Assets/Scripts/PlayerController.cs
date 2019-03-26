using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _mouseSensivity = 250.0f;

    float rotX = 0;
    float rotY = 0;

    float _speed = 5f;
    float posX = 0;
    float posZ = 0;

    [SerializeField] float CLAMP_ANGLE_Y = 30f;
    public Transform spawnPosition;

    public bool _isManipulating = false;

    #region Singleton
    public static PlayerController instance {
        get { return _instance; }
    }

    static PlayerController _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CursorLock();
        EventManager.StartListening(EventManager.MANIPULATION_EVENT, Manipulation);
        EventManager.StartListening(EventManager.END_MANIPULATION_EVENT, SetModeNormal);
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CrossHair.instance.isDetecting) return;
            CrossHair.instance.OnClick();
            //CrossHair.instance.SetManipulationMode();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!_isManipulating) return;

            EventManager.TriggerEvent(EventManager.END_MANIPULATION_EVENT);
            //CrossHair.instance.SetNormalMode();
        }

        if (!_isManipulating)
        {
            CameraRotation();
            Move();
        }
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * _speed * Input.GetAxis("Vertical");
        transform.position += transform.right * Time.deltaTime * _speed * Input.GetAxis("Horizontal");
    }

    void CameraRotation()
    {
        rotX += Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -CLAMP_ANGLE_Y, CLAMP_ANGLE_Y);

        transform.rotation = Quaternion.Euler(-rotY, rotX, 0f);
    }

    void Manipulation()
    {
        _isManipulating = true;
    }

    void SetModeNormal()
    {
        _isManipulating = false;
    }
}
