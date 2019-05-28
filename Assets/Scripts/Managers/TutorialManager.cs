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

    [HideInInspector] public int indexStep = 0;

    [SerializeField] Image _optionsImage;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialImage.gameObject.SetActive(false);
        Invoke("ShowMoveStep", 0.5f);
        Invoke("DisableOptionsImage", 15f);
    }

    void ShowMoveStep()
    {
        if (Utils.LANGUAGE == Enums.ELanguage.FRENCH) _tutorialImage.sprite = _imageArray[indexStep];
        if(Utils.LANGUAGE == Enums.ELanguage.ENGLISH) _tutorialImage.sprite = _imageArray[indexStep + 1];

        ShowImage();
    }

    public void UpdateIndex()
    {
        indexStep += 1;
    }

    public void ShowInteractionStep()
    {
        _tutorialImage.sprite = _imageArray[indexStep + 1];

        ShowImage();
    }

    public void ShowRotationStep()
    {
        _tutorialImage.sprite = _imageArray[indexStep + 1];

        ShowImage();
    }

    void ShowImage()
    {
        _tutorialImage.gameObject.SetActive(true);
        StartCoroutine(StaticFunctions.FadeInAlpha(result => _tutorialImage.color = result, _tutorialImage.color, 1f));
    }

    public void HideTutorielStep()
    {
        StartCoroutine(StaticFunctions.FadeOutAlpha(result => _tutorialImage.color = result, _tutorialImage.color, 1f, DisableImage));
    }

    void DisableImage()
    {
        _tutorialImage.gameObject.SetActive(false);
    }

    //Options images
    public void DisableOptionsImage()
    {
        _optionsImage.gameObject.SetActive(false);
    }
}
