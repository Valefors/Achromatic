using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Typewritter : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
        _interactionName = Utils.TYPEWRITTER_INTERACTION;
    }

    // Update is called once per frame
    public override void SetInteractionMode()
    {
        UIManager.instance.OnTypewritterScreen();
        SetModeNormal();
    }
}