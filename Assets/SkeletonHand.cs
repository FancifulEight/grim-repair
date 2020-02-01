using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHand : MonoBehaviour {
    public float distanceFromCam = 2.5f;
    void Start() {
        
    }

    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        transform.position = ray.origin + ray.direction * distanceFromCam;
    }
}
