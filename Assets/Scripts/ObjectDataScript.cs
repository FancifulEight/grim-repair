using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectDataScriptableObject", order = 1)]
public class ObjectDataScript : ScriptableObject
{
	[Header("Soul Materials")]
	public List<Material> materialList = new List<Material>();
	public List<Material> soulBodymaterialList = new List<Material>();
	
	[Header("Target Body Materials")]
	public List<Material> targetMaterialList = new List<Material>();
	[Header("Animal Forms (to use instead of cubes)")]
	public List<GameObject> bodyList = new List<GameObject>();

	public int RandomMaterialIndex()
	{
		return Random.Range(0, bodyList.Count);
	}
	public int RandomBodyList()
	{
		return Random.Range(0, bodyList.Count);
	}
}
