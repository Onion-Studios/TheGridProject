using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    public bool IsGamePaused = false;
    public GameObject PauseMenu;
    public GameObject ExitPrompt;
    public GameObject ExitCancelText;
    public GameObject NoButton;
    public EventSystem EventSystem;
    public GameObject resumebutton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);       
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(resumebutton);
        IsGamePaused = true;
        Cursor.visible = true;
    }

    public void RestartGame()
    {        
        StartCoroutine(GamePausedCoroutine());
    }

    public IEnumerator GamePausedCoroutine()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        yield return new WaitForEndOfFrame();        
        IsGamePaused = false;
        Cursor.visible = false;
    }

    public void ActiveExitPrompt()
    {
        ExitPrompt.SetActive(true);
        ExitCancelText.SetActive(false);
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(NoButton);
    }

    public void BackToPauseMenu()
    {
        ExitPrompt.SetActive(false);
        ExitCancelText.SetActive(true);
        EventSystem.SetSelectedGameObject(null);
        EventSystem.SetSelectedGameObject(resumebutton);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        IsGamePaused = false;
        //main menu scene
        SceneManager.LoadScene(1);
        
    }
}
