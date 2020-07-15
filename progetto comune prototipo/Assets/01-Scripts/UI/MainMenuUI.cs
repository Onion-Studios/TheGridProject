using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    public GameObject CreditsUI;
    public GameObject ExitSurePrompt;
    public Button[] MainMenuButtons;
    public Button[] OptionsButtons;
    public EventSystem EventSystem;
    public GameObject Backbutton;
    public GameObject StartGameButton;
    public GameObject ExitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActiveExitGamePrompt()
    {
        ExitSurePrompt.SetActive(true);
        DeactiveMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(ExitGameButton);
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(2);
        AudioManager.Instance.PlaySound("MenuConfirm");
        AudioManager.Instance.StopSound("MenuTheme");
    }

    public void ActiveCreditsUI()
    {
        CreditsUI.SetActive(true);
        DeactiveMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(Backbutton);
    }

    public void BackToMainMenuCredits()
    {
        CreditsUI.SetActive(false);
        ActivateMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(StartGameButton);
    }

    public void BackToMainMenuExit()
    {
        ExitSurePrompt.SetActive(false);
        ActivateMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(StartGameButton);
    }

    public void DeactiveMainMenuButton()
    {
        foreach(Button Buttons in MainMenuButtons)
        {
            Buttons.interactable = false;
        }
    }

    public void ActivateMainMenuButton()
    {
        foreach (Button Buttons in MainMenuButtons)
        {
            Buttons.interactable = true;
        }
    }

    public void DeactiveOptionsMenuButton()
    {
        foreach (Button Buttons in OptionsButtons)
        {
            Buttons.interactable = false;
        }
    }

    public void ActivateOptionsMenuButton()
    {
        foreach (Button Buttons in OptionsButtons)
        {
            Buttons.interactable = true;
        }
    }
}
