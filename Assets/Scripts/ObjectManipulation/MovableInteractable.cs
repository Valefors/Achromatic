using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableInteractable : Interactable
{
    bool _isHolding = false;
    [SerializeField] PutInteractable _putLocation;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _interactionName = Utils.MOVABLE_OBJECT_INTERACTION;
        if (_putLocation == null) Debug.LogError("MISSING REFERENCE IN " + this);
        _putLocation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isHolding) HoldingMode();
    }

    public override void SetInteractionMode(EventParam e)
    {
        if(e.lookedObject == _putLocation)
        {
            PutObject();
            return;
        }
        if (!_isHoover) return;

        SetModeHolding();
        _putLocation.gameObject.SetActive(true);
    }

    void SetModeHolding()
    {
        _isHolding = true;
    }

    void HoldingMode()
    {
        transform.position = PlayerControls.instance.holdingPoint.position;
    }

    void PutObject()
    {
        transform.position = _putLocation.transform.position;
        _isHolding = false;
        _putLocation.gameObject.SetActive(false);
    }
}
