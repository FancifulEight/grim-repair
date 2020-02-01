using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static AudioController ac = null;

    [Header("Audio Sources")]
    public AudioSource mainSrc;
    public AudioSource altSrc;

    [Header("Volumes")]
    public float musicVolume = 1;
    public float sfxVolume = 1;

    public bool isIntense = false;

    void Awake() {
        if (ac == null) {
            ac = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        
    }

    void FixedUpdate() {
        mainSrc.volume = Mathf.Lerp(mainSrc.volume, musicVolume, Time.fixedDeltaTime);
        altSrc.volume = Mathf.Lerp(altSrc.volume, ((isIntense) ? musicVolume:0), Time.fixedDeltaTime);
    }
}
