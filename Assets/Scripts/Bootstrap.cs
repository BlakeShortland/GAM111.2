using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
	//This script keeps my GameController active across all scenes

	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		SceneManager.LoadScene(1);
	}
}
