using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public GameObject BGImage;
    public int TutorialImageIndex;
    public GameObject[] TutorialImage;
    public GameObject TutorialButton;
    public GameObject MainMenu;
    public EventSystem eventsystem;
    public GameObject RightArrowbutton;
    public GameObject startgamebutton;
    public MainMenuUI MainMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        TutorialImageIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveTutorialUI()
    {
        MainMenu.SetActive(false);
        BGImage.SetActive(true);
        TutorialButton.SetActive(true);
        TutorialImage[TutorialImageIndex].SetActive(true);
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(RightArrowbutton);
    }

    public void ScrollRight()
    {
        TutorialImage[TutorialImageIndex].SetActive(false);
        if(TutorialImageIndex >= 0 && TutorialImageIndex <3 || TutorialImageIndex < 3)
        {
            TutorialImageIndex++;
        }
        else if(TutorialImageIndex == 3)
        {
            TutorialImageIndex = 0;
        }
        TutorialImage[TutorialImageIndex].SetActive(true);
    }

    public void ScrollLeft()
    {
        TutorialImage[TutorialImageIndex].SetActive(false);
        if (TutorialImageIndex > 0 || TutorialImageIndex <= 3 && TutorialImageIndex > 0)
        {
            TutorialImageIndex--;
        }
        else if (TutorialImageIndex == 0)
        {
            TutorialImageIndex = 3;
        }
        TutorialImage[TutorialImageIndex].SetActive(true);
    }

    public void BackToMainMenuTutorial()
    {
        MainMenu.SetActive(true);
        BGImage.SetActive(false);
        TutorialButton.SetActive(false);
        TutorialImage[TutorialImageIndex].SetActive(false);
        TutorialImageIndex = 0;
        eventsystem.SetSelectedGameObject(null);
        eventsystem.SetSelectedGameObject(startgamebutton);
    }
}
