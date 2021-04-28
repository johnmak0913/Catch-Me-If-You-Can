using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public string volumeParam = "MasterVolume";
    public AudioMixer audioMixer;
    public Slider slider;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParam, slider.value);
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParam, slider.value);
    }
}
