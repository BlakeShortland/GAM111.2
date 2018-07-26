using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] GameObject fadeImageObject;

	[SerializeField] Slider redSlider;
	[SerializeField] Slider greenSlider;
	[SerializeField] Slider blueSlider;

	[SerializeField] Text redSliderValueText;
	[SerializeField] Text greenSliderValueText;
	[SerializeField] Text blueSliderValueText;

	Image fadeImage;

	bool fading = true;

	void Awake ()
	{
		AssignObjects();
	}

	void Start()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			GameController.colorToSet = new Color32((byte)0, (byte)255, (byte)0, (byte)255);
		}

		if (SceneManager.GetActiveScene().name == "CharacterCreator")
		{
			SetUpSliders();
		}
	}

	void Update ()
	{
		if (UsingFadeIn())
		{
			FadeFromBlack();
		}
	}

	void SetUpSliders()
	{
		redSlider.onValueChanged.AddListener(delegate { SetPlayerColor(); });
		greenSlider.onValueChanged.AddListener(delegate { SetPlayerColor(); });
		blueSlider.onValueChanged.AddListener(delegate { SetPlayerColor(); });

		SetPlayerColor();
	}

	void AssignObjects()
	{
		if (UsingFadeIn())
		{
			fadeImageObject = GameObject.Find("FadeFromBlackContainer");
			fadeImage = fadeImageObject.GetComponentInChildren<Image>();
		}
	}

	void FadeFromBlack()
	{
		if (fadeImageObject.transform.GetChild(0).gameObject.activeInHierarchy == false)
			fadeImageObject.transform.GetChild(0).gameObject.SetActive(true);

		fadeImage = fadeImageObject.GetComponentInChildren<Image>();

		if (fading == true)
		{
			fadeImage.CrossFadeAlpha(1, 3.0f, false);
			fading = false;	
		}
		if (fading == false)
		{
			fadeImage.CrossFadeAlpha(0, 3.0f, false);
		}
	}

	public void ChangeSceneToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ChangeSceneToCharacterCreator()
	{
		SceneManager.LoadScene("CharacterCreator");
	}

	public void ChangeSceneToGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SetPlayerColor()
	{
		GameController.colorToSet = new Color32((byte) redSlider.value, (byte) greenSlider.value, (byte) blueSlider.value, (byte) 255);

		redSliderValueText.text = (" " + redSlider.value.ToString());
		greenSliderValueText.text = (" " + greenSlider.value.ToString());
		blueSliderValueText.text = (" " + blueSlider.value.ToString());
	}

	public bool UsingFadeIn()
	{
		if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "BattleScene")
			return true;
		else
			return false;
	}
}
