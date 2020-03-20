using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager GameManager;
    public Transform lifeToSpawn;
    public GameObject libePreFab1, libePreFab2, libePreFab3, libePreFab4, libePreFab5, libePreFab6;
    int playerLife;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();

    }

    private void Start()
    {
        SetCurrentPlayerLife();
    }

    public void ActiveUI()
    {
        if (playerLife == 6)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(true);
            libePreFab6.SetActive(true);
        }
        if (playerLife == 5)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(true);
            libePreFab6.SetActive(false);
        }
        if (playerLife == 4)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        if (playerLife == 3)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        if (playerLife == 2)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(false);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        if (playerLife == 1)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(false);
            libePreFab3.SetActive(false);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        if (playerLife == 0)
        {
            libePreFab1.SetActive(false);
            libePreFab2.SetActive(false);
            libePreFab3.SetActive(false);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }


    }
    public void SetCurrentPlayerLife()
    {
        playerLife = GameManager.ActualPlayer.life;
    }
    

}
