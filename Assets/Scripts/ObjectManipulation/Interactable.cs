﻿using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Outline _outline;
    protected bool _isHoover = false;
    protected Player _player;

    [HideInInspector] public string INTERACTION_NAME {
        get { return _interactionName; }
    }

    protected string _interactionName = "";

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _outline = GetComponent<Outline>();
        if (_outline == null) Debug.LogError("NO OUTLINE SCRIPT IN " + this);

        _outline.enabled = false;
        _player = ReInput.players.GetPlayer(0);
    }

    public void SetModeHoover()
    {
        if (_outline != null) _outline.enabled = true;
    }

    public virtual void SetModeNormal()
    {
        if(_outline != null) _outline.enabled = false;
    }

    public virtual void SetInteractionMode()
    {

    }

    private void OnDestroy()
    {
        Destroy();
    }

    protected virtual void Destroy()
    {

    }

}
