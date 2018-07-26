using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		SceneManager.LoadScene(1);
	}
}
