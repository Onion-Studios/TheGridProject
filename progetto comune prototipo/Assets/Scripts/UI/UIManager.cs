using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region VARIABLES
    GameManager GameManager;
    Playerbehaviour Playerbehaviour;
    [SerializeField]
    Text Enemycounter_text;
    [SerializeField]
    Text yokaislayer_text;
    public Text ink_text;
    Inkstone Ink;
    Enemyspawnmanager Enemyspawnmanager;
    PointSystem pointsystem;
    public Text score_text;
    public Text scoremultiplier_text;
    #endregion


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

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }

    }

    private void Start()
    {
        
    }

    private void Update()
    {

        UpdateEnemycounter();

        UpdateYokaiSlayerCounter();

        UpdateInkCounter();

        UpdateScore();

        UpdateScoreMultiplier();
    }


    private void UpdateEnemycounter()
    {
        Enemycounter_text.text = "Counter nemici: " + Enemyspawnmanager.enemykilled;
    }

    void UpdateYokaiSlayerCounter()
    {
        yokaislayer_text.text = ("Yokai Slayer: " + Playerbehaviour.yokaislayercount);
    }

    void UpdateInkCounter()
    {
        ink_text.text = (Ink.Ink + "/" + Ink.maxInk);
    }

    void UpdateScore()
    {
        int convertedscore = (int)pointsystem.score;
        score_text.text = convertedscore.ToString();
    }

    void UpdateScoreMultiplier()
    {
        scoremultiplier_text.text = "x" + pointsystem.scoreMultiplier.ToString(); 
    }

}
