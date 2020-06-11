using System.Collections;
using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 2;
    [SerializeField]
    public float speed, baseSpeedMax;
    public int inkDamage, maxInkDamage, inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    GameManager GM;
    public int scoreEnemy, armoredLife;
    public GameObject[] signarmoredenemy;
    public float baseSpeed, startPosition, extrapointsoverdistance, startGrid, BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject armor;
    [SerializeField]
    private GameObject bandana;
    [SerializeField]
    private ParticleSystem inkDeath;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem inkAbsorb;
    public float stopTime, waitTime;
    IEnumerator deathforendgrid;
    bool destinationReached;

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

        GM = FindObjectOfType<GameManager>();
        if (pointsystem == null)
        {
            Debug.LogError("GameManager is NULL");
        }

        speed = baseSpeed;

        startPosition = transform.position.x;

        armoredLife = 2;

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
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.localPosition.x > 3.75)
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
        Inkstone.Ink -= inkstoneDamage;
        foreach (GameObject segno in signarmoredenemy)
        {
            segno.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        enemyspawnmanager.enemykilled = 0;
        foreach (GameObject segno in signarmoredenemy)
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
        if (armoredLife == 2)
        {
            speed = baseSpeedMax + GM.intensitySpeedIncrease;
            armoredLife -= 1;
            foreach (GameObject segno in signarmoredenemy)
            {
                segno.SetActive(false);
            }
            int randomsegno = Random.Range(0, 6);
            signarmoredenemy[randomsegno].gameObject.SetActive(true);
        }
        else
        {
            inkDeath.Play();
            enemy.GetComponent<Renderer>().material.color = Color.black;
            armor.GetComponent<Renderer>().material.color = Color.black;
            bandana.GetComponent<Renderer>().material.color = Color.black;
            Invoke("Death", BlackToDeath);
        }
    }

    public void Death()
    {
        armoredLife = 2;
        enemyspawnmanager.enemykilled += 2;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signarmoredenemy)
        {
            segno.SetActive(false);
        }
        this.gameObject.SetActive(false);
        enemy.GetComponent<Renderer>().material.color = Color.white;
        armor.GetComponent<Renderer>().material.color = Color.white;
        bandana.GetComponent<Renderer>().material.color = Color.white;
        AudioManager.Instance.PlaySound("EnemyDeath");

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
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
