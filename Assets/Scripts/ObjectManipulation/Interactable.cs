using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        SetModeNormal();
    }
    public void SetModeHoover()
    {
        _outline.enabled = true;
    }

    public void SetModeNormal()
    {
        _outline.enabled = false;
    }
}
