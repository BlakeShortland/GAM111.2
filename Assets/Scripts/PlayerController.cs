using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	Material myMaterial; // Stores my material

	GameObject target; // Stores the target postion for the player to move to.

	void Start ()
	{
		GetComponents();

		SetMyColor();
	}
	
	void Update ()
	{
		MoveToTarget(); // Runs my MoveToTarget function.

		GameController.playerTransform.position = transform.position;
	}

	void GetComponents()
	{
		myMaterial = GetComponent<Renderer>().material;
	}

	//Set my color to the custom color defined in the Game Controller
	void SetMyColor()
	{
		myMaterial.color = GameController.colorToSet;
	}

	//Move to the target using the navmeshagent
	void MoveToTarget()
	{
		if (GameObject.Find("PlayerTarget(Clone)") != null)
		{
			target = GameObject.Find("PlayerTarget(Clone)");
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
			agent.destination = target.transform.position;
		}
	}

	//Destroy the target when the player reaches the target
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "PlayerTarget(Clone)")
		{
			Destroy(target);
		}
	}
}
