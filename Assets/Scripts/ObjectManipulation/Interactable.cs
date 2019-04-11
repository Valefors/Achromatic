using Rewired;
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
        EventManager.StartListening(EventManager.HOOVER_EVENT, SetModeHoover);
        EventManager.StartListening(EventManager.END_HOOVER_EVENT, SetModeNormal);
        EventManager.StartListening(EventManager.CLICK_ON_OBJECT_EVENT, SetInteractionMode);

        _outline = GetComponent<Outline>();
        if (_outline == null) Debug.LogError("NO OUTLINE SCRIPT IN " + this);

        _outline.enabled = false;
        _player = ReInput.players.GetPlayer(0);
    }

    public void SetModeHoover(EventParam e)
    {
        if (e.lookedObject == this)
        {
            _outline.enabled = true;
            _isHoover = true;
        }
    }

    public virtual void SetModeNormal(EventParam e)
    {
        if (_isHoover)
        {
            _isHoover = false;
            _outline.enabled = false;
        }
    }

    public virtual void SetInteractionMode(EventParam e)
    {
        if (!_isHoover) return;
        print("do stuff");
    }

    private void OnDestroy()
    {
        Destroy();
    }

    protected virtual void Destroy()
    {
        EventManager.StopListening(EventManager.HOOVER_EVENT, SetModeHoover);
        EventManager.StopListening(EventManager.END_HOOVER_EVENT, SetModeHoover);
        EventManager.StopListening(EventManager.CLICK_ON_OBJECT_EVENT, SetInteractionMode);
    }

}
