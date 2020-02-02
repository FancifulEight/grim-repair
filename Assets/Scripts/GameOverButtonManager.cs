using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverButtonManager : MonoBehaviour
{
    public void Replay()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
