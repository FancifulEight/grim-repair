using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHand : MonoBehaviour {
    void Start() {
        
    }

    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        transform.position = ray.origin + ray.direction * 3;
    }
}
