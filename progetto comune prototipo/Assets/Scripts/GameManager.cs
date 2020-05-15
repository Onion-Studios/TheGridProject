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
    public int StartIntensity2 = 15;
    public int StartIntensity3 = 25;

    private void Awake()
    {
        ActualPlayer = FindObjectOfType<Playerbehaviour>();
        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    private void Update()
    {
        ChangeIntensity(Enemyspawnmanager.enemykilled);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(2);
    }

    public Playerbehaviour GetPlayerBehaviur()
    {
        return FindObjectOfType<Playerbehaviour>(); 
    }

    void ChangeIntensity(int enemykilled)
    {
        if(enemykilled >= 0 && enemykilled < StartIntensity2)
        {
            GameIntensity = 1;
        }
        else if (enemykilled >= StartIntensity2 && enemykilled < StartIntensity3)
        {
            GameIntensity = 2;
        }
        else if (enemykilled >= StartIntensity3)
        {
            GameIntensity = 3;
        }
    }
}
