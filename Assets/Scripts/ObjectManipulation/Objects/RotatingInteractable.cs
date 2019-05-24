using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(rotateObject))]
[RequireComponent(typeof(Outline))]
public class RotatingInteractable : Interactable
{
    rotateObject _roScript;
    //bool _rightClick = false;
    bool _leftClick = false;
    bool _isManipulate = false;
    //bool _isFirstClick = false;

    Vector3 _originalPosition;
    Quaternion _originalRotation;

    public Enums.ERotatingType type = Enums.ERotatingType.NONE;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _roScript = GetComponent<rotateObject>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        if (interactionLabel == "") _interactionName = Utils.ROTATING_OBJECT_INTERACTION;
    }

    private void Update()
    {
        if (!_isManipulate) return;
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        //_rightClick = _player.GetButton(Utils.RIGHT_CLICK_ACTION);
        _leftClick = Input.GetMouseButtonDown(0);
    }

    void ProcessInput()
    {
        _roScript.SetManipulationMode();
        if (_leftClick) PutBack();
    }

    public override void SetInteractionMode()
    {
        ManipulationMode();
    }

    void ManipulationMode()
    {
        PlayCorrectSound(true);
        transform.position = PlayerControls.instance.manipulationPosition.position;
        _isManipulate = true;

        gameObject.layer = Utils.OBJECT_LAYER;
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = Utils.OBJECT_LAYER;
        }
    }

    void PutBack()
    {
        _roScript.SetNormalMode();
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;

        _isManipulate = false;

        gameObject.layer = Utils.DEFAULT_LAYER;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = Utils.DEFAULT_LAYER;
        }

        InteractableManager.instance.StopRotatingInteraction();
        PlayCorrectSound(false);
    }

    void PlayCorrectSound(bool pIsTaking)
    {
        switch (type)
        {
            case Enums.ERotatingType.PAPER:
                if (pIsTaking) print("CORENTIN: PRENDRE FEUILLE");
                else print("CORENTIN: POSER FEUILLE");
                break;

            case Enums.ERotatingType.NONE:
                if (pIsTaking) print("CORENTIN: SON GENERIQUE");
                else print("CORENTIN: POSER GENERIQUE");
                break;
        }
    }
}
