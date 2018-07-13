using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	Material myMaterial;

	bool isDead = false;

	GameObject gameController;

	void Start ()
	{
		GetComponents();
	}
	
	void Update ()
	{
		DeadCheck();

		RayCast();
	}

	void RayCast ()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		{
			if (hit.transform.tag == "Player")
			{
				GameController.battleMode = true;
			}
		}

		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
	}

	void DeadCheck ()
	{
		if(isDead)
		{
			myMaterial.color = Color.grey;
			return;
		}
		myMaterial.color = Color.red;
	}

	void GetComponents ()
	{
		myMaterial = GetComponent<Renderer>().material;
	}
}
