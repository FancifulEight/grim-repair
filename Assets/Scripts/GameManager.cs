using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	Soul[] soulArray;
	Target[] targetArray;
	List<int> validSoulMaterialIndexes;
	int correctMaterialIndex;
	public Poinsetta poinsetta;
	int currentScore;
	int pointsForMatch = 100;
	int pointsForMiss = -250;
	bool gameIsOver, matchMade;
	public Animator curtainAnimationController;

	// Game Start - randomize soul and target materials. Only one target is the correct one.
	void Awake()
	{
		gameIsOver = false;
		soulArray = FindObjectsOfType<Soul>();
		targetArray = FindObjectsOfType<Target>();
		if(instance !=null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		currentScore = 0;
		poinsetta.SetPoints(currentScore);
		validSoulMaterialIndexes = new List<int>();
		foreach (Soul s in soulArray)
		{
			if(s.doNotRandomize)
			{
				s.SetMaterial(s.materialIndex);
			}
			else
			{
				s.SetRandomMaterial();
			}
			
			validSoulMaterialIndexes.Add(s.materialIndex);
		}
		
		// Only one target should be the right one.
		targetArray[Random.Range(0, targetArray.Length)].correctTarget = true;

		correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0, validSoulMaterialIndexes.Count)];

		SetTargetMaterials();
		curtainAnimationController.SetBool("GameRunning", true);
	}
	//set materials and target materials.
	
	


	// Set Object

	//When soul matches target
	public void SoulMatches(Soul soul, Target target)
	{
		Debug.Log("Matches!");
		matchMade = true;
		currentScore += pointsForMatch;
		poinsetta.SetPoints(currentScore);
		// Randomize soul material and update what indexes are valid
		validSoulMaterialIndexes.Remove(soul.materialIndex);
		soul.SetRandomMaterial();
		validSoulMaterialIndexes.Add(soul.materialIndex);
		SelectValidIndex();

		// Close Curtains 
		curtainAnimationController.SetBool("GameRunning", false);

		// Randomize all target materials.
		SetTargetMaterials();

	}
	//When soul doesn't match target
	public void SoulNoMatch()
	{
		Debug.Log("No Match!");
		currentScore += pointsForMiss;
		if (currentScore < 0)
		{
			currentScore = 0;
		}
		poinsetta.SetPoints(currentScore);
	}
	//Set points (doesn't add to total, sets value).

	void SetTargetMaterials()
	{
		foreach (Target t in targetArray)
		{
			// If the target is the right one, set it's material index to one of the soulmaterial indexes.
			if (t.correctTarget == true)
			{
				t.SetMaterial(correctMaterialIndex);
			}
			else
			{
				// If the target is not the right one, set its material index to one not being 
				if(t.doNotRandomize)
				{
					t.SetMaterial(t.materialIndex);
				}
				else
				{
					t.SetRandomMaterial();
					while (validSoulMaterialIndexes.Contains(t.materialIndex))
					{
						t.SetRandomMaterial();
					}
				}
				
			}
		}
	}

	void SelectValidIndex()
	{
		correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0, validSoulMaterialIndexes.Count)];

	}

	public void EndGame()
	{
		// Do end game stuff.
		Debug.Log("GameOver");
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
		matchMade = false;
		curtainAnimationController.SetBool("GameRunning", false);
	}

	public void OnCurtainsIdle()
	{
		Debug.Log("Curtain Idle");
		if (gameIsOver)
		{
			EndGame();
		}
		else
		{
			SetTargetMaterials();
			curtainAnimationController.SetBool("GameRunning", true);
		}
	}
}
