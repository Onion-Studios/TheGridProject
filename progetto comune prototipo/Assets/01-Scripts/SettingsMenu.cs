using System.Collections;
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

    void Start()
    {
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

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = Resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

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

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
