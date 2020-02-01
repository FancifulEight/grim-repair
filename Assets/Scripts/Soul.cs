using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	public ObjectDataScript data;
	public Material material;
	public int  materialIndex;
	Vector3 startPosition;
	
	void Awake()
	{
		// material = data.materialList[materialIndex];
		materialIndex = data.RandomMaterialIndex();
		material = data.materialList[materialIndex];
		GetComponentInChildren<Renderer>().material = material;
	}
	private void Start()
	{
		startPosition = transform.position;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
	}

}
