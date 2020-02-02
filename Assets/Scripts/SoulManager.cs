using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour {
    public List<Transform> soulPositions = new List<Transform>();
    public List<Transform> targetPositions = new List<Transform>();

    public List<Target> targetBodies = new List<Target>();
    public List<Soul> soulOptions = new List<Soul>();

    public Target currentTarget = null;
    public List<Soul> currentSouls = new List<Soul>();

    public void SetUpGame() {
        SetSoulsToPositions();
        SetBody(currentSouls[Random.Range(0, currentSouls.Count)].materialIndex);
    }

    public List<Soul> SetSoulsToPositions() {
        ResetSoulPositions();
        currentSouls = new List<Soul>(soulOptions);

        int index = Random.Range(0, currentSouls.Count);
        while(4 < currentSouls.Count) {
            currentSouls.RemoveAt(index);
            index = Random.Range(0, currentSouls.Count);
        }

        return currentSouls;
    }

    public void ResetSoulPositions() {
        foreach (Soul soul in currentSouls) {
            soul.gameObject.transform.position = new Vector3(0, -100, 0);
        }

        currentSouls.Clear();
    }

    public void SetRandomBody() {
        SetBody(Random.Range(0, targetBodies.Count));
    }

    public void SetBody(int index) {
        if (currentTarget != null)
            currentTarget.gameObject.transform.position = new Vector3(0, -100, 0);
        currentTarget = targetBodies[index];
        currentTarget.transform.position = targetPositions[Random.Range(0, targetPositions.Count)].position;
    }

    public void ChangeSoul(Soul currentSoul) {
        ChangeSoul(currentSouls.IndexOf(currentSoul));
    }

    public void ChangeSoul(int index) {
        currentSouls[index].gameObject.transform.position = new Vector3(0, -100, 0);
        currentSouls[index] = GetNewRandomSoul();
    }

    public Soul GetNewRandomSoul() {
        List<Soul> soulList = new List<Soul>(soulOptions);
        soulList.RemoveAll(currentSouls.Contains);

        return soulList[Random.Range(0, soulList.Count)];
    }
}
