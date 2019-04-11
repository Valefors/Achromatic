using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInteractable : Interactable
{
    Vector3 putPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        putPosition = transform.position;
        _interactionName = Utils.PUT_INTERACTION;
    }
}
