using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticController : MonoBehaviour
{
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
