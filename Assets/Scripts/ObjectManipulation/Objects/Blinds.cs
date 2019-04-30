﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Blinds : Interactable
{
    bool _isOpen = true;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        _interactionName = Utils.CLOSE_INTERACTION;
        _animator = GetComponent<Animator>();
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
        if (_animator != null) _animator.SetBool("isOpen", _isOpen);

        PuzzleManager.instance.OpenBlinds();
        PlayerControls.instance.SetModeLightOn();
        _interactionName = Utils.CLOSE_INTERACTION;

        AkSoundEngine.PostEvent("Play_StoreOuvre", gameObject);
    }

    void CloseBlinds()
    {
        _isOpen = false;
        if (_animator != null) _animator.SetBool("isOpen", _isOpen);

        PuzzleManager.instance.CloseBlinds();
        PlayerControls.instance.SetModeLightOff();
        _interactionName = Utils.OPEN_INTERACTION;

        AkSoundEngine.PostEvent("Play_StoreFerme", gameObject);
    }
}
