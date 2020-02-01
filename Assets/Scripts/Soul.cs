﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	public ObjectDataScript data;
	public int materialIndex;
	Vector3 startPosition;

	void Awake()
	{
		// material = data.materialList[materialIndex];
		
	}
	private void Start()
	{
		startPosition = transform.position;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
	}

	public void SetMaterial(int matIndex)
	{
		GetComponentInChildren<Renderer>().material = data.materialList[matIndex];
		materialIndex = matIndex;
	}

	public void SetRandomMaterial()
	{
		materialIndex = data.RandomMaterialIndex();
		GetComponentInChildren<Renderer>().material = data.materialList[materialIndex];
	}
}
