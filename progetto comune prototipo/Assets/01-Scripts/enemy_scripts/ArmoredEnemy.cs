using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 2;
    [SerializeField]
    public float speed;
    public float baseSpeedMax;
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
    public GameObject[] SignIntensity1Armored;
    public GameObject[] SignIntensity1PlusArmored;
    public GameObject[] SignIntensity2Armored;
    public int armoredLife = 2;
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
        Enemymove();

        PointOverDistance();
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > 4.24)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkstoneDamage;
        switch (GameManager.GameIntensity)
        {
            case 1:
                foreach (GameObject segno in SignIntensity1Armored)
                {
                    segno.SetActive(false);
                }
                break;
            case 2:
                foreach (GameObject segno in SignIntensity1PlusArmored)
                {
                    segno.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject segno in SignIntensity2Armored)
                {
                    segno.SetActive(false);
                }
                break;
        }
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
            case 1:
                foreach (GameObject segno in SignIntensity1Armored)
                {
                    segno.SetActive(false);
                }
                break;
            case 2:
                foreach (GameObject segno in SignIntensity1PlusArmored)
                {
                    segno.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject segno in SignIntensity2Armored)
                {
                    segno.SetActive(false);
                }
                break;
        }
        //AudioManager.Instance.PlaySound("PlayerGetsHit");
    }

    public void Deathforsign()
    {
        if (armoredLife == 2)
        {
            speed = baseSpeedMax;
            armoredLife -= 1;
            switch (GameManager.GameIntensity)
            {
                case 1:
                    foreach (GameObject segno in SignIntensity1Armored)
                    {
                        segno.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject segno in SignIntensity1PlusArmored)
                    {
                        segno.SetActive(false);
                    }
                    break;
                case 3:
                    foreach (GameObject segno in SignIntensity2Armored)
                    {
                        segno.SetActive(false);
                    }
                    break;
            }
            int randomsegno = Random.Range(0, 6);
            int randomsegnofour = Random.Range(0, 4);
            switch (GameManager.GameIntensity)
            {
                case 1:
                    SignIntensity1Armored[randomsegno].gameObject.SetActive(true);
                    break;
                case 2:
                    SignIntensity1PlusArmored[randomsegno].gameObject.SetActive(true);
                    break;
                case 3:
                    SignIntensity2Armored[randomsegnofour].gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            armoredLife = 2;
            enemyspawnmanager.enemykilled += 2;
            Inkstone.Ink += playerbehaviour.inkGained;
            SecretT.bar += SecretT.charge;
            switch (GameManager.GameIntensity)
            {
                case 1:
                    foreach (GameObject segno in SignIntensity1Armored)
                    {
                        segno.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject segno in SignIntensity1PlusArmored)
                    {
                        segno.SetActive(false);
                    }
                    break;
                case 3:
                    foreach (GameObject segno in SignIntensity2Armored)
                    {
                        segno.SetActive(false);
                    }
                    break;
            }
            this.gameObject.SetActive(false);
        }

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
