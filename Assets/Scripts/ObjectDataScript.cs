using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectDataScriptableObject", order = 1)]
public class ObjectDataScript : ScriptableObject
{
	public List<Material> materialList = new List<Material>();
	public List<Material> targetMaterialList = new List<Material>();
	public List<Object> bodyList = new List<Object>();

	public int RandomMaterialIndex()
	{
		return Random.Range(0, materialList.Count);
	}
	public int RandomBodyList()
	{
		return Random.Range(0, bodyList.Count);
	}
}
