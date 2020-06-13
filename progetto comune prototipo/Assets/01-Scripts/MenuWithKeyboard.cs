using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuWithKeyboard : MonoBehaviour
{
    //public GameObject MainMenu, OptionsMenu, AudioMenu, VideoMenu, ControlsMenu, ExitDialogue, Record, PointsTag;
    //public GameObject FirstOptionsButton, CloseOptionsButton, FirstCloseButton, CloseCloseButton, AudioSlider, CloseAudioButton, VideoResolution, CloseVideoButton, FirstControlsButton, CloseControlsButton;
    public GameObject CurrentStateMenu;
    public List<GameObject> CurrentStateMenuButtons;
    public List<GameObject> SelectedImages;
    public bool ExistingList = false;
    public int Index;
    enum MenuStates { MainMenu,
        HowToPlay,
        Credits,
        ExitGame,
        Options,
        Controls,
        Audio,
        Video
    }
    MenuStates CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = MenuStates.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        MenuStateMachine();
    }
    /*#region Options
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
    //#region Audio
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
    //#region Video
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
    //#region Controls
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
    //#region Exit
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
    #endregion*/

    public void MenuStateMachine()
    {
       switch (CurrentState)
        {
            case MenuStates.MainMenu:
                MainMenu();
                break;
            case MenuStates.HowToPlay:

                break;
            case MenuStates.Credits:

                break;
            case MenuStates.ExitGame:

                break;
            case MenuStates.Options:

                break;
            case MenuStates.Controls:

                break;
            case MenuStates.Audio:

                break;
            case MenuStates.Video:

                break;
        }
    }
    
    public void GetGameObject()
    {
        if(ExistingList==false)
        {
            Button[] ButtonArray;
            CurrentStateMenu = gameObject.transform.Find(CurrentState.ToString()).gameObject;
            ButtonArray = CurrentStateMenu.GetComponentsInChildren<Button>();

            foreach (Button button in ButtonArray)
            {
                CurrentStateMenuButtons.Add(button.gameObject);
                SelectedImages.Add(button.transform.Find("Selected").gameObject);
            }
            Index = 0;
            ExistingList = true;
        }
        
    }

    public void CycleButtons()
    {
        if(ExistingList==true)
        {
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                Index--;
                if(Index<0)
                {
                    Index = CurrentStateMenuButtons.Count;
                }
            }
        }
    }

    public void MainMenu()
    {
        GetGameObject();
    }
}
