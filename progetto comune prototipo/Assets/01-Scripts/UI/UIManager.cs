using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region VARIABLES
    GameManager GameManager;
    Playerbehaviour Playerbehaviour;
    [SerializeField]
    Text Enemycounter_text;
    public Text ink_text;
    Inkstone Ink;
    Enemyspawnmanager Enemyspawnmanager;
    PointSystem pointsystem;
    [SerializeField]
    Yokaislayer YS;
    public PointSystem PS;
    public Text score_text;
    public Text scoremultiplier_text;
    public Text TEST_timer;
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
        if (Ink == null)
        {
            Debug.LogError("inkstone è null");
        }

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }
    }

    private void Update()
    {
        UpdateEnemycounter();

        UpdateInkCounter();

        UpdateScore();

        UpdateScoreMultiplier();

        TEST_UpdateTimer();
    }


    private void UpdateEnemycounter()
    {
        Enemycounter_text.text = "Counter nemici: " + Enemyspawnmanager.enemykilled;
    }

    void UpdateInkCounter()
    {
        if (YS.active == false)
        {
            int ink, maxink;
            if (Ink.Ink < 0) ink = 0;
            else ink = Ink.Ink;
            if (Ink.maxInk < 0) maxink = 0;
            else maxink = Ink.maxInk;
            ink_text.text = (ink + "/" + maxink);
        }
    }

    public void UpdateScore()
    {
        int convertedscore = (int)pointsystem.score;
        score_text.text = convertedscore.ToString("{0:0,0}");
    }

    void UpdateScoreMultiplier()
    {
        scoremultiplier_text.text = "x" + pointsystem.scoreMultiplier.ToString();
    }

    void TEST_UpdateTimer()
    {
        TEST_timer.text = PS.currentTimer.ToString();
    }
}
