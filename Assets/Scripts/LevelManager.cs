using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene (name);
	}

    public void LoadLevel(string name, float time)
    {
        StartCoroutine(LoadAfterTime(name, time));
    }

    IEnumerator LoadAfterTime(string name, float time)
    {
        yield return new WaitForSeconds(time);
        LoadLevel(name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void QuitGame()
	{
		Application.Quit ();
	}
}
