using UnityEngine;

public class BufferEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 6;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    PointSystem pointsystem;
    Secret SecretT;
    GameManager GM;
    public int scoreEnemy;
    public GameObject[] signbufferenemy;
    public GameObject[] ToBuff;
    public int segnocorrispondente;
    public int link = 7;
    public float Boost = 5f;
    public float Reset = 0.8f;
    public float speed;
    public float endPosition;
    public float baseSpeed;
    public float BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject hat;
    [SerializeField]
    private ParticleSystem inkDeath;
    [SerializeField]
    private ParticleSystem buffPower;
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
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
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
                                if (NormalEnemy.speed == NormalEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    NormalEnemy.buffEffect.Play();
                                    NormalEnemy.speed = NormalEnemy.speed + Boost;
                                }

                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed == KamikazeEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    KamikazeEnemy.buffEffect.Play();
                                    KamikazeEnemy.speed = KamikazeEnemy.speed + Boost;
                                }

                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed == ArmoredEnemy.baseSpeed + GM.intensitySpeedIncrease || ArmoredEnemy.speed == ArmoredEnemy.baseSpeedMax + GM.intensitySpeedIncrease)
                                {
                                    ArmoredEnemy.buffEffect.Play();
                                    ArmoredEnemy.speed = ArmoredEnemy.speed + Boost;
                                }


                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();

                                if (UndiyngEnemy.speed == UndiyngEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    UndiyngEnemy.buffEffect.Play();
                                    UndiyngEnemy.speed = UndiyngEnemy.speed + Boost;
                                }

                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy FrighteningEnemy = enemy.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed == frighteningEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    FrighteningEnemy.buffEffect.Play();
                                    frighteningEnemy.speed = frighteningEnemy.speed + Boost;
                                }

                            }
                            break;
                        case 6:
                            {
                                BufferEnemy BufferEnemy = enemy.GetComponent<BufferEnemy>();

                                if (bufferEnemy.speed == bufferEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    bufferEnemy.speed = bufferEnemy.speed + Boost;
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
                                if (NormalEnemy.speed != NormalEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    NormalEnemy.buffEffect.Stop();
                                    NormalEnemy.speed = NormalEnemy.baseSpeed + GM.intensitySpeedIncrease;
                                }
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed != KamikazeEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    KamikazeEnemy.buffEffect.Stop();
                                    KamikazeEnemy.speed = KamikazeEnemy.baseSpeed + GM.intensitySpeedIncrease;
                                }

                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed != ArmoredEnemy.baseSpeed + GM.intensitySpeedIncrease && ArmoredEnemy.speed != ArmoredEnemy.baseSpeedMax + GM.intensitySpeedIncrease)
                                {
                                    if (ArmoredEnemy.armoredLife == 2)
                                    {
                                        ArmoredEnemy.speed = ArmoredEnemy.baseSpeed + GM.intensitySpeedIncrease;
                                    }
                                    else
                                    {
                                        ArmoredEnemy.speed = ArmoredEnemy.baseSpeedMax + GM.intensitySpeedIncrease;
                                    }
                                    ArmoredEnemy.buffEffect.Stop();
                                }
                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();
                                if (UndiyngEnemy.speed != UndiyngEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    UndiyngEnemy.speed = UndiyngEnemy.baseSpeed + GM.intensitySpeedIncrease;
                                    UndiyngEnemy.buffEffect.Stop();
                                }

                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed != frighteningEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.baseSpeed + GM.intensitySpeedIncrease;
                                    frighteningEnemy.buffEffect.Stop();
                                }

                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();

                                if (bufferEnemy.speed != bufferEnemy.baseSpeed + GM.intensitySpeedIncrease)
                                {
                                    bufferEnemy.speed = bufferEnemy.baseSpeed + GM.intensitySpeedIncrease;
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
        buffPower.Stop();
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


        this.gameObject.SetActive(false);
        enemy.GetComponent<Renderer>().material.color = Color.white;
        hat.GetComponent<Renderer>().material.color = Color.white;
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signbufferenemy)
        {
            segno.SetActive(false);
        }
        SpeedReset();
        AudioManager.Instance.StopSound("SingsongBuffer");
        AudioManager.Instance.PlaySound("EnemyDeath");
    }
}
