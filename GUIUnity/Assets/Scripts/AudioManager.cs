using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", slider.value);
        slider.value = PlayerPrefs.GetFloat("MusicVolume", slider.value);
        slider.value = PlayerPrefs.GetFloat("SFXVolume", slider.value);
    }

    public void SetMasterLevel() {
        float sliderValue = slider.value;
        Debug.Log("VOLUME: " + sliderValue);

        audioMixer.SetFloat("volMaster", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetMusicLevel(float sliderValue) {
        //float sliderValue = slider.value;
        Debug.Log("VOLUME: " + sliderValue);

        audioMixer.SetFloat("volMusic", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXLevel() {
        float sliderValue = slider.value;
        Debug.Log("VOLUME: " + sliderValue);

        audioMixer.SetFloat("volSFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }
}
