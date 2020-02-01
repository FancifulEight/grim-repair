using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHand : MonoBehaviour {
    public float distanceFromCam = 2.5f;
    public float fixedZ = -8;
    void Start() {
        
    }

    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 newPos = ray.origin + ray.direction * distanceFromCam;

        transform.position = newPos;
    }
}
