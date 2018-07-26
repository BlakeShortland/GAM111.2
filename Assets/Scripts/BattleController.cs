﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<GameController>().SendBattleData();
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

		playersTurn = false;
	}

	IEnumerator EnemysTurn()
	{
		while (enemyMadeMove != true)
			yield return null;

		playersTurn = true;
	}

	public void Attack ()
	{
		if (playersTurn)
		{
			enemyHealth -= playerDamage;

			playerMadeMove = true;
		}
		else
		{
			playerHealth -= enemyDamage;

			enemyMadeMove = true;
		}
	}

	public void Defend()
	{
		if (playersTurn)
		{
			if (enemyDamage > 1)
				enemyDamage -= 1;

			playerMadeMove = true;
		}
		else
		{
			if (playerDamage > 1)
				playerDamage -= 1;

			enemyMadeMove = true;
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
		}
		else
		{
			if (enemyHealthPotions > 0)
			{
				enemyHealth += GameController.maxSkillPointsPerSkill - enemyHealth;
				enemyHealthPotions--;
			}

			enemyMadeMove = true;
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
