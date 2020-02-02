using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public string startSceneName;
	public GameObject mainPanel, optionsPanel;
	public Animator skeletonHand;

	[Header("SFX")]
	public AudioClip clickSFX;


	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	public void StartGame() {
		if (!skeletonHand.GetBool("Dragging")) {
			AudioController.ac.PlaySFX(clickSFX);
			skeletonHand.SetBool("Dragging", true);
			StartCoroutine(LoadSceneAfterTime());
		}
	}

	public IEnumerator LoadSceneAfterTime() {
		yield return new WaitForSeconds(0.2f);
		SceneManager.LoadScene(startSceneName);
	}

	public void QuictGame()
	{
		AudioController.ac.PlaySFX(clickSFX);
	#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
	#else
	      Application.Quit();
	#endif
	}

	public void OptionsPanelActivate()
	{
		AudioController.ac.PlaySFX(clickSFX);
		optionsPanel.SetActive(true);
		mainPanel.SetActive(false);
	}

	public void OptionsPanelDeactivate()
	{
		AudioController.ac.PlaySFX(clickSFX);
		mainPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}

}
