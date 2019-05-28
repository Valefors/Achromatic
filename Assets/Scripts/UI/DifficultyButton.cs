using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] string _difficultySentence = "";
    [SerializeField] Text _sentence;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _sentence.text = _difficultySentence;
    }

    public void OnExit()
    {
        _sentence.text = "";
    }
}
