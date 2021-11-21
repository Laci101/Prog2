using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{  
    public AudioMixer audioMixer;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public Toggle Toggle;
    
    //Saving toggles value
    private void Start() {
        if ((PlayerPrefs.GetInt("ToggleBool") == 1)) {
            Toggle.isOn = true;
        }
        else {
            Toggle.isOn = false;
        }
    }

    private void Update() {
        if (Toggle.isOn == true) {
            PlayerPrefs.SetInt("ToggleBool", 1);
        }
        else {
            PlayerPrefs.SetInt("ToggleBool", 0);
        }
    }
}
