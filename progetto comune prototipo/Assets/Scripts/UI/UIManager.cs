using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager GameManager;
    public Transform lifeToSpawn;
    public GameObject libePreFab1, libePreFab2, libePreFab3, libePreFab4, libePreFab5, libePreFab6;
    Playerbehaviour Playerbehaviour;
    public Text Gold_text;
    [SerializeField]
    Text Enemycounter_text;
    [SerializeField]
    Text yokaislayer_text;
    Enemyspawnmanager Enemyspawnmanager;

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("GameManager è null");
        }

        Playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (Playerbehaviour == null)
        {
            Debug.LogError("playerbehaviour è null");
        }

        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (Enemyspawnmanager == null)
        {
            Debug.LogError("enemyspawnmanager è null");
        }

    }

    private void Start()
    {
        
    }

    private void Update()
    {
      //  UpdateLifeUI();

        UpdateGoldCounter();

        UpdateEnemycounter();

        UpdateYokaiSlayerCounter();
    }


   /* public void UpdateLifeUI()
    {
        if (Playerbehaviour.life == 6)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(true);
            libePreFab6.SetActive(true);
        }
        else if (Playerbehaviour.life == 5)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(true);
            libePreFab6.SetActive(false);
        }
        else if (Playerbehaviour.life == 4)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(true);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        else if (Playerbehaviour.life == 3)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(true);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        else if (Playerbehaviour.life == 2)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(true);
            libePreFab3.SetActive(false);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        else if (Playerbehaviour.life == 1)
        {
            libePreFab1.SetActive(true);
            libePreFab2.SetActive(false);
            libePreFab3.SetActive(false);
            libePreFab4.SetActive(false);
            libePreFab5.SetActive(false);
            libePreFab6.SetActive(false);
        }
        else if (Playerbehaviour.life < 1)
        {
            GameManager.GoToEndMenu();
        }
    }*/

    private void UpdateEnemycounter()
    {
        Enemycounter_text.text = "Counter nemici: " + Enemyspawnmanager.nemicoucciso;
    }

    void UpdateGoldCounter()
    {
        Gold_text.text = ("Gold: " + Playerbehaviour.Gold);
    }

    void UpdateYokaiSlayerCounter()
    {
        yokaislayer_text.text = ("Yokai Slayer: " + Playerbehaviour.yokaislayercount);
    }
}
