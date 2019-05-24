using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Outline _outline;
    protected bool _isHoover = false;
    protected Player _player;

    [HideInInspector] public string INTERACTION_NAME {
        get { return _interactionName; }
    }

    protected string _interactionName = "";

    [SerializeField] protected string interactionLabel = "";

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _outline = GetComponent<Outline>();

        if (_outline != null)
        {
            _outline.enabled = false;
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
        }

        _player = ReInput.players.GetPlayer(0);

        if (interactionLabel != "") _interactionName = ReadJSON.GetCorrectTextInteraction(interactionLabel);
        OptionScreen.OnUpdate += UpdateInteractionName;
    }

    public virtual void SetModeHoover()
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

    void UpdateInteractionName()
    {
        _interactionName = ReadJSON.GetCorrectTextInteraction(interactionLabel);
    }

    private void OnDestroy()
    {
        Destroy();
    }

    protected virtual void Destroy()
    {

    }

}
