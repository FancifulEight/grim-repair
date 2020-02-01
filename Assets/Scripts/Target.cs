using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public ObjectDataScript data;
	public int materialIndex;
 public void SetMaterial(int matIndex)
	{
		GetComponentInChildren<Renderer>().material = data.targetMaterialLIst[matIndex];
		materialIndex = matIndex;
	}
}
