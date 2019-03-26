using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableProps : Interactable
{
    bool _isManipulate = false;
    Vector3 _originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isManipulate) ManipulationMode();
    }

    public override void SetModeNormal()
    {
        base.SetModeNormal();

        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;

        _isManipulate = false;
    }

    public override void SetInteractionMode()
    {
        base.SetInteractionMode();
        _isManipulate = true;

        EventManager.TriggerEvent(EventManager.MANIPULATION_EVENT);

    }

    void ManipulationMode()
    {
        transform.position = PlayerController.instance.spawnPosition.position;
    }
}
