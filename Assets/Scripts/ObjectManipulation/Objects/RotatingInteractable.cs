using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(rotateObject))]
[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(BoxCollider))]
public class RotatingInteractable : Interactable
{
    rotateObject _roScript;
    bool _rightClick = false;
    bool _leftClick = false;
    bool _isManipulate = false;
    bool _isFirstClick = false;

    Vector3 _originalPosition;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _roScript = GetComponent<rotateObject>();
        _originalPosition = transform.position;

        _interactionName = Utils.ROTATING_OBJECT_INTERACTION;
    }

    private void Update()
    {
        if (!_isManipulate) return;
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        _rightClick = _player.GetButton(Utils.RIGHT_CLICK_ACTION);
        _leftClick = Input.GetMouseButtonDown(0);
    }

    void ProcessInput()
    {
        if (_rightClick) _roScript.SetManipulationMode();
        else _roScript.SetNormalMode();

        if (_leftClick) PutBack();
    }

    public override void SetInteractionMode()
    {
        ManipulationMode();
    }

    void ManipulationMode()
    {
        transform.position = PlayerControls.instance.manipulationPosition.position;
        _isManipulate = true;
    }

    void PutBack()
    {
        _roScript.SetNormalMode();
        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;

        _isManipulate = false;

        InteractableManager.instance.StopRotatingInteraction();
    }
}
