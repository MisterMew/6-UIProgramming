using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameAudioManager : MonoBehaviour {
    public Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", slider.value);
    }

    void Update() {
        
    }
}
