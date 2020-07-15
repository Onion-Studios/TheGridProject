using UnityEngine;
using UnityEngine.EventSystems;

public class OptionMenuUI : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionMenu;
    public GameObject AudioMenu;
    public GameObject VideoMenu;
    public GameObject ControlsMenu;
    public MainMenuUI MainMenuUI;
    public EventSystem eventsystem;
    public GameObject Audiobutton;
    public GameObject soundslider;
    public GameObject resolutions;
    public GameObject controlsback;
    public GameObject startgamebutton;

    public void ActiveOptionUI()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(Audiobutton);
    }

    public void GoToMainMenu()
    {
        AudioManager.Instance.PlaySound("MenuCancel");
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(startgamebutton);
    }

    public void GoToOptionMenu()
    {
        if (AudioMenu.activeInHierarchy == true)
        {
            AudioManager.Instance.PlaySound("MenuCancel");
            AudioMenu.SetActive(false);
            MainMenuUI.ActivateOptionsMenuButton();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Audiobutton);
        }
        else if (VideoMenu.activeInHierarchy == true)
        {
            AudioManager.Instance.PlaySound("MenuCancel");
            VideoMenu.SetActive(false);
            MainMenuUI.ActivateOptionsMenuButton();
            eventsystem.SetSelectedGameObject(null);
            eventsystem.SetSelectedGameObject(Audiobutton);
        }
        else if (ControlsMenu.activeInHierarchy == true)
        {
            AudioManager.Instance.PlaySound("MenuCancel");
            ControlsMenu.SetActive(false);
            OptionMenu.SetActive(true);
            eventsystem.SetSelectedGameObject(null);
            eventsystem.SetSelectedGameObject(Audiobutton);
        }
    }

    public void GoToAudioMenu()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        AudioMenu.SetActive(true);
        MainMenuUI.DeactiveOptionsMenuButton();
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(soundslider);
    }

    public void GoToVideoMenu()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        VideoMenu.SetActive(true);
        MainMenuUI.DeactiveOptionsMenuButton();
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(resolutions);
    }

    public void GoToControlsMenu()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        OptionMenu.SetActive(false);
        ControlsMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(controlsback);
    }
}
