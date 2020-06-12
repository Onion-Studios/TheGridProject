using UnityEngine;

public class UndyingEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 3;
    [SerializeField]
    public float speed;
    public int inkDamage;
    public int maxInkDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] SignIntensity3Undying;
    public float endPosition;
    public float currentTime;
    public float maxTime;
    public float attackTimer;
    public float maxAttacktimer;
    public bool repelled;
    private bool undyingStopped;
    public Vector3 startingPosition;
    private Vector3 waveSlashPosition;
    public float pushSpeed;
    public float baseSpeed;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem undyingSlash;
    [SerializeField]
    private ParticleSystem undyingSlashWave;
    [SerializeField]
    private float speedSlashWave;
    [SerializeField]
    private float stopSlashWaveTime;
    private bool stopSlashPlayed;
    private bool destinationReached;
    public int laneID;
    private Animator undyingAnimator;
    private bool alreadyDead;

    #endregion

    private void Awake()
    {
        waveSlashPosition = GetComponentsInChildren<Transform>()[2].localPosition;
        undyingAnimator = GetComponentInChildren<Animator>();
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



        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("Gamemanager is NULL");
        }

        speed = baseSpeed;

        currentTime = maxTime;
        repelled = false;
        alreadyDead = false;
        startingPosition = this.transform.localPosition;
        undyingAnimator.SetBool("UndyingDeath", false);
        /*if (AudioManager.Instance.IsPlaying("UndyingWarcry") == false)
        {
            AudioManager.Instance.PlaySound("UndyingWarcry");
        }*/
    }


    // Update is called once per frame
    void Update()
    {

        switch (laneID)
        {
            case 0:
                undyingSlashWave.transform.rotation = Quaternion.Euler(0, -45, 0);
                StopSlashPlayed();
                break;
            case 1:
                undyingSlashWave.transform.rotation = Quaternion.Euler(0, -30, 0);
                StopSlashPlayed();
                break;
            case 2:
                StopSlashPlayed();
                break;
            case 3:
                undyingSlashWave.transform.rotation = Quaternion.Euler(0, 30, 0);
                StopSlashPlayed();
                break;
            case 4:
                undyingSlashWave.transform.rotation = Quaternion.Euler(0, 45, 0);
                StopSlashPlayed();
                break;
            default:
                break;
        }


        DeathForTimer();

        if (repelled == false)
        {

            Enemymove();
        }
        else
        {
            UndyingRepelled();
        }



    }

    private void StopSlashPlayed()
    {
        if (stopSlashPlayed == false)
        {
            undyingSlashWave.transform.Translate(speedSlashWave, 0, 0);
            if (destinationReached)
            {
                Invoke("StopSlash", stopSlashWaveTime);
            }
        }
    }

    private void StopSlash()
    {
        undyingSlashWave.Stop();
        stopSlashPlayed = true;
    }

    public void Enemymove()
    {

        if (this.transform.localPosition.x > endPosition)
        {
            StartGridAttack();
            destinationReached = true;
        }
        else if (undyingAnimator.GetBool("UndyingDeath") == false && undyingStopped == false)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            destinationReached = false;
        }
    }



    public void StartGridAttack()
    {
        if (attackTimer > 0)
        {
            attackTimer -= 1 * Time.deltaTime;
        }

        if (attackTimer < 0)
        {
            attackTimer = 0;
        }

        if (attackTimer == 0 && undyingAnimator.GetBool("UndyingDeath") == false)
        {
            undyingAnimator.SetBool("Attacking", true);
            Invoke("AnimationDelayAttack", 0.5f);
            undyingSlashWave.transform.localPosition = waveSlashPosition;
            stopSlashPlayed = false;
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
            undyingSlash.Play();
            undyingSlashWave.Play();
            attackTimer = maxAttacktimer;
            Inkstone.Ink -= inkDamage;
            Inkstone.maxInk -= maxInkDamage;
            Inkstone.Ink += playerbehaviour.inkGained;
            SecretT.bar = 0;
            enemyspawnmanager.enemykilled = 0;
        }
    }

    private void AnimationDelayAttack()
    {
        undyingAnimator.SetBool("Attacking", false);
    }

    public void UndyingRepelled()
    {
        if (undyingAnimator.GetBool("UndyingDeath") == false)
        {
            transform.Translate(Vector3.left * pushSpeed * Time.deltaTime);
        }
        if (this.transform.localPosition.x < startingPosition.x + 2)
        {
            repelled = false;
            undyingStopped = true;
            Invoke("RestartMovement", 1f);
            speed = baseSpeed;
            undyingAnimator.SetBool("IsRepelled", false);
        }
    }

    private void RestartMovement()
    {
        undyingStopped = false;
    }

    public void Deathforsign()
    {
        repelled = true;
        undyingAnimator.SetBool("IsRepelled", true);
        attackTimer = 0;

    }


    public void DeathForTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        if (currentTime < 0)
        {
            currentTime = 0;
        }

        if (currentTime == 0)
        {
            undyingAnimator.SetBool("UndyingDeath", true);
            Invoke("Death", 2.6f);
        }
    }

    private void Death()
    {
        if (alreadyDead == false)
        {
            alreadyDead = true;
            this.gameObject.SetActive(false);

            attackTimer = 0;
            currentTime = maxTime;
            enemyspawnmanager.enemykilled += 1;
            Inkstone.Ink += playerbehaviour.inkGained;
            SecretT.bar += SecretT.charge;
            if (GameManager.GameIntensity == 3)

            {

                foreach (GameObject segno in SignIntensity3Undying)

                {

                    segno.SetActive(false);

                }

            }

            pointsystem.currentTimer = pointsystem.maxTimer;
            pointsystem.countercombo++;

            pointsystem.Combo();

            pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
        }
    }
}
