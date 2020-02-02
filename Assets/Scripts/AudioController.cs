using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour {
    public static AudioController ac = null;

    [Header("Audio Sources")]
    public AudioSource mainSrc;
    public AudioSource altSrc;

    [Header("Volumes")]
    public float musicVolume = 1;
    public float sfxVolume = 1;

    private bool isIntense = false;

    void Awake() {
        if (ac == null) {
            ac = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate() {
        mainSrc.volume = Mathf.Lerp(
            mainSrc.volume, ((isIntense) ? 0:musicVolume), 2 * Time.fixedDeltaTime);
        altSrc.volume = Mathf.Lerp(
            altSrc.volume, ((isIntense) ? musicVolume:0), 2 * Time.fixedDeltaTime);
    }

    public void PlaySFX(AudioClip clip, float relativeVol = 1) {
        mainSrc.PlayOneShot(clip, sfxVolume * relativeVol);
    }

    public void SetIntensity(bool newIntensity) {
        if (this.isIntense == newIntensity) return;

        if (newIntensity) {
            StartIntenseMusic();
        } else {
            StopIntenseMusic();
        }
    }

    public void StartIntenseMusic() {
        isIntense = true;
        altSrc.timeSamples = ConvertSamples(
            mainSrc.timeSamples, mainSrc.clip, altSrc.clip);
    }

    public void StopIntenseMusic() {
        isIntense = false;
        mainSrc.timeSamples = ConvertSamples(
            altSrc.timeSamples, altSrc.clip, mainSrc.clip);
    }

    private int ConvertSamples(int otherSamples, AudioClip otherSong, AudioClip mySong) {
        float otherSampleLength = (otherSong.samples);
        float mySampleLength = (mySong.samples);
        
        int result = (int)((otherSamples * (mySampleLength / otherSampleLength)) % mySampleLength);
        return result;
    }
}
