using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Transform myTransform;

	Material myMaterial;

	GameObject target; // Stores the target postion for the player to move to.

	void Start ()
	{
		GetComponents();

		SetMyColor();

		SpawnPosition();
	}
	
	void Update ()
	{
		MoveToTarget(); // Runs my MoveToTarget function.

		GameController.playerTransform = transform;
	}

	void GetComponents()
	{
		myMaterial = GetComponent<Renderer>().material;
	}

	void SetMyColor()
	{
		myMaterial.color = GameController.colorToSet;
	}

	void SpawnPosition ()
	{
		if (GameController.playerTransform == null)
			myTransform = transform;
		else
			myTransform = GameController.playerTransform;
	}

	void MoveToTarget()
	{
		if (GameObject.Find("PlayerTarget(Clone)") != null)
		{
			target = GameObject.Find("PlayerTarget(Clone)");
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
			agent.destination = target.transform.position;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "PlayerTarget(Clone)")
		{
			Debug.Log("hit the target");
			Destroy(target);
		}
	}
}
