using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
	//Player stats
	public static int playerHealth;
	public static int playerDamage;
	public static int playerSpeed;
	public static int playerHealthPotions;

	//Enemy stats
	public static int enemyHealth;
	public static int enemyDamage;
	public static int enemySpeed;
	public static int enemyHealthPotions;

	//Game controller container
	GameObject gameController;

	//Booleans for managing the turn system
	public static bool playersTurn;
	bool playerMadeMove = false;
	bool enemyMadeMove = false;
	bool gamePlaying = true;

	void Awake()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<GameController>().SendBattleDataToBattleController();
	}

	void Start ()
	{
		CheckWhoseTurn();

		StartCoroutine(GameLoop());
	}

	//This function detirmins who goes first
	void CheckWhoseTurn()
	{
		if (playerSpeed >= enemySpeed)
			playersTurn = true;
		else
			playersTurn = false;
	}

	//This IEnumerator was made to replace the Update function, because I believed my game was freezing when the lower coroutines were yielding. It turned out to be a UI element that was blocking my clicks, but this IEnumerator still works seemingly as well.
	IEnumerator GameLoop ()
	{
		while (gamePlaying)
		{
			if (playersTurn)
				StartCoroutine(PlayersTurn());
			else
				StartCoroutine(EnemysTurn());

			DeadCheck();

			//Wait 2 seconds before continuing to allow speech pop ups to be readable
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

		EnemyBattleAI();

		while (enemyMadeMove != true)
			yield return null;

		playersTurn = true;
	}

	//If the player or the enemy is defeated, leave the battle scene
	public void DeadCheck()
	{
		if (playerHealth <= 0)
			SceneManager.LoadScene("MainMenu");
		if (enemyHealth <= 0)
			SceneManager.LoadScene("Game");
	}

	//Depending on whose turn it is, take away the damage from the oposing health
	public void Attack ()
	{
		if (playersTurn)
		{
			enemyHealth -= playerDamage;

			Debug.Log("Player attacking.");

			playerMadeMove = true;
		}
		else
		{
			playerHealth -= enemyDamage;

			Debug.Log("Enemy attacking.");

			enemyMadeMove = true;
		}
	}

	//If someone defends then the opponents damage is reduced for the remainder of the battle
	public void Defend()
	{
		if (playersTurn)
		{
			if (enemyDamage > 1)
				enemyDamage -= 1;

			Debug.Log("Player defending.");

			playerMadeMove = true;
		}
		else
		{
			if (playerDamage > 1)
				playerDamage -= 1;

			Debug.Log("Enemy defending.");

			enemyMadeMove = true;
		}
	}

	//If someone has health potions they can heal up to the maximum possible health.
	public void Heal()
	{
		if (playersTurn)
		{
			if (playerHealthPotions > 0)
			{
				playerHealth += GameController.maxSkillPointsPerSkill - playerHealth;
				playerHealthPotions--;

				Debug.Log("Player healing.");

				playerMadeMove = true;
			}
		}
		else
		{
			if (enemyHealthPotions > 0)
			{
				enemyHealth += GameController.maxSkillPointsPerSkill - enemyHealth;
				enemyHealthPotions--;

				Debug.Log("Enemy healing.");

				enemyMadeMove = true;
			}
		}
	}

	//Detirmines what move the enemy makes using a simulated D20
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

	//If attacking, attack. Otherwise defend
	public void AttackDefend()
	{
		if (Attacking())
			Attack();
		else
			Defend();
	}

	//A boolean to check if the enemy has less than half health. If they do, they are more likely to heal
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
