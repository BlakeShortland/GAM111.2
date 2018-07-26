using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
	public static int playerHealth;
	public static int playerDamage;
	public static int playerSpeed;

	public static int enemyHealth;
	public static int enemyDamage;
	public static int enemySpeed;

	GameObject gameController;

	bool playersTurn;
	bool playerMadeMove = false;
	bool enemyMadeMove = false;

	void Awake()
	{
		GetComponents();
	}

	void Start ()
	{
		CheckWhoseTurn();
	}
	
	void Update ()
	{
		if (playersTurn)
			StartCoroutine(PlayersTurn());
		else
			StartCoroutine(EnemysTurn());
	}

	void GetComponents ()
	{
		gameController.GetComponent<GameController>().SendBattleData();
		gameController = GameObject.FindGameObjectWithTag("GameController");
	}

	void CheckWhoseTurn()
	{
		if (playerSpeed >= enemySpeed)
			playersTurn = true;
		else
			playersTurn = false;
	}

	IEnumerator PlayersTurn ()
	{
		while (playerMadeMove != true)
			yield return null;
	}

	IEnumerator EnemysTurn()
	{
		while (enemyMadeMove != true)
			yield return null;
	}

	public void Attack ()
	{
		if (playersTurn)
		{

		}
		else
		{

		}
	}

	public void Defend()
	{
		if (playersTurn)
		{

		}
		else
		{

		}
	}

	public void Heal()
	{
		if (playersTurn)
		{

		}
		else
		{

		}
	}
}
