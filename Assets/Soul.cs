using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
	public ObjectDataScript data;
	public Color color;
	public int colorIndex;
	private void Start()
	{
		color = data.colorList[colorIndex];
	}


}
