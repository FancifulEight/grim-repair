using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    public float turnSpeed = 10f;

    void Start() {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    void FixedUpdate() {
        transform.Rotate(0, turnSpeed * Time.fixedDeltaTime, 0);
    }
}
