using UnityEngine;

public class FrighteningEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 5;
    [SerializeField]
    public float speed;
    public float playerSpeed;
    public float reduceSpeed;
    public int inkDamage;
    public int maxInkDamage;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signfrighteningenemy;
    public float baseSpeed;
    public float startPosition;
    public float extrapointsoverdistance;
    public float startGrid;
    public float BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject hair;
    [SerializeField]
    private ParticleSystem inkDeath;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem inkAbsorb;
    public float stopTime;

    #endregion

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

        speed = baseSpeed;

        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Frightening();
        Enemymove();
        PointOverDistance();
        if (!playerbehaviour.frightenedPlayer.isPlaying)
        {
            playerbehaviour.frightenedPlayer.Play();
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

        if (this.transform.localPosition.x > 2.5)
        {
            inkAbsorb.Play();
            Invoke("DeathForEndGrid", stopTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void DeathForEndGrid()
    {
        inkAbsorb.Stop();
        this.gameObject.SetActive(false);
        playerbehaviour.frightenedPlayer.Stop();
        Inkstone.Ink -= inkstoneDamage;
        foreach (GameObject segno in signfrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        playerbehaviour.frightenedPlayer.Stop();
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        enemyspawnmanager.enemykilled = 0;
        foreach (GameObject segno in signfrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;

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
        hair.GetComponent<Renderer>().material.color = Color.black;
        playerbehaviour.frightenedPlayer.Stop();
        Invoke("Death", BlackToDeath);
    }
    public void Death()
    {
        this.gameObject.SetActive(false);
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hair.GetComponent<Renderer>().material.color = Color.white;
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signfrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;

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
