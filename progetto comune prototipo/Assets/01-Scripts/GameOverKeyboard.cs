using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverKeyboard : MonoBehaviour
{
    public GameObject CurrentStateMenu;
    public List<GameObject> CurrentStateMenuButtons;
    public List<GameObject> SelectedImages;
    public GameObject currentButtonOver, check;
    public bool ExistingList;
    public bool mouseClick;
    public int Index;
    enum MenuStates
    {
        GameOver
    }
    MenuStates CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = MenuStates.GameOver;
        ExistingList = false;
        mouseClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameOverStateMachine();
        MouseOverButtons();
    }

    public void GameOverStateMachine()
    {
        switch(CurrentState)
        {
            case MenuStates.GameOver:
                StateOperations();
                GameOver();
                break;
        }
    }

    public void StateOperations()
    {
        GetGameObject();
        CycleButtons();
    }

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

    public void GameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || mouseClick == true)
        {
            switch(Index)
            {
                case 0:
                    SceneManager.LoadScene(2);
                    AudioManager.Instance.PlaySound("MenuConfirm");
                    break;
                case 1:
                    SceneManager.LoadScene(1);
                    AudioManager.Instance.PlaySound("MenuCancel");
                    break;
            }
            mouseClick = false;
        }
    }

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
