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
    public bool _withObject = false;

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
        EventManager.StartListening(EventManager.START_HOLDING_EVENT, Manipulation);
        EventManager.StartListening(EventManager.END_HOLDING_EVENT, SetModeNormal);
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
            if (!_withObject)
            {
                if (!CrossHair.instance.isDetecting) return; //Check if we can take an object
                CrossHair.instance.OnClick();
            }

            else EventManager.TriggerEvent(EventManager.END_HOLDING_EVENT);
        }

        if (Input.GetMouseButton(1))
        {
            _isManipulating = true;
            EventManager.TriggerEvent(EventManager.MANIPULATION_EVENT);
        }
        else
        {
            _isManipulating = false;
            EventManager.TriggerEvent(EventManager.END_MANIPULATION_EVENT);
        }

        if (!_isManipulating)
        {
            Move();
            CameraRotation();
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

    void SetModeInteraction()
    {
        _withObject = true;
    }

    void Manipulation()
    {
        _withObject = true;
        _isManipulating = true;
    }

    void SetModeNormal()
    {
        _withObject = false;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventManager.START_HOLDING_EVENT, Manipulation);
        EventManager.StopListening(EventManager.END_HOLDING_EVENT, SetModeNormal);
    }
}
