using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuWithKeyboard : MonoBehaviour
{
    StartEndSequence startendsequence;
    public GameObject CurrentStateMenu;
    public List<GameObject> CurrentStateMenuButtons;
    public List<GameObject> SelectedImages;
    public GameObject currentButtonOver, check;
    public GameObject firstMenu, areYouSure, PauseText;
    public bool ExistingList;
    public bool mouseClick;
    public int Index;
    PauseMenu PM;
    enum MenuStates
    {
        GameInPlay,
        FirstPauseMenu,
        AreYouSure
    }
    MenuStates CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = MenuStates.GameInPlay;
        PM = FindObjectOfType<PauseMenu>();
        startendsequence = FindObjectOfType<StartEndSequence>();
        ExistingList = false;
        mouseClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseStateMachine();
        MouseOverButtons();
    }

    public void PauseStateMachine()
    {
        switch (CurrentState)
        {
            case MenuStates.GameInPlay:
                GameInPlay();
                break;
            case MenuStates.FirstPauseMenu:
                //StateOperations();
                FirstPauseMenu();
                break;
            case MenuStates.AreYouSure:
                //StateOperations();
                AreYouSure();
                break;
        }
    }

    #region State Operations
    public void GetGameOperations()
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
    public void CycleButtons()
    {
        if (ExistingList == true)
        {
            ActivateImage();
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Vertical") == 1 || Input.GetAxis("VerticalButtonsJoystick") == 0.1f)
            {
                Index--;
                if (Index < 0)
                {
                    Index = CurrentStateMenuButtons.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Vertical") == -1 || Input.GetAxis("VerticalButtonsJoystick") == -0.1f)
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
        GetGameOperations();
        CycleButtons();
    }

    #endregion

    #region All Menus Button Functions
    public void GameInPlay()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && startendsequence.starting == false && startendsequence.ending == false)
        {
            ExistingList = false;
            CurrentState = MenuStates.FirstPauseMenu;
            firstMenu.SetActive(true);
            PauseText.SetActive(true);
            PM.Pause();
        }
    }
    public void FirstPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //resume
                    PM.Resume();
                    ExistingList = false;
                    CurrentState = MenuStates.GameInPlay;
                    firstMenu.SetActive(false);
                    PauseText.SetActive(false);
                    break;
                case 1:
                    //back to menu
                    ExistingList = false;
                    CurrentState = MenuStates.AreYouSure;
                    firstMenu.SetActive(false);
                    areYouSure.SetActive(true);
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PM.Resume();
            ExistingList = false;
            CurrentState = MenuStates.GameInPlay;
            PauseText.SetActive(false);
            firstMenu.SetActive(false);
        }
    }
    public void AreYouSure()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch (Index)
            {
                case 0:
                    //yes
                    AudioManager.Instance.StopSound("MainTrack");
                    AudioManager.Instance.StopSound("Yoo");
                    AudioManager.Instance.StopSound("GongSound");
                    AudioManager.Instance.PlaySound("MenuTheme");
                    PM.Resume();
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    //no
                    ExistingList = false;
                    CurrentState = MenuStates.FirstPauseMenu;
                    areYouSure.SetActive(false);
                    firstMenu.SetActive(true);
                    break;
            }
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExistingList = false;
            CurrentState = MenuStates.FirstPauseMenu;
            areYouSure.SetActive(false);
            firstMenu.SetActive(true);
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
        mouseClick = true;
    }
    #endregion
}
