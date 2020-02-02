using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public ObjectDataScript data;
	public bool correctTarget = false;
	public int materialIndex;
	public GameObject targetBody;
	public bool doNotRandomize;
	public void SetMaterial(int matIndex)
	{
		GetComponentInChildren<Renderer>().material = data.targetMaterialList[matIndex];
		materialIndex = matIndex;
	}

	public void SetRandomMaterial()
	{
		materialIndex = data.RandomMaterialIndex();
		GetComponentInChildren<Renderer>().material = data.targetMaterialList[materialIndex];
	}
}
