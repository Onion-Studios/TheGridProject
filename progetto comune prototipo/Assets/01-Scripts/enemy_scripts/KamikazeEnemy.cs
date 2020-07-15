using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 1;
    [SerializeField]
    public float speed;
    public int inkDamage;
    public int maxInkDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Managercombo Managercombo;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    WaveManager WM;
    [SerializeField]
    int scoreEnemy1;
    [SerializeField]
    int scoreEnemy2;
    [SerializeField]
    int scoreEnemy3;
    public int scoreEnemy;
    public GameObject[] SignIntensity1Kamikaze;
    public GameObject[] SignIntensity1PlusKamikaze;
    public GameObject[] SignIntensity2Kamikaze;
    public float baseSpeed;
    private Collider[] hitColliders;
    public float blastRadius;
    //public LayerMask explosionLayers;
    public float startPosition;
    public float extrapointsoverdistance;
    public float startGrid;
    public float explosionDelay;
    [SerializeField]
    private ParticleSystem explosion;
    public ParticleSystem buffEffect;
    public Animator kamikazeAnimator;
    [HideInInspector]
    public bool isBuffed;
    #endregion

    private void Awake()
    {
        kamikazeAnimator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        isBuffed = false;
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

        WM = FindObjectOfType<WaveManager>();
        if (WM == null)
        {
            Debug.LogError("Wave Manager is NULL");
        }

        startPosition = transform.position.x;

        SetScoreGiven();
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
        PointOverDistance();
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
            case 1:
                scoreEnemy = scoreEnemy1;
                break;
            case 2:
                scoreEnemy = scoreEnemy2;
                break;
            case 3:
                scoreEnemy = scoreEnemy3;
                break;
        }
    }
    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > 2)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        TrueDeath();
        ExplosionWork(this.transform.position);
        Invoke("ParentReassignment", explosionDelay);
    }

    public void Deathforgriglia()
    {
        if (playerbehaviour.invincibilityActive == false)
        {
            explosion.transform.SetParent(null);
            explosion.Play();
            playerbehaviour.ReceiveDamage(inkDamage, maxInkDamage, false);
            TrueDeath();
            ExplosionWork(this.transform.position);
            Invoke("ParentReassignment", explosionDelay);
        }
    }

    public void Deathforsign()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        enemyspawnmanager.enemykilled++;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        TrueDeath();
        ExplosionWork(this.transform.position);
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
        Invoke("ParentReassignment", explosionDelay);
    }

    public void ParentReassignment()
    {
        explosion.transform.SetParent(this.transform);
        explosion.transform.localPosition = Vector3.zero;
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

    void ExplosionWork(Vector3 explosionPoint)
    {
        //AudioManager.Instance.PlaySound("KamikazeExplosion");
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);
        foreach (Collider hitCol in hitColliders)
        {
            if (hitCol.gameObject.GetComponent<NormalEnemy>() != null)
            {
                hitCol.gameObject.GetComponent<NormalEnemy>().Deathforsign();
            }
            else if (hitCol.gameObject.GetComponent<KamikazeEnemy>() != null)
            {
                hitCol.gameObject.GetComponent<KamikazeEnemy>().Deathforsign();
            }
            else if (hitCol.gameObject.GetComponent<ArmoredEnemy>() != null)
            {
                hitCol.gameObject.GetComponent<ArmoredEnemy>().Deathforsign();
            }
            else if (hitCol.gameObject.GetComponent<FrighteningEnemy>() != null)
            {
                hitCol.gameObject.GetComponent<FrighteningEnemy>().Deathforsign();
            }
            else if (hitCol.gameObject.GetComponent<BufferEnemy>() != null)
            {
                hitCol.gameObject.GetComponent<BufferEnemy>().Deathforsign();
            }
        }
    }
    public void TrueDeath()
    {
        foreach (GameObject segno in SignIntensity1Kamikaze)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity1PlusKamikaze)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity2Kamikaze)
        {
            segno.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
