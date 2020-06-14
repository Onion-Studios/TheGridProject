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
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signkamikazenemy;
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
    #endregion

    private void Awake()
    {
        kamikazeAnimator = GetComponentInChildren<Animator>();
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


        speed = baseSpeed;

        startPosition = transform.position.x;

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

        if (this.transform.localPosition.x > 2)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        this.gameObject.SetActive(false);
        Inkstone.Ink -= 10;
        foreach (GameObject segno in signkamikazenemy)
        {
            segno.SetActive(false);
        }
        ExplosionWork(this.transform.position);
        Invoke("ParentReassignment", explosionDelay);
    }

    public void Deathforgriglia()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        enemyspawnmanager.enemykilled = 0;
        foreach (GameObject segno in signkamikazenemy)
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
        ExplosionWork(this.transform.position);
        Invoke("ParentReassignment", explosionDelay);
    }

    public void Deathforsign()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signkamikazenemy)
        {
            segno.SetActive(false);
        }

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

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }

    void ExplosionWork(Vector3 explosionPoint)
    {
        AudioManager.Instance.PlaySound("KamikazeExplosion");
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

}
