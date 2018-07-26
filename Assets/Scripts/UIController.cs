using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] GameObject fadeImageObject;

	Image fadeImage;

	bool fading = true;

	void Awake ()
	{
		AssignObjects();
	}
	
	void Update ()
	{
		if (SceneManager.GetActiveScene().name == "BattleScene")
		{
			FadeFromBlack();
		}
	}

	void AssignObjects()
	{
		fadeImageObject = GameObject.Find("FadeFromBlackContainer");
		fadeImage = fadeImageObject.GetComponentInChildren<Image>();
	}

	void FadeFromBlack()
	{
		if (fadeImageObject.transform.GetChild(0).gameObject.activeInHierarchy == false)
			fadeImageObject.transform.GetChild(0).gameObject.SetActive(true);

		fadeImage = fadeImageObject.GetComponentInChildren<Image>();

		if (fading == true)
		{
			fadeImage.CrossFadeAlpha(1, 2.0f, false);
			fading = false;	
		}
		if (fading == false)
		{
			fadeImage.CrossFadeAlpha(0, 2.0f, false);
		}
	}
}
