using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    public Playerbehaviour ActualPlayer;
    public int GameIntensity = 1;
    Enemyspawnmanager Enemyspawnmanager;

    private void Awake()
    {
        ActualPlayer = FindObjectOfType<Playerbehaviour>();
        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    private void Update()
    {
        ChangeIntensity(Enemyspawnmanager.nemicoucciso);
    }


    public void GoToGameplay()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMenuScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToSplashStart()
    {
        SceneManager.LoadScene(0);
    }

    public Playerbehaviour GetPlayerBehaviur()
    {
        return FindObjectOfType<Playerbehaviour>(); 
    }

    void ChangeIntensity(int enemykilled)
    {
        if(enemykilled >= 0 && enemykilled < 5)
        {
            GameIntensity = 1;
        }
        else if (enemykilled >= 5 && enemykilled < 10)
        {
            GameIntensity = 2;
        }
        else if (enemykilled >= 10)
        {
            GameIntensity = 3;
        }
    }
}
