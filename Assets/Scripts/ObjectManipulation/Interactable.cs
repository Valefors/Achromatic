using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Outline _outline;

    void Start()
    {
        Init();
        //EventManager.StartListening(EventManager.MANIPULATION_EVENT, SetManipulationMode);
    }

    protected virtual void Init()
    {
        _outline = GetComponent<Outline>();
        if (_outline == null) Debug.LogError("NO OUTLINE SCRIPT IN " + this);
        SetModeNormal();
    }

    public void SetModeHoover()
    {
        _outline.enabled = true;
    }

    public virtual void SetModeNormal()
    {
        _outline.enabled = false;
    }

    public virtual void SetInteractionMode()
    {

    }

}
