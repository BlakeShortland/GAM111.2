using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[System.Serializable] struct Player
	{
		public int health;
		public int damage;
		public int speed;
		public int skillPointsRemaining;

		public Transform playTransform;

		public bool isDead;

		public Color myColor;
	}

	[System.Serializable] struct Enemy
	{
		public int health;
		public int damage;
		public int speed;
		public int skillPointsRemaining;

		public bool isDead;
	}

	[SerializeField] Player playerStruct;
	[SerializeField] Enemy[] enemyStructArray;

	[SerializeField] int skillPointsAvailable = 10;
	[SerializeField] int maxSkillPointsPerSkill = 5;

	public bool battleMode = false;

	public static Color colorToSet;

	int enemyID;

	void Start()
	{
		RandomiseEnemies();
	}

	void Update()
	{

	}

	void RandomiseEnemies()
	{
		int i;

		for (i = 0; i < enemyStructArray.Length; i++)
		{
			enemyStructArray[i].skillPointsRemaining = skillPointsAvailable;

			enemyStructArray[i].health = GiveRandomSkillPoints(enemyStructArray[i].skillPointsRemaining);

			enemyStructArray[i].skillPointsRemaining -= enemyStructArray[i].health;

			enemyStructArray[i].damage = GiveRandomSkillPoints(enemyStructArray[i].skillPointsRemaining);

			enemyStructArray[i].skillPointsRemaining -= enemyStructArray[i].damage;

			enemyStructArray[i].speed = GiveRemainingSkillPoints(enemyStructArray[i].skillPointsRemaining);

			enemyStructArray[i].skillPointsRemaining -= enemyStructArray[i].speed;
		}
	}

	int GiveRandomSkillPoints(int skillPoints)
	{
		int skillPointsUsed;

		skillPointsUsed = Random.Range(1, MaxSkillPointsToApply(skillPoints));

		return skillPointsUsed;
	}

	int MaxSkillPointsToApply(int skillPointsLeft)
	{
		int maxSkillPoints;

		if (skillPointsLeft > maxSkillPointsPerSkill)
			maxSkillPoints = maxSkillPointsPerSkill;
		else
			maxSkillPoints = skillPointsLeft;

		return maxSkillPoints;
	}

	int GiveRemainingSkillPoints(int skillPointsRemaining)
	{
		int skillPointsUsed;

		skillPointsUsed = skillPointsRemaining;

		return skillPointsUsed;
	}

	public void EnterBattleMode(int ID)
	{
		enemyID = ID;

		battleMode = true;

		SceneManager.LoadScene("BattleScene");
	}

	public void ExitBattleMode()
	{
		battleMode = false;

		if (playerStruct.isDead)
			SceneManager.LoadScene("MainMenu");
		else
			SceneManager.LoadScene("Game");
	}

	public void SendBattleData()
	{
		BattleController.playerHealth = playerStruct.health;
		BattleController.playerDamage = playerStruct.damage;
		BattleController.playerSpeed = playerStruct.speed;

		BattleController.enemyHealth = enemyStructArray[enemyID].health;
		BattleController.enemyDamage = enemyStructArray[enemyID].damage;
		BattleController.enemySpeed = enemyStructArray[enemyID].speed;
	}

	public void RecieveBattleData()
	{
		playerStruct.health = BattleController.playerHealth;
		playerStruct.damage = BattleController.playerDamage;
		playerStruct.speed = BattleController.playerSpeed;

		enemyStructArray[enemyID].health = BattleController.enemyHealth;
		enemyStructArray[enemyID].damage = BattleController.enemyDamage;
		enemyStructArray[enemyID].speed = BattleController.enemySpeed;
	}
}
