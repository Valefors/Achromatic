using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(BoxCollider))]
public class MovableInteractable : Interactable
{
    protected bool _isHolding = false;
    public PutInteractable putLocation;
    [SerializeField] public PutInteractable correctLocation;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _interactionName = Utils.MOVABLE_OBJECT_INTERACTION;
        if (putLocation == null) Debug.LogError("MISSING REFERENCE IN " + this);
        putLocation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isHolding) HoldingMode();
    }

    public override void SetInteractionMode()
    {
        SetModeHolding(); 
    }

    void SwitchPosition(MovableInteractable pObject)
    {
        transform.position = pObject.transform.position;
        putLocation = pObject.putLocation;
        putLocation.gameObject.SetActive(false);
        _isHolding = false;

        pObject.GetComponent<MovableInteractable>().putLocation = null;
        pObject.GetComponent<MovableInteractable>().SetModeHolding();
    }

    public void SetModeHolding()
    {
        _isHolding = true;
        if(putLocation != null) putLocation.gameObject.SetActive(true);
        putLocation = null;

        AkSoundEngine.PostEvent("Play_Prendre", gameObject);
    }

    protected void HoldingMode()
    {
        transform.position = PlayerControls.instance.holdingPoint.position;
    }

    public virtual void PutObject(PutInteractable pFreeSpace)
    {
        transform.position = pFreeSpace.spawnPosition.position;
        putLocation = pFreeSpace;
        putLocation.SetModeNormal();
        putLocation.gameObject.SetActive(false);

        _isHolding = false;

        AkSoundEngine.PostEvent("Play_Poser", gameObject);

        PuzzleManager.instance.CheckCorrectPosition();
    }
}
