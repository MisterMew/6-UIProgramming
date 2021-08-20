using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    /// Variables
    private static AudioManager musicTransitionInstance;
    
    [SerializeField] string volumeParameter = "volMaster";
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] float volumeMultiplier = 20F;

     /// AWAKE
    /* Execute upon awaking */
    private void Awake() {
        if (musicTransitionInstance == null) {
            musicTransitionInstance = this;
            DontDestroyOnLoad(musicTransitionInstance);
        } else { 
            Destroy(gameObject); //Destroy to ensure duplicates don't exists
        }

        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

     /// START
    /* Execute upon starting */
    void Start() {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    private void HandleSliderValueChanged(float sliderValue) {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(sliderValue) * volumeMultiplier);
    }
}
