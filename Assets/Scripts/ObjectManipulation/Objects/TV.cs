using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class TV : Interactable
{
    bool _isTurnOn = true;
    Shader _screenShader;
    Shader _screenOffSahder;
    [SerializeField] Renderer _screenRenderer;
    [SerializeField] Light _televisionLight;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (interactionText == "") _interactionName = Utils.TURN_OFF_INTERACTION;
        if (_screenRenderer == null) Debug.LogError("NO MATERIAL LINKED IN " + this);
        if (_televisionLight == null) Debug.LogError("NO LIGHT LINKED IN " + this);

        _screenShader = Shader.Find("CustomOutline");
        _screenOffSahder = Shader.Find("Simple Double-Sided");
        print(_screenShader);
        print(_screenOffSahder);
        //_originalScreenMaterial = _screenMaterial;
    }

    public override void SetInteractionMode()
    {
        base.SetInteractionMode();

        if (_isTurnOn) TurnOff();
        else TurnOn();
    }

    void TurnOff()
    {
        _isTurnOn = false;
        _screenRenderer.material.shader = _screenOffSahder;
        _televisionLight.enabled = false;
        _interactionName = Utils.TURN_ON_INTERACTION;
        //_screenMaterial = _screenOffMaterial;
        //CORENTIN COUPER SON ICI
    }

    void TurnOn()
    {
        _isTurnOn = true;
        _screenRenderer.material.shader = _screenShader;
        _televisionLight.enabled = true;
        _interactionName = Utils.TURN_OFF_INTERACTION;
        //_screenMaterial = _originalScreenMaterial;
        //CORENTIN METTRE SON ICI
    }
}
