using System.Collections;
using UnityEngine;

public class MalevolentEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 4;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    GameManager GameManager;
    WaveManager WM;
    [SerializeField]
    int scoreEnemy2;
    [SerializeField]
    int scoreEnemy3;
    public int scoreEnemy;
    [HideInInspector]
    public Vector3 position;
    private Animator malevolentAnimator;
    private bool playerDeathPlayed;

    #endregion

    private void Awake()
    {
        malevolentAnimator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("playerbehaviour is NULL!");
        }

        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("enemyspawnmanager is NULL!");
        }

        Inkstone = FindObjectOfType<Inkstone>();
        if (Inkstone == null)
        {
            Debug.LogError("Inkstone is NULL!");
        }

        SecretT = FindObjectOfType<Secret>();
        if (SecretT == null)
        {
            Debug.LogError("Secret is NULL");
        }

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }

        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }

        WM = FindObjectOfType<WaveManager>();
        if (WM == null)
        {
            Debug.LogError("Wave Manager is NULL");
        }

        SetScoreGiven();
    }

    private void OnEnable()
    {
        StartCoroutine("MalevolentSound");
        malevolentAnimator.SetBool("MalevolentDeath", false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
        if (Inkstone.Ink == 0 && playerDeathPlayed == false)
        {
            PlayerDeath();
            playerDeathPlayed = true;
        }
    }
    void SetScoreGiven()
    {
        int actualIntensity;
        if (WM.TEST_WaveActive == true)
        {
            actualIntensity = WM.TEST_WaveIntensity;
        }
        else
        {
            actualIntensity = GameManager.GameIntensity;
        }
        switch (actualIntensity)
        {
            case 2:
                scoreEnemy = scoreEnemy2;
                break;
            case 3:
                scoreEnemy = scoreEnemy3;
                break;
        }
    }
    private IEnumerator MalevolentSound()
    {
        while (this.isActiveAndEnabled)
        {
            yield return new WaitForSeconds(3.50f);
            AudioManager.Instance.PlaySound("MalevolentSpawn");
        }
    }

    public void Deathforsign()
    {
        malevolentAnimator.SetBool("MalevolentDeath", true);
        Inkstone.Ink += playerbehaviour.inkGained;
        enemyspawnmanager.enemykilled++;
        SecretT.bar += SecretT.charge;
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;
        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
        Invoke("TrueDeath", 3);
    }

    private void PlayerDeath()
    {
        malevolentAnimator.SetBool("MalevolentDeath", true);
        Invoke("TrueDeath", 3);
    }

    public void TrueDeath()
    {
        this.gameObject.SetActive(false);
    }
}
