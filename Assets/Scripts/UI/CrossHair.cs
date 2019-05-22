using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] Text _objectSelectedText = null;
    [SerializeField] Text _objectInteractionText = null;

    Image _imageComponent;
    [SerializeField] Sprite _normalSprite = null;
    [SerializeField] Sprite _hooverSprite = null;

    private Player _player; // The Rewired Player
    bool _rightClick = false;
    bool _leftClick = false;

    bool _isRefusing = false;
    bool _isDisplay = true;

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
        }
    }

    void ObjectDetection()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!_isDisplay || UIManager.instance.onUI) return;

        if (Physics.Raycast(ray, out rayHit, Utils.PLAYER_DETECTION))
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
        _imageComponent.sprite = _hooverSprite;
        _objectSelectedText.text = InteractableManager.instance.hooverObject.name;
        if (!_isRefusing) _objectInteractionText.text = InteractableManager.instance.hooverObject.INTERACTION_NAME;
    }

    void SetNormalMode()
    {
        _imageComponent.sprite = _normalSprite;
        _objectSelectedText.text = "";
        _objectInteractionText.text = "";

        InteractableManager.instance.SetObjectNormalMode();
    }

    public void HideCursor()
    {
        _imageComponent.sprite = _normalSprite;
        _imageComponent.gameObject.SetActive(false);
        //_objectSelectedText.text = "";
        //_objectInteractionText.text = "";
        //_objectSelectedText.gameObject.SetActive(false);
        //_objectInteractionText.gameObject.SetActive(false);

        _isDisplay = false;
    }

    public void ShowCursor()
    {
        _imageComponent.gameObject.SetActive(true);
        _objectInteractionText.gameObject.SetActive(true);

        _isDisplay = true;
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
