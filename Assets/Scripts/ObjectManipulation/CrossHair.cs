using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Interactable _selectedObject = null;
    [SerializeField] Text _objectSelectedText;

    Image _imageComponent;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _hooverSprite;

    public static CrossHair instance {
        get { return _instance; }
    }

    static CrossHair _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }

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
        ObjectDetection();
    }

    void ObjectDetection()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out rayHit, 500))
        {
            if (rayHit.collider.gameObject.GetComponent<Interactable>() == null)
            {
                if (_selectedObject != null) SetObjectNormal();

                _imageComponent.sprite = _normalSprite;
                _objectSelectedText.text = "";

                return;
            }

            if (_selectedObject == null) SetHooverObject(rayHit.collider.gameObject.GetComponent<Interactable>());

            if (rayHit.collider.gameObject != _selectedObject.gameObject)
            {
                _selectedObject.SetModeNormal();
                SetHooverObject(rayHit.collider.gameObject.GetComponent<Interactable>());
            }      
        }
    }

    void SetHooverObject(Interactable pObject)
    {
        _selectedObject = pObject;
        _selectedObject.SetModeHoover();

        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = pObject.transform.name;
    }

    void SetObjectNormal()
    {
        _selectedObject.SetModeNormal();
        _selectedObject = null;
    }
}
