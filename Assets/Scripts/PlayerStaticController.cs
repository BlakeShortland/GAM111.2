using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticController : MonoBehaviour
{
	//This script sets the colour of the static non functioning player model in the background of the chacracter customisation scene. It's done the same way as the real player in the player controller
	
	Material myMaterial;

	void Start()
	{
		GetComponents();
	}

	void Update ()
	{
		SetMyColor();
	}

	void GetComponents()
	{
		myMaterial = GetComponent<Renderer>().material;
	}

	void SetMyColor()
	{
		myMaterial.color = GameController.colorToSet;
	}
}
