using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider pCol)
    {
        if (pCol.GetComponent<Interactable>()) pCol.GetComponent<Interactable>().SetModeHoover();
    }

    private void OnTriggerExit(Collider pCol)
    {
        if (pCol.GetComponent<Interactable>()) pCol.GetComponent<Interactable>().SetModeNormal();
    }
}
