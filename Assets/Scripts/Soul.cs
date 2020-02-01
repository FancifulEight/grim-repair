using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	public ObjectDataScript data;
	public Color color;
	public Material material;
	public int colorIndex, materialIndex;
	public Vector3 startPosition;
	
	void Awake()
	{
		color = data.colorList[colorIndex];
		material = data.materialList[materialIndex];

		GetComponent<Renderer>().material = material;
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
