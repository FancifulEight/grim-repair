using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour {
    public List<Transform> soulPositions = new List<Transform>();
    public List<Transform> targetPositions = new List<Transform>();

    public List<Target> targetBodies = new List<Target>();
    public List<Soul> soulOptions = new List<Soul>();

    public ObjectDataScript soulData;
}
