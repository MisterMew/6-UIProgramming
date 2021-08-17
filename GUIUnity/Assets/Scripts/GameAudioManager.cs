using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAudioManager : MonoBehaviour {
    public Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", slider.value);
    }
}
