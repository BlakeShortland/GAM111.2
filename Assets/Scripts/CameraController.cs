using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	Transform player; // Stores the player transform.
	Vector3 playerPosition; // Stores the player vector.
	Camera mainCamera; // Stores the camera component of the main camera.

	[SerializeField]
	GameObject playerTarget; // Stores the player target prefab.

	GameObject oldTarget;


	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform; // Sets the player transform by finding the object with the tag "player".

		mainCamera = GetComponent<Camera>(); // Gets the camera component.
	}
	
	void Update ()
	{
		MatchPlayerPosition(); // Run the Match Target Position function.

		CheckInputs();
	}

	void MatchPlayerPosition ()
	{
		if (player != null)
		{
			playerPosition = new Vector3(player.position.x, player.position.y + 10, player.position.z + 2); // Sets the target position 10 units above the player and 2 units down screen from the player.
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, 100f); // Moves the camera towards the start position.
			mainCamera.depth = player.transform.position.y + 4; // Changes the "height" of the camear depending on what level the player is on.
			return;
		}
		Debug.Log("Designate the player game object by tagging it as player.");
	}

	void CheckInputs ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ClearOldTarget();
			SetPlayerTarget();
		}
	}

	void SetPlayerTarget ()
	{
		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit))
		{
			if(playerTarget != null)
			{
				Instantiate(playerTarget, hit.point, transform.rotation);
			}
			else
			{
				Debug.Log("Set player target prefab");
			}
		}
	}

	void ClearOldTarget ()
	{
		oldTarget = GameObject.Find("PlayerTarget(Clone)");

		Destroy(oldTarget);
	}
}
