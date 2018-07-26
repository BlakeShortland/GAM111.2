using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	Material myMaterial;

	bool isDead = false;

	GameObject gameController;

	public NavMeshAgent agent;
	public Transform[] waypoints;
	public float targetThreshold = 0f;
	private int currentWaypointIndex = 0;
	private Transform target;

	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");

		GetComponents();

		if (waypoints.Length != 0)
		{
			//Setup initial target based on the first waypoint
			target = waypoints[0];
		}		
	}
	
	void Update ()
	{
		DeadCheck();

		if(isDead == false)
		{
			RayCast();
			Patrol();
		}
	}

	void RayCast ()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		{
			if (hit.transform.tag == "Player")
			{
				gameController.GetComponent<GameController>().EnterBattleMode();
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

	void Patrol ()
	{
		//Do not patrol if there is more than one waypoint
		if (waypoints.Length != 0)
		{
			agent.SetDestination(target.position);
			//If the enemy has reached the current waypoint, iterate to the next waypoint
			if ((transform.position - target.position).magnitude <= targetThreshold)
			{
				UpdateWaypoint();
			}
		}
	}

	void UpdateWaypoint()
	{
		currentWaypointIndex++;
		if (currentWaypointIndex >= waypoints.Length)
		{
			currentWaypointIndex = 0;
		}

		target = waypoints[currentWaypointIndex];
	}
}
