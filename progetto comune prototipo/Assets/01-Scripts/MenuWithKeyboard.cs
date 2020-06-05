using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuWithKeyboard : MonoBehaviour
{
    public GameObject MainMenu, OptionsMenu, AudioMenu, VideoMenu, ControlsMenu, ExitDialogue, Record, PointsTag;
    public GameObject FirstOptionsButton, CloseOptionsButton, FirstCloseButton, CloseCloseButton, AudioSlider, CloseAudioButton, VideoResolution, CloseVideoButton, FirstControlsButton, CloseControlsButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Options
    public void OpenOptions()
    {
        OptionsMenu.SetActive(true);
        MainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstOptionsButton);
    }
    public void CloseOptions()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CloseOptionsButton);
    }
    #endregion
    #region Audio
    public void OpenAudio()
    {
        AudioMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(AudioSlider);
    }
    public void CloseAudio()
    {
        AudioMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CloseAudioButton);
    }
    #endregion
    #region Video
    public void OpenVideo()
    {
        VideoMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(VideoResolution);
    }
    public void CloseVideo()
    {
        VideoMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CloseVideoButton);
    }
    #endregion
    #region Controls
    public void OpenControls()
    {
        ControlsMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        Record.SetActive(false);
        PointsTag.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstControlsButton);
    }
    public void CloseControls()
    {
        ControlsMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        Record.SetActive(true);
        PointsTag.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstControlsButton);
    }
    #endregion
    #region Exit
    public void OpenExit()
    {
        ExitDialogue.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstCloseButton);
    }
    public void CloseExit()
    {
        ExitDialogue.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CloseCloseButton);
    }
    #endregion
}
