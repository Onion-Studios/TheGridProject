﻿using UnityEngine;

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
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signundyingenemy;
    public float endPosition;
    public float currentTime;
    public float maxTime;
    public float attackTimer;
    public float maxAttacktimer;
    public bool repelled;
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
    private int count;
    private bool destinationReached;
    #endregion

    private void Start()
    {
        waveSlashPosition = GetComponentsInChildren<Transform>()[2].localPosition;
        Debug.Log(GetComponentsInChildren<Transform>()[2].localPosition);
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

        currentTime = maxTime;
        repelled = false;
        startingPosition = this.transform.localPosition;
        if (AudioManager.Instance.IsPlaying("UndyingWarcry") == false)
        {
            AudioManager.Instance.PlaySound("UndyingWarcry");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (stopSlashPlayed == false)
        {
            undyingSlashWave.transform.Translate(speedSlashWave, 0, 0);
            if (destinationReached)
            {
                Invoke("StopSlash", stopSlashWaveTime);
            }
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

    private void StopSlash()
    {
        undyingSlashWave.Stop();
        stopSlashPlayed = true;
        count++;
        Debug.Log(count);
    }

    public void Enemymove()
    {

        if (this.transform.localPosition.x > endPosition)
        {
            DeathForStartGrid();
            destinationReached = true;
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            destinationReached = false;
        }
    }



    public void DeathForStartGrid()
    {
        //this.gameObject.SetActive(false);


        if (attackTimer > 0)
        {
            attackTimer -= 1 * Time.deltaTime;
        }

        if (attackTimer < 0)
        {
            attackTimer = 0;
        }

        if (attackTimer == 0)
        {
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

    public void UndyingRepelled()
    {
        transform.Translate(Vector3.left * pushSpeed * Time.deltaTime);
        if (this.transform.localPosition.x < startingPosition.x)
        {
            repelled = false;
            speed = baseSpeed;
        }
    }

    public void Deathforsign()
    {
        repelled = true;
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
            this.gameObject.SetActive(false);
            attackTimer = 0;
            currentTime = maxTime;
            enemyspawnmanager.enemykilled += 1;
            Inkstone.Ink += playerbehaviour.inkGained;
            SecretT.bar += SecretT.charge;
            foreach (GameObject segno in signundyingenemy)
            {
                segno.SetActive(false);

            }

            pointsystem.currentTimer = pointsystem.maxTimer;
            pointsystem.countercombo++;

            pointsystem.Combo();

            pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
        }
    }
}
