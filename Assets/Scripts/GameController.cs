using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[System.Serializable] struct player
	{
		public int health;
		public int damage;
		public int speed;
		public int skillPointsRemaining;

		public Transform playTransform;

		public bool isDead;

		public Color myColor;
	}

	[System.Serializable]
	struct enemy
	{
		public int health;
		public int damage;
		public int speed;
		public int skillPointsRemaining;

		public bool isDead;
	}

	[SerializeField] player playerStruct;
	[SerializeField] enemy[] enemyStructArray;

	[SerializeField] int skillPointsAvailable = 10;
	[SerializeField] int maxSkillPointsPerSkill = 5;

	public bool battleMode = false;

	public static Color colorToSet;

	void Start ()
	{
		RandomiseEnemies();
	}
	
	void Update ()
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

	public void EnterBattleMode()
	{
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
}
