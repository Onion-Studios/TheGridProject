using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWithKeyboard : MonoBehaviour
{
    public GameObject mainMenu, OptionsMenu, AudioMenu, VideoMenu, ControlsMenu, ExitDialogue, Record, PointsTag;
    //public GameObject FirstOptionsButton, CloseOptionsButton, FirstCloseButton, CloseCloseButton, AudioSlider, CloseAudioButton, VideoResolution, CloseVideoButton, FirstControlsButton, CloseControlsButton;
    public GameObject CurrentStateMenu;
    public List<GameObject> CurrentStateMenuButtons;
    public List<GameObject> SelectedImages;
    public bool ExistingList = false;
    public int Index;
    MainMenu MM;
    OptionMenu OM;
    enum MenuStates
    {
        MainMenu,
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
        MM = FindObjectOfType<MainMenu>();
        OM = FindObjectOfType<OptionMenu>();
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
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    MainMenu();
                }
                StateOperations();
                break;
            case MenuStates.HowToPlay:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    HowToPlay();
                }
                StateOperations();
                break;
            case MenuStates.Credits:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    Credits();
                }
                StateOperations();
                break;
            case MenuStates.ExitGame:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    ExitGame();
                }
                StateOperations();
                break;
            case MenuStates.Options:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    Options();
                }
                StateOperations();
                break;
            case MenuStates.Controls:
                StateOperations();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    Controls();
                }
                break;
            case MenuStates.Audio:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    Audio();
                }
                StateOperations();
                break;
            case MenuStates.Video:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    Video();
                }
                StateOperations();
                break;
        }
    }

    #region All Generic State Operations
    public void GetGameObject()
    {
        if (ExistingList == false)
        {
            CurrentStateMenuButtons = new List<GameObject>();
            SelectedImages = new List<GameObject>();
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
        if (ExistingList == true)
        {
            ActivateImage();
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Index--;
                if (Index < 0)
                {
                    Index = CurrentStateMenuButtons.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Index++;
                if (Index == CurrentStateMenuButtons.Count)
                {
                    Index = 0;
                }
            }
        }
    }
    public void StateOperations()
    {
        GetGameObject();
        CycleButtons();
    }
    public void ActivateImage()
    {
        for (int i = 0; i < SelectedImages.Count; i++)
        {
            if (i == Index)
            {
                SelectedImages[i].SetActive(true);
            }
            else
            {
                SelectedImages[i].SetActive(false);
            }
        }
    }
    #endregion

    #region All Menus Buttons Functions
    public void MainMenu()
    {
        switch (Index)
        {
            case 0:
                MM.GoToLevel();
                AudioManager.Instance.PlaySound("MenuConfirm");
                AudioManager.Instance.StopSound("MenuTheme");
                break;
            case 1:
                AudioManager.Instance.PlaySound("MenuConfirm");
                break;
            case 2:
                AudioManager.Instance.PlaySound("MenuConfirm");
                CurrentState = MenuStates.Options;
                ResetList();
                mainMenu.SetActive(false);
                OptionsMenu.SetActive(true);
                break;
            case 3:
                AudioManager.Instance.PlaySound("MenuConfirm");
                break;
            case 4:
                AudioManager.Instance.PlaySound("MenuConfirm");
                CurrentState = MenuStates.ExitGame;
                ExistingList = false;
                ExitDialogue.SetActive(true);
                break;
        }

    }

    public void HowToPlay()
    {

    }

    public void Credits()
    {

    }

    public void ExitGame()
    {
        switch (Index)
        {
            case 0:
                MM.QuitGame();
                break;
            case 1:
                ExitDialogue.SetActive(false);
                CurrentState = MenuStates.MainMenu;
                ExistingList = false;
                break;
        }
    }

    public void Options()
    {
        switch (Index)
        {

        }
    }

    public void Controls()
    {
        switch (Index)
        {

        }
    }

    public void Audio()
    {
        switch (Index)
        {

        }
    }

    public void Video()
    {
        switch (Index)
        {

        }
    }
    #endregion
}
