using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    //public Interactable selectedObject = null;
    [SerializeField] Text _objectSelectedText;
    [SerializeField] Text _objectInteractionText;

    Image _imageComponent;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _hooverSprite;

    /*public bool isDetecting = false;
    bool _isManipulating = false;*/
    private Player _player; // The Rewired Player
    bool _rightClick = false;
    bool _leftClick = false;

    Interactable _lookedObject;

    #region Singleton
    public static CrossHair instance {
        get { return _instance; }
    }

    static CrossHair _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

    private void Start()
    {
        _player = ReInput.players.GetPlayer(0);

        _imageComponent = GetComponent<Image>();
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";

        //EventManager.StartListening(EventManager.START_HOLDING_EVENT, SetHoldingMode);
        //EventManager.StartListening(EventManager.END_HOLDING_EVENT, SetNormalMode);
        Check();
    }

    void Check()
    {
        if(_objectSelectedText == null) Debug.LogError("MISSING TEXT REFERENCE IN " + this);
        if (_objectInteractionText == null) Debug.LogError("MISSING TEXT REFERENCE IN " + this);
        if (_imageComponent == null) Debug.LogError("MISSING IMAGE COMPONENT IN " + this);
        if (_normalSprite == null || _hooverSprite == null) Debug.LogError("MISSING SPRITE REFERENCE IN " + this);
    }

    // Update is called once per frame
    void Update()
    {
        //if(!_isManipulating) ObjectDetection();
        GetInput();
        ProcessInput();
        ObjectDetection();
    }

    void GetInput()
    {
        _leftClick = Input.GetMouseButtonDown(0);
        _rightClick = Input.GetMouseButtonDown(1);
    }

    void ProcessInput()
    {
        if (_leftClick)
        {
            if (_lookedObject == null) return;

            if(_lookedObject.GetComponent<RotatingInteractable>())
            _lookedObject = null;
            HideCursor();

            EventParam e = new EventParam();
            
            EventManager.TriggerEvent(EventManager.CLICK_ON_OBJECT_EVENT, e);
        }
    }

    void ObjectDetection()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, Camera.main.transform.forward * 5, Color.red);

        if (Physics.Raycast(ray, out rayHit, 5))
        {
            if (rayHit.collider.gameObject.GetComponent<Interactable>())
            {
                if (rayHit.collider.gameObject.GetComponent<Interactable>() != _lookedObject)
                {
                    SetHooverMode(rayHit.collider.gameObject.GetComponent<Interactable>());
                }
                return;
            }

            else SetNormalMode();
        }

        SetNormalMode();
    }

    void SetHooverMode(Interactable pObject)
    {
        _lookedObject = pObject;
        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = _lookedObject.name;
        _objectInteractionText.text = _lookedObject.INTERACTION_NAME;

        //TO-DO REFAIRE EVENTS
        EventParam e = new EventParam();
        e.lookedObject = _lookedObject;

        EventManager.TriggerEvent(EventManager.HOOVER_EVENT, e);
    }

    void SetNormalMode()
    {
        _lookedObject = null;
        _imageComponent.sprite = _normalSprite;
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";

        EventParam e = new EventParam();
        EventManager.TriggerEvent(EventManager.END_HOOVER_EVENT, e);
    }

    void HideCursor()
    {
        _imageComponent.sprite = _normalSprite;
        _imageComponent.gameObject.SetActive(false);
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";
        _objectSelectedText.gameObject.SetActive(false);
        _objectInteractionText.gameObject.SetActive(false);
        print("reé");
    }

    public void ShowCursor()
    {
        _imageComponent.gameObject.SetActive(true);
        _objectSelectedText.gameObject.SetActive(true);
        _objectInteractionText.gameObject.SetActive(true);
    }

    /*public void OnClick()
    {
        if(selectedObject.GetComponent<InteractableProps>()) EventManager.TriggerEvent(EventManager.START_HOLDING_EVENT);
        selectedObject.SetInteractionMode();
    }*/

    /*void ObjectDetection()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, Camera.main.transform.forward * 50000000, Color.red);

        if (Physics.Raycast(ray,out rayHit, 50000000))
        {
            if (rayHit.collider.gameObject.GetComponent<Interactable>())
            {
                if (selectedObject == null) selectedObject = rayHit.collider.gameObject.GetComponent<Interactable>();

                if (rayHit.collider.gameObject != selectedObject.gameObject)
                {
                    selectedObject.SetModeNormal();
                    selectedObject = rayHit.collider.gameObject.GetComponent<Interactable>();

                }

                SetHoover();
            }

            if (rayHit.collider.gameObject.GetComponent<Interactable>() == null)
            {
                if (selectedObject != null) SetObjectNormal();

                _imageComponent.sprite = _normalSprite;
                _objectSelectedText.text = "";
                isDetecting = false;

                return;
            }
        }
    }

    void SetHoover()
    {
        selectedObject.SetModeHoover();

        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = selectedObject.transform.name;

        isDetecting = true;
    }

    void SetObjectNormal()
    {
        selectedObject.SetModeNormal();
        selectedObject = null;
    }

    public void SetHoldingMode()
    {
        _isManipulating = true;
        isDetecting = false;

        _imageComponent.sprite = _normalSprite;
        _objectSelectedText.text = "";
        _imageComponent.enabled = false;
    }

    public void SetNormalMode()
    {
        _isManipulating = false;
        selectedObject.SetModeNormal();

        selectedObject = null;

        _imageComponent.enabled = true;
    }*/

    private void OnDestroy()
    {
        //EventManager.StopListening(EventManager.MANIPULATION_EVENT, SetHoldingMode);
        //EventManager.StopListening(EventManager.END_HOLDING_EVENT, SetNormalMode);
    }
}
