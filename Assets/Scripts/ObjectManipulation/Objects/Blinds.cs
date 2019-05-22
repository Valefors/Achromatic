using System.Collections;
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
        if(interactionText == "") _interactionName = Utils.CLOSE_INTERACTION;
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

        AkSoundEngine.PostEvent("Play_StoreOuvre", gameObject);
    }

    void CloseBlinds()
    {
        _isOpen = false;
        if (_animator != null) _animator.SetBool("isOpen", _isOpen);

        AkSoundEngine.PostEvent("Play_StoreFerme", gameObject);
    }

    public void OnOpenAnimationEnd()
    {
        if (!_isOpen) return;

        //PuzzleManager.instance.OpenBlinds();
        PlayerControls.instance.SetModeLightOn();
        _interactionName = Utils.CLOSE_INTERACTION;   
    }

    public void OnCloseAnimationEnd()
    {
        if (_isOpen) return;

        //PuzzleManager.instance.CloseBlinds();
        PlayerControls.instance.SetModeLightOff();
        _interactionName = Utils.OPEN_INTERACTION;
    }

    public void SwitchLightOn()
    {
        if (!_isOpen) return;

        PuzzleManager.instance.OpenBlinds();
        StartCoroutine(StaticFunctions.ChangeLightSettings(null, Utils.lightColor, Utils.TURN_ON_LIGHT_DELAY, 1));
    }

    public void SwitchLightOff()
    {
        if (_isOpen) return;

        //PuzzleManager.instance.OpenBlinds();
        PuzzleManager.instance.CloseBlinds();
        StartCoroutine(StaticFunctions.ChangeLightSettings(null, new Color(0,0,0), Utils.TURN_OFF_LIGHT_DELAY, 0));
    }
}
