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
    GameManager GameManager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] SignIntensity2Frightening;
    public GameObject[] SignIntensity2PlusFrightening;
    public float baseSpeed;
    public float startPosition;
    public float extrapointsoverdistance;
    public float startGrid;
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

        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("Gamemanager is NULL");
        }

        speed = baseSpeed;

        startPosition = transform.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Frightening();
        Enemymove();

        PointOverDistance();
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

        if (this.transform.localPosition.x > 4.24)
        {
            DeathForEndGrid();
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkstoneDamage;
        switch (GameManager.GameIntensity)
        {
            case 2:
                foreach (GameObject segno in SignIntensity2Frightening)
                {
                    segno.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject segno in SignIntensity2PlusFrightening)
                {
                    segno.SetActive(false);
                }
                break;
        }

        playerbehaviour.speed = playerSpeed;
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        SecretT.bar = 0;
        enemyspawnmanager.enemykilled = 0;
        switch (GameManager.GameIntensity)
        {
            case 2:
                foreach (GameObject segno in SignIntensity2Frightening)
                {
                    segno.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject segno in SignIntensity2PlusFrightening)
                {
                    segno.SetActive(false);
                }
                break;
        }

        playerbehaviour.speed = playerSpeed;
        AudioManager.Instance.PlaySound("PlayerGetsHit");


    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        switch (GameManager.GameIntensity)
        {
            case 2:
                foreach (GameObject segno in SignIntensity2Frightening)
                {
                    segno.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject segno in SignIntensity2PlusFrightening)
                {
                    segno.SetActive(false);
                }
                break;
        }

        playerbehaviour.speed = playerSpeed;

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
