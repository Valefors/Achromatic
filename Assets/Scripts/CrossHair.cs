using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public Interactable selectedObject = null;
    [SerializeField] Text _objectSelectedText;

    Image _imageComponent;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _hooverSprite;

    public bool isDetecting = false;
    bool _isManipulating = false;

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
        _imageComponent = GetComponent<Image>();
        _objectSelectedText.text = "";

        EventManager.StartListening(EventManager.START_HOLDING_EVENT, SetHoldingMode);
        EventManager.StartListening(EventManager.END_HOLDING_EVENT, SetNormalMode);
        Check();
    }

    void Check()
    {
        if(_objectSelectedText == null) Debug.LogError("MISSING TEXT REFERENCE IN " + this);
        if (_imageComponent == null) Debug.LogError("MISSING IMAGE COMPONENT IN " + this);
        if (_normalSprite == null || _hooverSprite == null) Debug.LogError("MISSING SPRITE REFERENCE IN " + this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isManipulating) ObjectDetection();
    }

    public void OnClick()
    {
        if(selectedObject.GetComponent<InteractableProps>()) EventManager.TriggerEvent(EventManager.START_HOLDING_EVENT);
        selectedObject.SetInteractionMode();
    }

    void ObjectDetection()
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
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventManager.MANIPULATION_EVENT, SetHoldingMode);
        EventManager.StopListening(EventManager.END_HOLDING_EVENT, SetNormalMode);
    }
}
