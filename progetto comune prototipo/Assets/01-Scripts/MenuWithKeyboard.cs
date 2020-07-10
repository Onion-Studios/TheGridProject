using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*public class MenuWithKeyboard : MonoBehaviour
{
    public GameObject mainMenu, OptionsMenu, AudioMenu, VideoMenu, ControlsMenu, ExitDialogue, Record, PointsTag, howToPlay, credits, tutorial;
    public GameObject audioSelectedImage, videoSelectedImage;
    public GameObject CurrentStateMenu;
    public List<GameObject> CurrentStateMenuButtons;
    public List<GameObject> SelectedImages;
    public GameObject currentButtonOver, check;
    public bool ExistingList;
    public bool mouseClick;
    bool changingState;
    public int Index, howToPlayIndex;
    MainMenu MM;
    public GameObject howToPlayBG;
    public GameObject[] howToPlayImages;
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
        ExistingList = false;
        mouseClick = false;
        changingState = true;
    }

    // Update is called once per frame
    void Update()
    {
        MenuStateMachine();
        MouseOverButtons();
    }

    public void MenuStateMachine()
    {
        switch (CurrentState)
        {
            case MenuStates.MainMenu:
                StateOperations();
                MainMenu();
                break;
            case MenuStates.HowToPlay:
                StateOperations();
                HowToPlay();
                break;
            case MenuStates.Credits:
                StateOperations();
                Credits();
                break;
            case MenuStates.ExitGame:
                StateOperations();
                ExitGame();
                break;
            case MenuStates.Options:
                StateOperations();
                Options();
                break;
            case MenuStates.Controls:
                StateOperations();
                Controls();
                break;
            case MenuStates.Audio:
                StateOperations();
                Audio();
                break;
            case MenuStates.Video:
                StateOperations();
                Video();
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Vertical") > 0 || Input.GetAxis("VerticalButtonsJoystick") == 0.1f)
            {
                Index--;
                AudioManager.Instance.PlaySound("Hightonetick");
                if (Index < 0)
                {
                    Index = CurrentStateMenuButtons.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Vertical") < 0 || Input.GetAxis("VerticalButtonsJoystick") == -0.1f)
            {
                Index++;
                AudioManager.Instance.PlaySound("Hightonetick");
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

    #region Mouse Functionality
    private void MouseOverButtons()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        bool checkBool = false;

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject.GetComponent<Button>() != null)
                {
                    currentButtonOver = go.gameObject;
                    if (currentButtonOver != check)
                    {
                        check = currentButtonOver;
                        checkBool = true;
                    }
                }
            }
        }

        if (checkBool == true)
        {
            for (int i = 0; i < CurrentStateMenuButtons.Count; i++)
            {
                if (CurrentStateMenuButtons[i].name == currentButtonOver.name)
                {
                    Index = i;
                }
            }
            checkBool = false;
        }
    }

    public void MouseClick()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.gameObject.name;
        if (clickedButton == CurrentStateMenuButtons[Index].name)
        {
            mouseClick = true;
        }
        else
        {
            switch (clickedButton)
            {
                case "StartGameButton":
                    MM.GoToLevel();
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    AudioManager.Instance.StopSound("MenuTheme");
                    break;
                case "HowToPlayButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.HowToPlay;
                    break;
                case "OptionsButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Options;
                    break;
                case "CreditsButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Credits;
                    break;
                case "ExitGameButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.ExitGame;
                    break;
                case "AudioButton":
                    changingState = true;
                    audioSelectedImage.SetActive(true);
                    videoSelectedImage.SetActive(false);
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Audio;
                    break;
                case "VideoButton":
                    changingState = true;
                    audioSelectedImage.SetActive(false);
                    videoSelectedImage.SetActive(true);
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Video;
                    break;
                case "ControlsButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Controls;
                    break;
                case "BackButton":
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.MainMenu;
                    break;
            }
        }
    }
    #endregion

    #region All Menus Buttons Functions
    public void MainMenu()
    {
        if (changingState == true)
        {
            ExistingList = false;

            HowToPlayCycleImages(true);

            mainMenu.SetActive(true);
            howToPlay.SetActive(false);
            OptionsMenu.SetActive(false);
            credits.SetActive(false);
            ExitDialogue.SetActive(false);
            Record.SetActive(true);
            PointsTag.SetActive(true);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //Start Game
                    MM.GoToLevel();
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    AudioManager.Instance.StopSound("MenuTheme");
                    break;
                case 1:
                    //How to play
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.HowToPlay;
                    break;
                case 2:
                    //Options
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Options;
                    break;
                case 3:
                    //Credits
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Credits;
                    break;
                case 4:
                    //Exit Game
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.ExitGame;
                    break;
            }
            mouseClick = false;
        }
    }

    #region HowToPlay
    public void HowToPlay()
    {
        if (changingState == true)
        {
            ExistingList = false;

            howToPlayIndex = 0;
            howToPlay.SetActive(true);
            mainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            ExitDialogue.SetActive(false);
            credits.SetActive(false);
            Record.SetActive(false);
            PointsTag.SetActive(false);

            changingState = false;
        }
        howToPlayBG.SetActive(true);
        HowToPlayCycleImages(false);
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //left arrow
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    howToPlayIndex--;
                    if (howToPlayIndex < 0)
                    {
                        howToPlayIndex = howToPlayImages.Length - 1;
                    }
                    break;
                case 1:
                    //right arrow
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    howToPlayIndex++;
                    if (howToPlayIndex >= howToPlayImages.Length)
                    {
                        howToPlayIndex = 0;
                    }
                    break;
                case 2:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.MainMenu;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.MainMenu;
        }
    }

    public void HowToPlayCycleImages(bool alloff)
    {
        if (alloff == false)
        {
            for (int i = 0; i < howToPlayImages.Length; i++)
            {
                if (i == howToPlayIndex)
                {
                    howToPlayImages[i].SetActive(true);
                }
                else
                {
                    howToPlayImages[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < howToPlayImages.Length; i++)
            {
                howToPlayImages[i].SetActive(false);
            }
            howToPlayBG.SetActive(false);
            howToPlayIndex = 0;
        }
    }
    #endregion

    public void Credits()
    {
        if (changingState == true)
        {
            ExistingList = false;

            credits.SetActive(true);
            mainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            howToPlay.SetActive(false);
            ExitDialogue.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.MainMenu;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.MainMenu;
        }
    }

    public void ExitGame()
    {
        if (changingState == true)
        {
            ExistingList = false;

            ExitDialogue.SetActive(true);
            mainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
            credits.SetActive(false);
            howToPlay.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //yes
                    MM.QuitGame();
                    break;
                case 1:
                    //no
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.MainMenu;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.MainMenu;
        }
    }

    public void Options()
    {
        if (changingState == true)
        {
            ExistingList = false;

            OptionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            ExitDialogue.SetActive(false);
            howToPlay.SetActive(false);
            credits.SetActive(false);
            AudioMenu.SetActive(false);
            VideoMenu.SetActive(false);
            ControlsMenu.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //audio
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Audio;
                    break;
                case 1:
                    //video
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Video;
                    break;
                case 2:
                    //controls
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    CurrentState = MenuStates.Controls;
                    break;
                case 3:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.MainMenu;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.MainMenu;
        }
    }

    public void Controls()
    {
        if (changingState == true)
        {
            ExistingList = false;

            ControlsMenu.SetActive(true);
            AudioMenu.SetActive(false);
            VideoMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            Record.SetActive(false);
            PointsTag.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.Options;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.Options;
        }
    }

    public void Audio()
    {
        if (changingState == true)
        {
            ExistingList = false;

            AudioMenu.SetActive(true);
            VideoMenu.SetActive(false);
            ControlsMenu.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.Options;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.Options;
        }
    }

    public void Video()
    {
        if (changingState == true)
        {
            ExistingList = false;

            VideoMenu.SetActive(true);
            AudioMenu.SetActive(false);
            ControlsMenu.SetActive(false);

            changingState = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //back
                    changingState = true;
                    AudioManager.Instance.PlaySound("MenuCancel");
                    CurrentState = MenuStates.Options;
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changingState = true;
            AudioManager.Instance.PlaySound("MenuCancel");
            CurrentState = MenuStates.Options;
        }
    }
    #endregion
}


}*/
