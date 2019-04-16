using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInteractable : Interactable
{
    public Transform spawnPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        _interactionName = Utils.PUT_INTERACTION;
        if (spawnPosition == null) Debug.LogError("NO SPAWN POSITION IN " + this);
    }
}
