using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Outline _outline;
    bool _isManipulate = false;
    Vector3 _originalPosition;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _originalPosition = transform.position;

        SetModeNormal();
    }

    private void Update()
    {
        if (_isManipulate) ManipulationMode();
    }

    public void SetModeHoover()
    {
        _outline.enabled = true;
    }

    public void SetModeNormal()
    {
        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;

        _outline.enabled = false;
        _isManipulate = false;
    }

    public void SetManipulationMode()
    {
        _isManipulate = true;
    }

    void ManipulationMode()
    {
        transform.position = PlayerController.instance.spawnPosition.position;
    }
}
