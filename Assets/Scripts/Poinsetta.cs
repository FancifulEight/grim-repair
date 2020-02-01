using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Poinsetta : MonoBehaviour {
    private TextMeshProUGUI text;
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetPoints(int points) {
        string digits = "";

        for (int i = 0;i < 6;i++) {
            digits = (points % 10) + digits;
            points /= 10;
        }

        text.text = digits;
    }
}
