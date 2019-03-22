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

    void ObjectDetection()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out rayHit, 500))
        {
            if (rayHit.collider.gameObject.GetComponent<Interactable>() == null)
            {
                if (selectedObject != null) SetObjectNormal();

                _imageComponent.sprite = _normalSprite;
                _objectSelectedText.text = "";
                isDetecting = false;

                return;
            }

            if (selectedObject == null) SetHooverObject(rayHit.collider.gameObject.GetComponent<Interactable>());

            if (rayHit.collider.gameObject != selectedObject.gameObject)
            {
                selectedObject.SetModeNormal();
                SetHooverObject(rayHit.collider.gameObject.GetComponent<Interactable>());
            }      
        }
    }

    void SetHooverObject(Interactable pObject)
    {
        selectedObject = pObject;
        selectedObject.SetModeHoover();

        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = pObject.transform.name;
        isDetecting = true;
    }

    void SetObjectNormal()
    {
        selectedObject.SetModeNormal();
        selectedObject = null;
        isDetecting = false;
    }

    public void SetManipulationMode()
    {
        _isManipulating = true;

        selectedObject.SetManipulationMode();

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
}
