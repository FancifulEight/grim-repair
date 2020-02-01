using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public string startSceneName;
	 public GameObject mainPanel, optionsPanel;

	public void StartGame()
	{
		SceneManager.LoadScene(startSceneName);
	}

	public void QuictGame()
	{
	#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
	#else
	      Application.Quit();
	#endif
	}

	public void OptionsPanelActivate()
	{
		optionsPanel.SetActive(true);
		mainPanel.SetActive(false);
	}

	public void OptionsPanelDeactivate()
	{
		mainPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}

}
