using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager GameManager;
    public Transform lifeToSpawn;
    Playerbehaviour Playerbehaviour;
    public Text Gold_text;
    [SerializeField]
    Text Enemycounter_text;
    [SerializeField]
    Text yokaislayer_text;
    public Text ink_text;
    Inkstone Ink;
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

        Ink = FindObjectOfType<Inkstone>();
        if(Ink == null)
        {
            Debug.LogError("inkstone è null");
        }

    }

    private void Start()
    {
        
    }

    private void Update()
    {

        UpdateGoldCounter();

        UpdateEnemycounter();

        UpdateYokaiSlayerCounter();

        UpdateInkCounter();
    }


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

    void UpdateInkCounter()
    {
        ink_text.text = (Ink.Ink + "/" + Ink.maxInk);
    }

}
