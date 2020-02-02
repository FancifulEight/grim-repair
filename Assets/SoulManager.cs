using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour {
    public List<Transform> soulPositions = new List<Transform>();
    public List<Transform> targetPositions = new List<Transform>();

    public List<Target> targetBodies = new List<Target>();
    public List<Soul> soulOptions = new List<Soul>();

    public ObjectDataScript soulData;

    public List<Soul> SetUpGame() {
        List<Soul> soulList = SetSoulsToPositions();

        SetBody(soulList[Random.Range(0, soulList.Count)].materialIndex);

        return soulList;
    }

    public List<Soul> SetSoulsToPositions() {
        List<Soul> soulList = new List<Soul>(soulOptions);

        int index = Random.Range(0, soulList.Count);
        while(4 < soulList.Count) {
            soulList.RemoveAt(index);
            index = Random.Range(0, soulList.Count);
        }

        return soulList;
    }

    public void SetBody(int index) {

    }

    public void ChangeSoul() {

    }
}
