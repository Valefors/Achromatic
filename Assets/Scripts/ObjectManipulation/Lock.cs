using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    protected override void Init()
    {
        base.Init();
        _interactionName = Utils.UNLOCK_INTERACTION;
    }
    public void Unlock()
    {
        print("unlock");
        Destroy(GetComponent<Lock>());
    }
}
