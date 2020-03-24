using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDManager : MonoBehaviour
{
    GameManager GameManager;
    public Transform lifeToSpawn;
    public GameObject libePreFab1, libePreFab2, libePreFab3, libePreFab4, libePreFab5, libePreFab6;
    int playerLife;
    Playerbehaviour Playerbehaviour;
    [SerializeField]
    Text gameover_text;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        Playerbehaviour = FindObjectOfType<Playerbehaviour>();

    }

    private void Start()
    {
        playerLife = Playerbehaviour.life;
    }

    private void Update()
    {
        if(playerLife > 0)
        {
            playerLife = Playerbehaviour.life;
            ActiveUI();
        }
        else
        {
            ActiveUI();
            gameover_text.gameObject.SetActive(true);
        }
        
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
    
    

}
