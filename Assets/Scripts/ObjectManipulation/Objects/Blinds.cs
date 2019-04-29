using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Blinds : Interactable
{
    bool _isOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        _interactionName = Utils.CLOSE_INTERACTION;
    }

    public override void SetInteractionMode()
    {
        base.SetInteractionMode();

        if (_isOpen) CloseBlinds();
        else OpenBlinds();
    }

    void OpenBlinds()
    {
        _isOpen = true;
        PuzzleManager.instance.OpenBlinds();
        PlayerControls.instance.SetModeLightOn();
        _interactionName = Utils.CLOSE_INTERACTION;
    }

    void CloseBlinds()
    {
        _isOpen = false;
        PuzzleManager.instance.CloseBlinds();
        PlayerControls.instance.SetModeLightOff();
        _interactionName = Utils.OPEN_INTERACTION;
    }
}
