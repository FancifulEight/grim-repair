using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	Soul[] soulArray;
	Target[] targetArray;


	// Game Start - randomize soul and target materials. Only one target is the correct one.
	 void Awake()
	{
		soulArray = FindObjectsOfType<Soul>();
		targetArray = FindObjectsOfType<Target>();
	}

	private void Start()
	{
		List<int> validSoulMaterialIndexes = new List<int>();
		foreach (Soul s in soulArray)
		{
			s.SetRandomMaterial();
			validSoulMaterialIndexes.Add(s.materialIndex);
		}
		
		// Only one target should be the right one.
		targetArray[Random.Range(0, targetArray.Length)].correctTarget = true;

		foreach(Target t in targetArray)
		{
			int correctMaterialIndex = validSoulMaterialIndexes[Random.Range(0,validSoulMaterialIndexes.Count)];
			// If the target is the right one, set it's material index to one of the soulmaterial indexes.
			if (t.correctTarget == true)
			{
				t.SetMaterial(correctMaterialIndex);
			}
			else
			{
				// If the target is not the right one, set its material index to one not being 
				t.SetRandomMaterial();
				while(validSoulMaterialIndexes.Contains(t.materialIndex))
				{
					t.SetRandomMaterial();
				}
			}
		}
	}
	//set materials and target materials.
	//void SetMaterial()


	// Set Object

	//When soul matches target

	//When soul doesn't match target

	//Set points (doesn't add to total, sets value).
}
