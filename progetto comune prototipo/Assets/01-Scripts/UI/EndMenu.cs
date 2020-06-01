using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Start()
    {
       
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(1);
    }

}
