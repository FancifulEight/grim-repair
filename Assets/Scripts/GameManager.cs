﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	List<int> validSoulMaterialIndexes;
	int correctMaterialIndex;
	public Poinsetta poinsetta;
	int currentScore;
	[SerializeField]
	int defaultPositivePoints = 100;
	int currentPositivePoints;
	[SerializeField]
	int minimumNegativePoints = -50;
	[SerializeField]
	int additionalNegativePoints = -200;
	int currentNegativePoints;
	float pointMultiplier;
	bool gameIsOver, matchMade, canResetGame;
	public Animator curtainAnimationController;
	public GameObject gameOverPanel;
	InputWatcher inputWatcher;
	SoulManager soulManager;
	public int carriedSoulIndex;
	
 void Awake()
	{
		gameIsOver = false;
		
		if(instance !=null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		inputWatcher = FindObjectOfType<InputWatcher>();
		soulManager = FindObjectOfType<SoulManager>();
	}

	
	private void Start()
	{
		currentScore = 0;
		poinsetta.SetPoints(currentScore);
		soulManager.SetUpGame();
		validSoulMaterialIndexes = new List<int>();
		foreach (Soul s in soulManager.currentSouls)
		{
			if (s.doNotRandomize)
			{
				s.SetMaterial(s.materialIndex);
			}
		//	else
		//	{
		//		s.SetRandomMaterial();
		//	}

		 validSoulMaterialIndexes.Add(s.materialIndex);
		}

		// Only one target should be the right one.
		correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0, validSoulMaterialIndexes.Count)];

		
		curtainAnimationController.SetBool("GameRunning", true);
	}
	//set materials and target materials.
	private void Update()
	{
		if(matchMade && canResetGame)
		{
			curtainAnimationController.SetBool("GameRunning", false);
			canResetGame = false;
		}
		if ( !gameIsOver && curtainAnimationController.GetCurrentAnimatorStateInfo(0).IsName("Open"))
		{
			//Debug.Log("Opening");
			//Debug.Log(curtainAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime);
			pointMultiplier = 1 - curtainAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime;
			canResetGame = true;
			currentPositivePoints = (int)Mathf.Round(defaultPositivePoints * pointMultiplier);
			//currentNegativePoints = minimumNegativePoints - (additionalNegativePoints * pointMultiplier);
		}
		else
		{
			canResetGame = false;
		}
			
	}
	//When soul matches target
	public void SoulMatches(Soul soul, Target target)
	{
		Debug.Log("Matches!");
		matchMade = true;
		canResetGame = false;
		currentScore += currentPositivePoints;
		poinsetta.SetPoints(currentScore);
		// Randomize soul material and update what indexes are valid
		validSoulMaterialIndexes.Remove(soul.materialIndex);
		int grabbedIndex = soulManager.currentSouls.IndexOf(soul);
		
		soulManager.ChangeSoul(soul);
		soulManager.ReturnSoulStartPosition(grabbedIndex);
		validSoulMaterialIndexes.Add(soulManager.currentSouls[grabbedIndex].materialIndex);
		SelectValidIndex();
		
		// Close Curtains 
		//curtainAnimationController.SetBool("GameRunning", false);
		//If not allowing input until curtain starts opening again?


	}
	//When soul doesn't match target
	public void SoulNoMatch(Soul soul)
	{
		Debug.Log("No Match!");
		int grabbedIndex = soulManager.currentSouls.IndexOf(soul);
		soulManager.ReturnSoulStartPosition(grabbedIndex);
		currentScore += minimumNegativePoints;
		if (currentScore < 0)
		{
			currentScore = 0;
		}
		poinsetta.SetPoints(currentScore);
	}
	//Set points (doesn't add to total, sets value).

	void SetTargetMaterials()
	{
		//foreach (Target t in targetArray)
		//{
		//	// If the target is the right one, set it's material index to one of the soulmaterial indexes.
		//	if (t.correctTarget == true)
		//	{
		//		t.SetMaterial(correctMaterialIndex);
		//	}
		//	else
		//	{
		//		// If the target is not the right one, set its material index to one not being 
		//		if(t.doNotRandomize)
		//		{
		//			t.SetMaterial(t.materialIndex);
		//		}
		//		else
		//		{
		//			t.SetRandomMaterial();
		//			while (validSoulMaterialIndexes.Contains(t.materialIndex))
		//			{
		//				t.SetRandomMaterial();
		//			}
		//		}

		//	}
		//}
		//soulManager.currentTarget.SetMaterial(correctMaterialIndex);
	}

	void SelectValidIndex()
	{
		correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0, validSoulMaterialIndexes.Count)];

	}

	public void EndGame()
	{
		// Do end game stuff.
		Debug.Log("GameOver");
		gameOverPanel.SetActive(true);
	}

	public void OnCurtainsClosed()
	{
		Debug.Log("Curtains Closed.");
		//When curtains closed
		
	}

	public void OnCurtainsOpened()
	{
		// Called on curtains fully opened
		Debug.Log("Curtains Opened");
		if(!matchMade)
		{
			gameIsOver = true;
		}
		curtainAnimationController.SetBool("GameRunning", false);
	}

	public void OnCurtainsIdle()
	{
		Debug.Log("Curtain Idle");
		matchMade = false;
		if (gameIsOver)
		{
			EndGame();
		}
		else
		{
			soulManager.SetRandomBody();
			curtainAnimationController.SetBool("GameRunning", true);

			//Reset positive and negative points to default values for potential matches.
			currentPositivePoints = defaultPositivePoints;
			currentNegativePoints = minimumNegativePoints;
		}

	}

	
	public void ReloadGame()
	{
		// Currently just reload the current scene on replay.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void CheckForMatch(Soul soul, Target target)
	{
		
			Debug.Log("Soul matIndex = " + soul.materialIndex + " Target matIndex =  " + target.materialIndex);
			if (soul.materialIndex == target.materialIndex)
			{
				SoulMatches(soul, target);
			}
			else
			{
				SoulNoMatch(soul);
			}
		
		
	}

	public void ResetSoulPosition(Soul soul)
	{
			soulManager.ReturnSoulStartPosition(soulManager.currentSouls.IndexOf(soul));
	
	}
}
