using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Key : MovableInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHolding) HoldingMode();
    }

    public override void PutObject(PutInteractable pFreeSpace)
    {
        transform.position = pFreeSpace.spawnPosition.position;
        putLocation = pFreeSpace;
        putLocation.gameObject.SetActive(false);

        _isHolding = false;
    }

    public void Unlock()
    {
        Destroy(gameObject);
    }
}
