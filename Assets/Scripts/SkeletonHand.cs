using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHand : MonoBehaviour {
    public float distanceFromCam = 2.5f;
    public float fixedZ = -8;
	// If grabbedSoulPosition is Vector3.zero, use defaultDistanceFromCa. Else set position to GSP.
	public Vector3 grabbedSoulPosition;
    void Start() {
        
    }

    void FixedUpdate() {
		if(grabbedSoulPosition==Vector3.zero)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			Vector3 newPos = ray.origin + ray.direction * distanceFromCam;

			transform.position = newPos;
		}
		else
		{
			transform.position = grabbedSoulPosition;
		}
       
    }
}
