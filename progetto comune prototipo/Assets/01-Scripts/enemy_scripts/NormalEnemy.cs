using System.Collections;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 0;
    [HideInInspector]
    public float speed;
    public int inkDamage;
    public int maxInkDamage;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Managercombo Managercombo;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] SignNormalYokai;
    public GameObject[] SignIntensity1Normal;
    public GameObject[] SignIntensity1PlusNormal;
    public float baseSpeed;
    [HideInInspector]
    public float startPosition;
    [HideInInspector]
    public float extrapointsoverdistance;
    [HideInInspector]
    public float startGrid;
    public float timeToDespawn = 2f;
    public Animator normalAnimator;
    private bool destinationReached;
    public float BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject band;
    [SerializeField]
    private ParticleSystem inkDeath;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem inkAbsorb;
    public float stopTime, waitTime;
    IEnumerator deathforendgrid;

    #endregion

    private void Start()
    {
        normalAnimator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        destinationReached = false;
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
            Debug.LogError("Gamemanager is NULL");
        }

        Managercombo = FindObjectOfType<Managercombo>();
        if (Managercombo == null)
        {
            Debug.LogError("Managercombo is NULL");
        }

        speed = baseSpeed;

        startPosition = transform.position.x;

        deathforendgrid = null;
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
        normalAnimator.SetFloat("CurrentPosition", transform.position.x);
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.localPosition.x > 3.75)
        {
            Invoke("DeathForEndGrid", timeToDespawn);
            AudioManager.Instance.PlaySound("Backwash");
            inkAbsorb.Play();
            destinationReached = true;
        }
    }


    public IEnumerator DeathForEndGrid()
    {
        yield return new WaitForSeconds(waitTime);
        inkAbsorb.Stop();
        playerbehaviour.ReceiveDamage(inkstoneDamage, 0);
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
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        band.GetComponent<Renderer>().material.color = Color.black;
        Invoke("DeathForCollision", BlackToDeath);

        playerbehaviour.ReceiveDamage(inkDamage, maxInkDamage);
        AudioManager.Instance.PlaySound("EnemyDeath");
    }

    public void DeathForCollision()
    {
        enemy.GetComponent<Renderer>().material.color = Color.white;
        band.GetComponent<Renderer>().material.color = Color.white;
        TrueDeath();
    }

    public void Deathforsign()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        band.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }

    private void Death()
    {
        enemy.GetComponent<Renderer>().material.color = Color.white;
        band.GetComponent<Renderer>().material.color = Color.white;
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
        foreach (GameObject segno in SignNormalYokai)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity1Normal)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity1PlusNormal)
        {
            segno.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
