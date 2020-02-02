using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	public ObjectDataScript data;
	public int materialIndex;
	Vector3 startPosition;
	public GameObject soulBody;
	public ParticleSystem ps;
	public bool doNotRandomize;

	private Renderer rend;

	void Awake()
	{
		//material = data.materialList[materialIndex];
		//soulBody = data.bodyList[materialIndex];
		rend = GetComponent<Renderer>();
	}

	void Start()
	{
		ps = GetComponentInChildren<ParticleSystem>();
		SetMaterial(materialIndex);
		//startPosition = transform.position;
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
	}

	public void SetMaterial(int matIndex)
	{
		rend.material = data.materialList[matIndex];
		ParticleSystem.MainModule mainModule = ps.main;
		mainModule.startColor = rend.material.color;
		Debug.Log(rend.material.color);
		materialIndex = matIndex;
	}

	public void SetRandomMaterial()
	{
		materialIndex = data.RandomMaterialIndex();
		GetComponentInChildren<Renderer>().material = data.materialList[materialIndex];
	}

	public void SetStartPosition()
	{
		
	}
}
