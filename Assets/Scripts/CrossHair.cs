﻿using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] Text _objectSelectedText;
    [SerializeField] Text _objectInteractionText;

    Image _imageComponent;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _hooverSprite;

    private Player _player; // The Rewired Player
    bool _rightClick = false;
    bool _leftClick = false;

    public bool isHolding = false;
    public MovableInteractable holdingObject = null;

    Interactable _lookedObject;
    bool _isRefusing = false;

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
            InteractableManager.instance.CheckObjectToSetInteraction();
            /*if (_lookedObject == null) return;

            if(!(_lookedObject.GetComponent<PutInteractable>() || _lookedObject.GetComponent<MovableInteractable>()) && isHolding)
            {
                _objectInteractionText.text = Utils.OTHER_HOLDING_INTERACTION;
                return;
            }

            EventParam e = new EventParam();
            e.lookedObject = _lookedObject;
            _lookedObject = null;

            EventManager.TriggerEvent(EventManager.CLICK_ON_OBJECT_EVENT, e);*/
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
                if (InteractableManager.instance.holdingObject == null
                    && rayHit.collider.gameObject.GetComponent<PutInteractable>()) return;

                InteractableManager.instance.SetObjectHooverMode(rayHit.collider.gameObject.GetComponent<Interactable>());
                SetHooverMode();

                return;
            }

            else SetNormalMode();
        }

        SetNormalMode();
    }

    void SetHooverMode()
    {
        //_lookedObject = pObject;
        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = InteractableManager.instance.hooverObject.name;
        if (!_isRefusing) _objectInteractionText.text = InteractableManager.instance.hooverObject.INTERACTION_NAME;
    }

    void SetNormalMode()
    {
        //_lookedObject = null;
        _imageComponent.sprite = _normalSprite;
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";

        InteractableManager.instance.SetObjectNormalMode();
        //EventParam e = new EventParam();
        //EventManager.TriggerEvent(EventManager.END_HOOVER_EVENT, e);
    }

    public void HideCursor()
    {
        _imageComponent.sprite = _normalSprite;
        _imageComponent.gameObject.SetActive(false);
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";
        _objectSelectedText.gameObject.SetActive(false);
        _objectInteractionText.gameObject.SetActive(false);
    }

    public void ShowCursor()
    {
        _imageComponent.gameObject.SetActive(true);
        _objectSelectedText.gameObject.SetActive(true);
        _objectInteractionText.gameObject.SetActive(true);
    }

    public void SetRefuseText()
    {
        StartCoroutine(RefuseCoroutine());
    }

    IEnumerator RefuseCoroutine()
    {
        _isRefusing = true;
        _objectInteractionText.text = Utils.OTHER_HOLDING_INTERACTION;
        
        yield return new WaitForSeconds(1.2f);
        _isRefusing = false;
    }
}
