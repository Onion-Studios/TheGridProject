using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public void ExitGame()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        Application.Quit();
    }

    public void ActiveExitGamePrompt()
    {
        AudioManager.Instance.PlaySound("MenuCancel");
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
        AudioManager.Instance.PlaySound("MenuConfirm");
        CreditsUI.SetActive(true);
        DeactiveMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(Backbutton);
    }

    public void BackToMainMenuCredits()
    {
        AudioManager.Instance.PlaySound("MenuCancel");
        CreditsUI.SetActive(false);
        ActivateMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(StartGameButton);
    }

    public void BackToMainMenuExit()
    {
        AudioManager.Instance.PlaySound("MenuCancel");
        ExitSurePrompt.SetActive(false);
        ActivateMainMenuButton();
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(StartGameButton);
    }

    public void DeactiveMainMenuButton()
    {
        foreach (Button Buttons in MainMenuButtons)
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
