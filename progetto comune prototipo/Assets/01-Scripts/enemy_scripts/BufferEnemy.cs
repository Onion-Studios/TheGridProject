﻿using UnityEngine;

public class BufferEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 6;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Managercombo Managercombo;
    Inkstone Inkstone;
    PointSystem pointsystem;
    Secret SecretT;
    WaveManager WM;
    [SerializeField]
    int scoreEnemy1;
    [SerializeField]
    int scoreEnemy2;
    [SerializeField]
    int scoreEnemy3;
    public int scoreEnemy;
    public GameObject[] SignIntensity1Buffer;
    public GameObject[] SignIntensity1PlusBuffer;
    public GameObject[] SignIntensity2Buffer;
    public GameObject[] ToBuff;
    public int segnocorrispondente, link = 7;
    public float Boost, speed, endPosition, baseSpeed, BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject hat;
    [SerializeField]
    private ParticleSystem inkDeath;
    [SerializeField]
    private ParticleSystem buffPower;
    public Animator bufferAnimator;
    #endregion

    private void Start()
    {
        bufferAnimator = GetComponentInChildren<Animator>();
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

        Managercombo = FindObjectOfType<Managercombo>();
        if (Managercombo == null)
        {
            Debug.LogError("Managercombo is NULL");
        }

        GameManager = FindObjectOfType<GameManager>();
        if (pointsystem == null)
        {
            Debug.LogError("GameManager is NULL");
        }

        WM = FindObjectOfType<WaveManager>();
        if (WM == null)
        {
            Debug.LogError("Wave Manager is NULL");
        }

        SetScoreGiven();
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
        bufferAnimator.SetFloat("CurrentPosition", transform.position.x);
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
        if (this.transform.localPosition.x > endPosition)
        {
            buffPower.Play();
            SpeedBoost();
            if (AudioManager.Instance.IsPlaying("SingsongBuffer") == false)
            {
                AudioManager.Instance.PlaySound("SingsongBuffer");
            }
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void SpeedBoost()
    {
        for (int i = 0; i < link; i++)
        {
            foreach (GameObject enemy in enemyspawnmanager.poolenemy[i])
            {
                if (enemy.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                                if (NormalEnemy.speed == NormalEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    NormalEnemy.buffEffect.Play();
                                    NormalEnemy.speed += Boost;
                                }
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed == KamikazeEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    KamikazeEnemy.buffEffect.Play();
                                    KamikazeEnemy.speed += Boost;
                                }
                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed == ArmoredEnemy.baseSpeed + GameManager.intensitySpeedIncrease || ArmoredEnemy.speed == ArmoredEnemy.baseSpeedMax + GameManager.intensitySpeedIncrease)
                                {
                                    ArmoredEnemy.buffEffect.Play();
                                    ArmoredEnemy.speed += Boost;
                                }
                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();

                                if (UndiyngEnemy.speed == UndiyngEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    //UndiyngEnemy.buffEffect.Play();
                                    UndiyngEnemy.speed += Boost;
                                }
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy FrighteningEnemy = enemy.GetComponent<FrighteningEnemy>();

                                if (FrighteningEnemy.speed == FrighteningEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    FrighteningEnemy.buffEffect.Play();
                                    FrighteningEnemy.speed += Boost;
                                }
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy BufferEnemy = enemy.GetComponent<BufferEnemy>();

                                if (BufferEnemy.speed == BufferEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    BufferEnemy.speed += Boost;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }

    public void SpeedReset()
    {
        for (int i = 0; i < link; i++)
        {
            foreach (GameObject enemy in enemyspawnmanager.poolenemy[i])
            {
                if (enemy.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                                if (NormalEnemy.speed != NormalEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    NormalEnemy.buffEffect.Stop();
                                    NormalEnemy.speed = NormalEnemy.baseSpeed + GameManager.intensitySpeedIncrease;
                                }
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed != KamikazeEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    KamikazeEnemy.buffEffect.Stop();
                                    KamikazeEnemy.speed = KamikazeEnemy.baseSpeed + GameManager.intensitySpeedIncrease;
                                }
                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed != ArmoredEnemy.baseSpeed + GameManager.intensitySpeedIncrease && ArmoredEnemy.speed != ArmoredEnemy.baseSpeedMax + GameManager.intensitySpeedIncrease)
                                {
                                    if (ArmoredEnemy.armoredLife == 2)
                                    {
                                        ArmoredEnemy.speed = ArmoredEnemy.baseSpeed + GameManager.intensitySpeedIncrease;
                                    }
                                    else
                                    {
                                        ArmoredEnemy.speed = ArmoredEnemy.baseSpeedMax + GameManager.intensitySpeedIncrease;
                                    }
                                    ArmoredEnemy.buffEffect.Stop();
                                }
                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();
                                if (UndiyngEnemy.speed != UndiyngEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    UndiyngEnemy.speed = UndiyngEnemy.baseSpeed + GameManager.intensitySpeedIncrease;

                                    UndiyngEnemy.buffEffect.Stop();
                                }
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed != frighteningEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.baseSpeed + GameManager.intensitySpeedIncrease;

                                    frighteningEnemy.buffEffect.Stop();
                                }
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();

                                if (bufferEnemy.speed != bufferEnemy.baseSpeed + GameManager.intensitySpeedIncrease)
                                {
                                    bufferEnemy.speed = bufferEnemy.baseSpeed + GameManager.intensitySpeedIncrease;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }

    public void Deathforsign()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hat.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    public void Death()
    {
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hat.GetComponent<Renderer>().material.color = Color.white;

        enemyspawnmanager.enemykilled++;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;

        AudioManager.Instance.PlaySound("EnemyDeath");

        TrueDeath();
    }

    public void TrueDeath()
    {
        foreach (GameObject segno in SignIntensity1Buffer)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity1PlusBuffer)
        {
            segno.SetActive(false);
        }
        foreach (GameObject segno in SignIntensity2Buffer)
        {
            segno.SetActive(false);
        }
        buffPower.Stop();
        SpeedReset();
        AudioManager.Instance.StopSound("SingsongBuffer");
        this.gameObject.SetActive(false);
    }
}
