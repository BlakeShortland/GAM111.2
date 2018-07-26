using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
	public static int playerHealth;
	public static int playerDamage;
	public static int playerSpeed;
	public static int playerHealthPotions;

	public static int enemyHealth;
	public static int enemyDamage;
	public static int enemySpeed;
	public static int enemyHealthPotions;

	GameObject gameController;

	public static bool playersTurn;
	bool playerMadeMove = false;
	bool enemyMadeMove = false;
	bool gamePlaying = true;

	void Awake()
	{
		GetComponents();
	}

	void Start ()
	{
		CheckWhoseTurn();

		StartCoroutine(GameLoop());
	}
	
	void Update ()
	{
		
	}

	void GetComponents ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<GameController>().SendBattleDataToBattleController();
	}

	void CheckWhoseTurn()
	{
		if (playerSpeed >= enemySpeed)
			playersTurn = true;
		else
			playersTurn = false;
	}

	IEnumerator GameLoop ()
	{
		while (gamePlaying)
		{
			if (playersTurn)
				StartCoroutine(PlayersTurn());
			else
				StartCoroutine(EnemysTurn());

			DeadCheck();

			yield return new WaitForSecondsRealtime(2);
		}
	}

	IEnumerator PlayersTurn ()
	{
		playerMadeMove = false;

		while (playerMadeMove != true)
			yield return null;

		playersTurn = false;
	}

	IEnumerator EnemysTurn()
	{
		enemyMadeMove = false;

		while (enemyMadeMove != true)
			yield return null;

		playersTurn = true;
	}

	public void DeadCheck()
	{
		if (playerHealth <= 0)
			SceneManager.LoadScene("MainMenu");
		if (enemyHealth <= 0)
			SceneManager.LoadScene("Game");
	}

	public void Attack ()
	{
		if (playersTurn)
		{
			enemyHealth -= playerDamage;

			playerMadeMove = true;

			Debug.Log("Player attacking.");
		}
		else
		{
			playerHealth -= enemyDamage;

			enemyMadeMove = true;

			Debug.Log("Enemy attacking.");
		}
	}

	public void Defend()
	{
		if (playersTurn)
		{
			if (enemyDamage > 1)
				enemyDamage -= 1;

			playerMadeMove = true;

			Debug.Log("Player defending.");
		}
		else
		{
			if (playerDamage > 1)
				playerDamage -= 1;

			enemyMadeMove = true;

			Debug.Log("Enemy defending.");
		}
	}

	public void Heal()
	{
		if (playersTurn)
		{
			if (playerHealthPotions > 0)
			{
				playerHealth += GameController.maxSkillPointsPerSkill - playerHealth;
				playerHealthPotions--;
			}

			playerMadeMove = true;

			Debug.Log("Player healing.");
		}
		else
		{
			if (enemyHealthPotions > 0)
			{
				enemyHealth += GameController.maxSkillPointsPerSkill - enemyHealth;
				enemyHealthPotions--;
			}

			enemyMadeMove = true;

			Debug.Log("Enemy healing.");
		}
	}

	public void EnemyBattleAI()
	{
		int roll = Random.Range(1, 21);

		if (LowHealth(enemyHealth))
		{
			if (roll > 5)
				Heal();
			else
				AttackDefend();
		}
		else
		{
			if (roll < 5)
				Heal();
			else
				AttackDefend();
		}
	}

	public void AttackDefend()
	{
		if (Attacking())
			Attack();
		else
			Defend();
	}

	bool LowHealth(int health)
	{
		if (health < GameController.maxSkillPointsPerSkill / 2)
			return true;
		else
			return false;
	}

	bool Attacking()
	{
		int roll = Random.Range(0,1);

		if (roll != 0)
			return true;
		else
			return false;
	}
}
