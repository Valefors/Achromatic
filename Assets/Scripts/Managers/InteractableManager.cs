using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    //Faire un get
    [HideInInspector] public Interactable hooverObject = null;
    [HideInInspector] public MovableInteractable holdingObject = null;

    [SerializeField] MovableInteractable[] _movableObjects;

    #region Singleton
    public static InteractableManager instance {
        get { return _instance; }
    }

    static InteractableManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");
    }
    #endregion

    public void CheckObjectToSetInteraction()
    {
        if (hooverObject == null) return;

        if (hooverObject.GetComponent<MovableInteractable>()) DoMovableInteraction();

        else if (hooverObject.GetComponent<PutInteractable>()) DoPutInteraction(hooverObject.GetComponent<PutInteractable>());

        else if (hooverObject.GetComponent<Lock>()) DoLockInteraction();

        else if (hooverObject.GetComponent<RotatingInteractable>())
        {
            if (holdingObject != null)
            {
                CrossHair.instance.SetRefuseText();
                return;
            }

            DoRotatingInteraction();
        }

        else
        {
            if (holdingObject != null)
            {
                CrossHair.instance.SetRefuseText();
                return;
            }

            hooverObject.SetInteractionMode();
            //CORENTIN: RIEN A PRENDRE
        }

        hooverObject = null;
    }

    public void SetObjectHooverMode(Interactable pObject)
    {
        if (hooverObject != null) hooverObject.SetModeNormal();   

        hooverObject = pObject;
        hooverObject.SetModeHoover();
    }

    public void SetObjectNormalMode()
    {
        if(hooverObject != null)
        {
            hooverObject.SetModeNormal();
            hooverObject = null;
        }
    }

    void DoRotatingInteraction()
    {
        hooverObject.SetInteractionMode();
        CrossHair.instance.HideCursor();
        PlayerControls.instance.SetManipulationMode();
    }

    public void StopRotatingInteraction()
    {
        PlayerControls.instance.SetNormalMode();
        CrossHair.instance.ShowCursor();
    }

    void DoMovableInteraction()
    {
        if (holdingObject == null) TakeObject();
        else SwitchObject();

    }

    void DoPutInteraction(PutInteractable pFreeSpace)
    {
        holdingObject.PutObject(pFreeSpace);
        holdingObject = null;
    }

    void DoLockInteraction()
    {
        if (!holdingObject.GetComponent<Key>()) return;

        holdingObject.GetComponent<Key>().Unlock();
        hooverObject.GetComponent<Lock>().Unlock();

        holdingObject = null;
    }

    void TakeObject(MovableInteractable pObject = null)
    {
        if (pObject == null) holdingObject = hooverObject.GetComponent<MovableInteractable>();
        else holdingObject = pObject;

        holdingObject.SetInteractionMode();
    }

    void SwitchObject()
    {
        MovableInteractable lSwitchObject = hooverObject.GetComponent<MovableInteractable>();

        holdingObject.PutObject(lSwitchObject.putLocation);
        lSwitchObject.putLocation = null;
        TakeObject(lSwitchObject);
    }

    public void DisableMovableInteraction()
    {
        for(int i = 0; i < _movableObjects.Length; i++)
        {
            Destroy(_movableObjects[i].GetComponent<MovableInteractable>());
        }
    }
}
