using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveOptionUI()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(Audiobutton);     
    }

    public void GoToMainMenu()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(startgamebutton);
    }

    public void GoToOptionMenu()
    {
        if (AudioMenu.activeInHierarchy == true)
        {
            AudioMenu.SetActive(false);
            MainMenuUI.ActivateOptionsMenuButton();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Audiobutton);
        }
        else if (VideoMenu.activeInHierarchy == true)
        {
            VideoMenu.SetActive(false);
            MainMenuUI.ActivateOptionsMenuButton();
            eventsystem.SetSelectedGameObject(null);
            eventsystem.SetSelectedGameObject(Audiobutton);
        }
        else if (ControlsMenu.activeInHierarchy == true)
        {
            ControlsMenu.SetActive(false);
            OptionMenu.SetActive(true);
            eventsystem.SetSelectedGameObject(null);
            eventsystem.SetSelectedGameObject(Audiobutton);
        }
    }

    public void GoToAudioMenu()
    {
        AudioMenu.SetActive(true);
        MainMenuUI.DeactiveOptionsMenuButton();
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(soundslider);
    }

    public void GoToVideoMenu()
    {
        VideoMenu.SetActive(true);
        MainMenuUI.DeactiveOptionsMenuButton();
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(resolutions);
    }

    public void GoToControlsMenu()
    {
        OptionMenu.SetActive(false);
        ControlsMenu.SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(controlsback);
    }
}
