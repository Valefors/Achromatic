using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour 
{
	public  bool isOn = false;

	public Color onColorBg;
	public Color offColorBg;

	public Image toggleBgImage;
	public RectTransform toggle;

	public GameObject handle;
	private RectTransform handleTransform;

    [SerializeField] TextMeshProUGUI _difficultyText;

    private float handleSize;
	private float onPosX;
	private float offPosX;

	float handleOffset = 4;

	float speed = 1;
	static float t = 0.0f;

	private bool switching = false;


	void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 2) - (handleSize/2) - handleOffset;
		offPosX = onPosX * -1;

	}

	void Start()
	{
		if(isOn)
		{
			toggleBgImage.color = onColorBg;
			handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
		}
		else
		{
			toggleBgImage.color = offColorBg;
			handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
		}

        UpdateDifficulty();
	}
		
	void Update()
	{
		if(switching)
		{
			Toggle(isOn);
		}
	}

	public void UpdateDifficulty()
	{
		Debug.Log(isOn);
        //OptionScreen.SetDifficulty(isOn);
	}

	public void Switching()
	{
		switching = true;
	}
		
	public void Toggle(bool toggleStatus)
	{	
		if(toggleStatus)
		{
			toggleBgImage.color = SmoothColor(onColorBg, offColorBg);
			handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
		}
		else 
		{
			toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
			handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
		}	
	}


	Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	{
		
		Vector3 position = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
		StopSwitching();
		return position;
	}

	Color SmoothColor(Color startCol, Color endCol)
	{
		Color resultCol;
		resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
		return resultCol;
	}

	void StopSwitching()
	{
		if(t > 1.0f)
		{
			switching = false;

			t = 0.0f;
			switch(isOn)
			{
			case true:
				isOn = false;
				UpdateDifficulty();
                _difficultyText.text = Utils.EASY_MODE;
				break;

			case false:
				isOn = true;
				UpdateDifficulty();
                 _difficultyText.text = Utils.DIFFICULT_MODE;
				break;
			}

		}
	}

}
