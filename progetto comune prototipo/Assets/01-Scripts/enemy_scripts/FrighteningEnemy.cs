using System.Collections;
using UnityEngine;

public class FrighteningEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 5;
    [SerializeField]
    public float speed, playerSpeed, reduceSpeed;
    public int inkDamage, maxInkDamage, inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Managercombo Managercombo;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    WaveManager WM;
    [SerializeField]
    int scoreEnemy2;
    [SerializeField]
    int scoreEnemy3;
    public int scoreEnemy;
    public GameObject[] signfrighteningenemy;
    public float baseSpeed, startPosition, extrapointsoverdistance, startGrid, BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject hair;
    [SerializeField]
    private ParticleSystem inkDeath;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem inkAbsorb;
    public float stopTime, waitTime;
    IEnumerator deathforendgrid;
    bool destinationReached;
    public Animator frighteningAnimator;

    public GameObject[] SignIntensity2Frightening;
    public GameObject[] SignIntensity2PlusFrightening;
    #endregion

    private void Start()
    {
        frighteningAnimator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
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

        Managercombo = FindObjectOfType<Managercombo>();
        if (Managercombo == null)
        {
            Debug.LogError("Managercombo is NULL");
        }

        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("Gamemanager is NULL");
        }

        WM = FindObjectOfType<WaveManager>();
        if (WM == null)
        {
            Debug.LogError("Wave Manager is NULL");
        }

        startPosition = transform.position.x;

        deathforendgrid = null;

        destinationReached = false;

        SetScoreGiven();
    }

    private void OnDisable()
    {
        if (deathforendgrid != null)
        {
            StopCoroutine(deathforendgrid);
            deathforendgrid = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Frightening();
        if (destinationReached == false)
        {
            Enemymove();
        }
        else
        {
            if (deathforendgrid == null)
            {
                deathforendgrid = DeathForEndGrid();
                StartCoroutine(deathforendgrid);
            }
        }
        PointOverDistance();
        if (!playerbehaviour.frightenedPlayer.isPlaying)
        {
            playerbehaviour.frightenedPlayer.Play();
        }
        frighteningAnimator.SetFloat("CurrentPosition", transform.position.x);
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
    public void Frightening()
    {
        if (reduceSpeed != playerbehaviour.speed)
        {
            playerbehaviour.speed = reduceSpeed;
        }

    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.localPosition.x > 2.75)
        {
            inkAbsorb.Play();
            AudioManager.Instance.PlaySound("Backwash");
            Invoke("DeathForEndGrid", stopTime);
            destinationReached = true;
        }
    }

    public IEnumerator DeathForEndGrid()
    {
        yield return new WaitForSeconds(waitTime);
        inkAbsorb.Stop();
        playerbehaviour.ReceiveDamage(inkstoneDamage, 0, false);
        foreach (GameObject segno in SignIntensity2Frightening)

        {

            segno.SetActive(false);

        }
        foreach (GameObject segno in SignIntensity2PlusFrightening)

        {
            segno.SetActive(false);
        }
        Die();
    }

    void Die()
    {
        if (deathforendgrid != null)
        {
            StopCoroutine(deathforendgrid);
            deathforendgrid = null;
        }
        TrueDeath();
    }

    public void Deathforgriglia()
    {
        if (playerbehaviour.invincibilityActive == false)
        {
            inkDeath.Play();
            enemy.GetComponent<Renderer>().material.color = Color.black;
            hair.GetComponent<Renderer>().material.color = Color.black;
            Invoke("DeathForCollision", BlackToDeath);

            playerbehaviour.ReceiveDamage(inkDamage, maxInkDamage, false);

            AudioManager.Instance.PlaySound("EnemyDeath");
        }
    }

    public void DeathForCollision()
    {
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hair.GetComponent<Renderer>().material.color = Color.white;
        TrueDeath();
    }
    public void Deathforsign()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hair.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    public void Death()
    {
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hair.GetComponent<Renderer>().material.color = Color.white;
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
        AudioManager.Instance.PlaySound("EnemyDeath");
        TrueDeath();
    }

    void PointOverDistance()
    {
        if (this.transform.position.x < startGrid)
        {
            extrapointsoverdistance = scoreEnemy + scoreEnemy / (Mathf.Abs(startPosition) - Mathf.Abs(startGrid)) * transform.position.x;
        }
        else
        {
            extrapointsoverdistance = scoreEnemy;
        }
    }
    public void TrueDeath()
    {
        foreach (GameObject segno in SignIntensity2Frightening)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity2PlusFrightening)
        {
            segno.SetActive(false);
        }
        playerbehaviour.speed = playerSpeed;
        playerbehaviour.frightenedPlayer.Stop();
        this.gameObject.SetActive(false);
        playerbehaviour.hitOnce = false;
    }
}
