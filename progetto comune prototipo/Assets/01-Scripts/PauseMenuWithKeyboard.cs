using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuWithKeyboard : MonoBehaviour
{
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
        ExistingList = false;
        mouseClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseStateMachine();
    }

    public void PauseStateMachine()
    {
        switch(CurrentState)
        {
            case MenuStates.GameInPlay:
                GameInPlay();
                break;
            case MenuStates.FirstPauseMenu:
                StateOperations();
                FirstPauseMenu();
                break;
            case MenuStates.AreYouSure:
                StateOperations();
                AreYouSure();
                break;
        }
    }

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
        GetGameOperations();
        CycleButtons();
    }

    public void GameInPlay()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExistingList = false;
            CurrentState = MenuStates.FirstPauseMenu;
            firstMenu.SetActive(true);
            PauseText.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void FirstPauseMenu()
    {
        switch(Index)
        {
            case 0:
                Time.timeScale = 1f;
                ExistingList = false;
                CurrentState = MenuStates.GameInPlay;
                firstMenu.SetActive(false);
                PauseText.SetActive(false);
                break;
            case 1:
                ExistingList = false;
                CurrentState = MenuStates.AreYouSure;
                firstMenu.SetActive(false);
                areYouSure.SetActive(true);
                break;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            ExistingList = false;
            CurrentState = MenuStates.GameInPlay;
            PauseText.SetActive(false);
            firstMenu.SetActive(false);
        }
    }
    public void AreYouSure()
    {
        switch (Index)
        {
            case 0:
                PM.LoadMenu();
                break;
            case 1:
                ExistingList = false;
                CurrentState = MenuStates.FirstPauseMenu;
                areYouSure.SetActive(false);
                firstMenu.SetActive(true);
                break;
        }
    }
}
