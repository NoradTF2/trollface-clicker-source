using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Clicker clicker;
    public AudioMixer audioMixer;
    public Dropdown soundDropdown;
    public Slider volumeSlider;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20.0f);
        clicker.currentVolume = volume;
    }
}
