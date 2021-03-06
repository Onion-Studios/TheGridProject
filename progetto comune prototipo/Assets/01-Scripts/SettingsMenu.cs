﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    Resolution[] Resolutions;
    public TMP_Dropdown ResolutionDropdown;
    public Slider VoiceSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider BrightnessSlider;
    public static float Voice;
    public static float Music;
    public static float SFX;
    public static float Brightness;

    void Start()
    {
        VoiceSlider.value = PlayerPrefs.GetFloat("VoicE");
        MusicSlider.value = PlayerPrefs.GetFloat("MusiC");
        SFXSlider.value = PlayerPrefs.GetFloat("SfX");

        Resolutions=Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int CurrentResolutionIndex = 0;
        for(int i=0; i<Resolutions.Length; i++)
        {
            string option = Resolutions[i].width + "x" + Resolutions[i].height;
            options.Add(option);

            if(Resolutions[i].width==Screen.width && Resolutions[i].height==Screen.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("VoicE", VoiceSlider.value);
        PlayerPrefs.SetFloat("MusiC", MusicSlider.value);
        PlayerPrefs.SetFloat("SfX", SFXSlider.value);
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = Resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #region Audio
    public void SetVoice (float voice)
    {
        AudioMixer.SetFloat("Voice", voice);
    }
    public void SetMusic(float music)
    {
        AudioMixer.SetFloat("Music", music);
    }
    public void SetSFX(float sfx)
    {
        AudioMixer.SetFloat("SFX", sfx);
    }
    #endregion
    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
}
