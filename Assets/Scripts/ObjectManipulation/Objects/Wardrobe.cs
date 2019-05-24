using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Animator))]
public class Wardrobe : Interactable
{
    Animator _animator;
    bool _isFirstOpen = false;
    BoxCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (interactionLabel == "") _interactionName = Utils.OPEN_INTERACTION;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    public override void SetInteractionMode()
    {
        if (_isFirstOpen) return;

        base.SetInteractionMode();

        if (!_isFirstOpen)
        {
            _isFirstOpen = true;
            OpenWardrobe();
            SetModeNormal();
            Destroy(this.GetComponent<Wardrobe>());
            Destroy(this.GetComponent<Outline>());

            if (_collider != null) _collider.enabled = false;
        }
        
    }

    void OpenWardrobe()
    {
        if (_animator != null) _animator.SetBool("IsOpen", true);
        //CORENTIN : OUVRE ARMOIRE
    }
}
