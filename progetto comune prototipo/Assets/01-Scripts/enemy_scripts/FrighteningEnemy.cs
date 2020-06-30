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

        speed = baseSpeed;

        startPosition = transform.position.x;

        deathforendgrid = null;

        destinationReached = false;
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
            Invoke("DeathForEndGrid", stopTime);
            destinationReached = true;
        }
    }

    public IEnumerator DeathForEndGrid()
    {
        yield return new WaitForSeconds(waitTime);
        inkAbsorb.Stop();
        playerbehaviour.frightenedPlayer.Stop();
        playerbehaviour.ReceiveDamage(inkstoneDamage, 0);
        foreach (GameObject segno in SignIntensity2Frightening)

        {

            segno.SetActive(false);

        }
        foreach (GameObject segno in SignIntensity2PlusFrightening)

                {
                    segno.SetActive(false);
                }
        playerbehaviour.speed = playerSpeed;
        Die();
    }

    void Die()
    {
        if (deathforendgrid != null)
        {
            StopCoroutine(deathforendgrid);
            deathforendgrid = null;
        }
        this.gameObject.SetActive(false);
    }

    public void Deathforgriglia()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hair.GetComponent<Renderer>().material.color = Color.black;
        playerbehaviour.frightenedPlayer.Stop();
        Invoke("DeathForCollision", BlackToDeath);

        playerbehaviour.frightenedPlayer.Stop();
        playerbehaviour.ReceiveDamage(inkDamage, maxInkDamage);
        foreach (GameObject segno in SignIntensity2Frightening)

        {

            segno.SetActive(false);

        }
        foreach (GameObject segno in SignIntensity2PlusFrightening)

        {

            segno.SetActive(false);

        }
        playerbehaviour.speed = playerSpeed;
        AudioManager.Instance.PlaySound("EnemyDeath");
    }

    public void DeathForCollision()
    {
        this.gameObject.SetActive(false);
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hair.GetComponent<Renderer>().material.color = Color.white;
    }
    public void Deathforsign()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hair.GetComponent<Renderer>().material.color = Color.black;
        playerbehaviour.frightenedPlayer.Stop();
        Invoke("Death", BlackToDeath);
    }
    public void Death()
    {
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hair.GetComponent<Renderer>().material.color = Color.white;
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in SignIntensity2Frightening)

        {

            segno.SetActive(false);

        }
        foreach (GameObject segno in SignIntensity2PlusFrightening)

        {

            segno.SetActive(false);

        }

        playerbehaviour.speed = playerSpeed;

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
        AudioManager.Instance.PlaySound("EnemyDeath");
        Die();
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
}
