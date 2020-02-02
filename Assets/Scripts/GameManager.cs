using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	List<int> validSoulMaterialIndexes;
	int correctMaterialIndex;
	public Poinsetta poinsetta;
	int currentScore;
	int defaultPositivePoints = 250;
	int currentPositivePoints;
	int minimumNegativePoints = -50;
	int currentNegativePoints;
	float pointMultiplier;
	bool gameIsOver, matchMade, canResetGame;
	public Animator curtainAnimationController;
	public GameObject gameOverPanel;
	InputWatcher inputWatcher;
	SoulManager soulManager;
	public int carriedSoulIndex;
	//public TextMeshProUGUI positivePointsText;
	string positivePointString;
	bool musicIsIntense;
	public int currentRoundNumber;
	AudioController audioController;
	int intenseRoundNumber = 4;
	
	 float startCurtainCloseSpeed = 0.1f;
	 float maxCurtainCloseSpeed = 2;
	 float currentCurtainCloseSpeed;
	 float curtainCloseLerpSpeed = 100f;

	 [Header("SFX")]
	 public AudioClip goodSFX;
	 public AudioClip badSFX;

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
		audioController = FindObjectOfType<AudioController>();
	}

	
	private void Start()
	{
		currentScore = 0;
		musicIsIntense = false;
		poinsetta.SetPoints(currentScore);
		currentRoundNumber = 0;
		currentNegativePoints = minimumNegativePoints;
		currentPositivePoints = defaultPositivePoints;
		soulManager.SetUpGame();
		validSoulMaterialIndexes = new List<int>();
		foreach (Soul s in soulManager.currentSouls)
		{
			//if (s.doNotRandomize)
			//{
			//	s.SetMaterial(s.materialIndex);
			//}
		//	else
		//	{
		//		s.SetRandomMaterial();
		//	}

		 validSoulMaterialIndexes.Add(s.materialIndex);
		}

		// Only one target should be the right one.
		correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0, validSoulMaterialIndexes.Count)];

		currentCurtainCloseSpeed = startCurtainCloseSpeed;
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
			pointMultiplier = curtainAnimationController.GetCurrentAnimatorStateInfo(0).normalizedTime;
			canResetGame = true;
			currentPositivePoints = (int)(defaultPositivePoints * (1-pointMultiplier));

			//curtainAnimationController.SetFloat("CurtainCloseSpeed", currentCurtainCloseSpeed,);
			//currentCurtainCloseSpeed = Mathf.Lerp(startCurtainCloseSpeed, 1, currentRoundNumber / 50);
			//curtainAnimationController.SetFloat("CurtainCloseSpeed", currentCurtainCloseSpeed, 1, currentRoundNumber / 10);

		}
		else
		{
			canResetGame = false;
		}
		//positivePointsText.text = "+" + currentPositivePoints.ToString();
	}
	//When soul matches target
	public void SoulMatches(Soul soul, Target target)
	{
		AudioController.ac.PlaySFX(goodSFX);

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
		AudioController.ac.PlaySFX(badSFX);

		Debug.Log("No Match!");
		int grabbedIndex = soulManager.currentSouls.IndexOf(soul);
		soulManager.ReturnSoulStartPosition(grabbedIndex);
		currentScore += currentNegativePoints;
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
			AudioController.ac.PlaySFX(badSFX);
			gameIsOver = true;
		}
		curtainAnimationController.SetBool("GameRunning", false);
	}

	public void OnCurtainsIdle()
	{
		Debug.Log("Curtain Idle");
		currentRoundNumber++;
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
			audioController.SetIntensity(currentRoundNumber >= intenseRoundNumber);
			//Increase curtain closing speed at the end of each round
			if(currentCurtainCloseSpeed<maxCurtainCloseSpeed)
			{
				currentCurtainCloseSpeed = Mathf.Lerp(startCurtainCloseSpeed, maxCurtainCloseSpeed, currentRoundNumber/curtainCloseLerpSpeed);
				curtainAnimationController.SetFloat("CurtainCloseSpeed", currentCurtainCloseSpeed);
			}
			
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
