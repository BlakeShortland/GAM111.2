using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] GameObject playerPrefab;

	GameObject player;

	[SerializeField] Vector3 startPosition = new Vector3(9, 1, 7);

	void Start ()
	{
		//If there is no playerTransform defined, define it and then spawn the player at the starting position.
		if (GameController.playerTransform == null)
		{
			GameController.playerTransform = playerPrefab.transform;
			Instantiate(playerPrefab, startPosition, Quaternion.identity);
		}
		//Otherwise instantiate at the last position
		else
			Instantiate(playerPrefab, GameController.playerTransform.position, Quaternion.identity);

		player = playerPrefab;

		FindPlayer();
	}

	public void FindPlayer()
	{
		CameraController.playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Sets the player transform by finding the object with the tag "player".
	}
}
