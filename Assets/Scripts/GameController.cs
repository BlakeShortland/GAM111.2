using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public bool battleMode = false;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public void EnterBattleMode()
	{
		battleMode = true;

		SceneManager.LoadScene(2);
	}

	public void ExitBattleMode()
	{
		battleMode = false;

		SceneManager.LoadScene(1);
	}
}
