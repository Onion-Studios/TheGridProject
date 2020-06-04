using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 0;
    [SerializeField]
    public float speed;
    public int inkDamage;
    public int maxInkDamage;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signnormalenemy;
    public float baseSpeed;
    public float startPosition;
    public float extrapointsoverdistance;
    public float startGrid;
    public float timeToDespawn = 2f;
    private Animator normalAnimator;
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
    public float stopTime;

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



        speed = baseSpeed;

        startPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
        PointOverDistance();
        normalAnimator.SetFloat("CurrentPosition", transform.position.x);
    }

    public void Enemymove()
    {
        if (destinationReached == false)
        {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (this.transform.localPosition.x > 3.5)
        {
            destinationReached = true;
            Invoke("DeathForEndGrid", timeToDespawn);
            inkAbsorb.Play();
        }
    }

    public void DeathForEndGrid()
    {
        inkAbsorb.Stop();
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkstoneDamage;
        foreach (GameObject segno in signnormalenemy)
        {
            segno.SetActive(false);
        }


    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        enemyspawnmanager.enemykilled = 0;
        foreach (GameObject segno in signnormalenemy)
        {
            segno.SetActive(false);
        }

        if (SecretT.bar == 100)
        {
            AudioManager.Instance.PlaySound("PlayerGetsHit");
            SecretT.paintParticles.Stop();
            SecretT.active = false;
            SecretT.currentTime = SecretT.timeMax;
            SecretT.symbol.SetActive(false);
        }
        else
        {
            // Insert THUD Sound
        }

        SecretT.bar = 0;

        AudioManager.Instance.PlaySound("EnemyDeath");

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
        this.gameObject.SetActive(false);
        enemy.GetComponent<Renderer>().material.color = Color.white;
        band.GetComponent<Renderer>().material.color = Color.white;
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signnormalenemy)
        {
            segno.SetActive(false);
        }


        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
        AudioManager.Instance.PlaySound("EnemyDeath");

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
