using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInteractable : Interactable
{
    public Transform spawnPosition;
    GameObject _phantomObject = null;

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

    public override void SetModeHoover()
    {
        base.SetModeHoover();
        SpawnHoldObject();
    }

    void SpawnHoldObject()
    {
        if (!InteractableManager.instance.holdingObject) return;

        _phantomObject = Instantiate(InteractableManager.instance.holdingObject, spawnPosition.position, Quaternion.identity).gameObject;
        Destroy(_phantomObject.GetComponent<Interactable>());
        Destroy(_phantomObject.GetComponent<Outline>());

        _phantomObject.GetComponent<Renderer>().material.SetFloat("_Transparency", 0.6f);
        StartCoroutine(GlowCoroutine());
    }

    public override void SetModeNormal()
    {
        base.SetModeNormal();
        StopCoroutine(GlowCoroutine());
        _phantomObject.GetComponent<Renderer>().material.SetFloat("_Transparency", 1f);
        Destroy(_phantomObject);
    }

    IEnumerator GlowCoroutine()
    {
        float value = 0.6f;
        float gap = 0.01f;

        while (true)
        {
            if(value >= 0.8f)
            {
                value = 0.8f;
                gap *= -1;
            }

            if(value <= 0.2f)
            {
                value = 0.2f;
                gap *= -1;
            }

            value += gap;
            _phantomObject.GetComponent<Renderer>().material.SetFloat("_Transparency", value);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
