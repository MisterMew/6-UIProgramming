using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    /// Variables
    Resolution[] resolutions;
    public AudioMixer audioMixer;

    void Start() {
        SetWindowMode(0);
    }

    /* Set the window mode */
    public void SetWindowMode(int windowMode) {
        FullScreenMode mWindowMode = (FullScreenMode)windowMode;
        Screen.fullScreenMode = mWindowMode;
        Debug.Log("WINDOW MODE: " + mWindowMode);
    }

    /* Set the screen resolution */
    public void SetResolution(int res) {
        Vector2 mRes = new Vector2();
        if (res == 0) { mRes.x = 1920; mRes.y = 1080; } else
        if (res == 1) { mRes.x = 1280; mRes.y =  720; } else
        if (res == 2) { mRes.x =  720; mRes.y =  480; } 

        Screen.SetResolution((int)mRes.x, (int)mRes.y, (Screen.fullScreenMode));

        Debug.Log("RESOLUTION: " + mRes.x + "x" + mRes.y);
    }
}
