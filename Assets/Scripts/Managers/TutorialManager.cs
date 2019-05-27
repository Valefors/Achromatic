using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region Singleton
    public static TutorialManager instance {
        get { return _instance; }
    }

    private static TutorialManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Debug.LogError("AN INSTANCE ALREADY EXISTS");

    }

    #endregion

    [SerializeField] Image _tutorialImage;
    [SerializeField] Sprite[] _imageArray;

    public bool firstStep = false;
    public bool secondStep = false;
    public bool thridStep = false;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialImage.gameObject.SetActive(false);
        Invoke("ShowMoveStep", 0.5f);
    }

    void ShowMoveStep()
    {
        _tutorialImage.gameObject.SetActive(true);
        if (Utils.LANGUAGE == Enums.ELanguage.FRENCH) _tutorialImage.sprite = _imageArray[0];
        if(Utils.LANGUAGE == Enums.ELanguage.ENGLISH) _tutorialImage.sprite = _imageArray[1];
    }

    public void ShowInteractionStep()
    {
        secondStep = true;
        _tutorialImage.gameObject.SetActive(true);
        _tutorialImage.sprite = _imageArray[2];
    }

    public void HideTutorielStep()
    {
        _tutorialImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
