using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableProps : Interactable
{
    bool _isManipulate = false;
    bool _isHolding = false;
    Vector3 _originalPosition;

    rotateObject _roScript;

    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position;
        _roScript = GetComponent<rotateObject>();

        EventManager.StartListening(EventManager.MANIPULATION_EVENT, SetManipulationMode);
        EventManager.StartListening(EventManager.END_MANIPULATION_EVENT, SetEndManipulationMode);

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHolding) HoldingnMode();
    }

    public override void SetModeNormal()
    {
        base.SetModeNormal();

        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;
        _roScript.SetNormalMode();

        _isHolding = false;
    }

    void SetManipulationMode()
    {
        if(_isHolding) _roScript.SetManipulationMode();
    }

    void SetEndManipulationMode()
    {
        if (_isHolding) _roScript.SetNormalMode();
    }

    public override void SetInteractionMode()
    {
        base.SetInteractionMode();
        _isHolding = true;
    }

    void HoldingnMode()
    {
        transform.position = PlayerController.instance.spawnPosition.position;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventManager.MANIPULATION_EVENT, SetManipulationMode);
        EventManager.StopListening(EventManager.END_MANIPULATION_EVENT, SetEndManipulationMode);
    }
}
