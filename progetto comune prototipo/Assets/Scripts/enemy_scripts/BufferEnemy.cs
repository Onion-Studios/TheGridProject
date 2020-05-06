using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 7;
    public bool Buff;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    PointSystem pointsystem;
    Secret SecretT;
    public int scoreEnemy;
    public GameObject[] segnibufferenemy;
    public GameObject[] ToBuff;
    public int segnocorrispondente;
    public int link = 8;
    public float Boost = 5f;
    public float Reset = 0.8f;
    public float speed;
    public float endPosition;
    public float baseSpeed;
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
            SpeedBoost();
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
            foreach (GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                                if (NormalEnemy.speed == NormalEnemy.baseSpeed)
                                {
                                    NormalEnemy.speed = NormalEnemy.speed * Boost;

                                }

                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed == KamikazeEnemy.baseSpeed)
                                {
                                    KamikazeEnemy.speed = KamikazeEnemy.speed * Boost;

                                }

                            }
                            break;
                        case 2:
                            {
                                GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                                if (GoldenEnemy.speed == GoldenEnemy.baseSpeed)
                                {
                                    GoldenEnemy.speed = GoldenEnemy.speed * Boost;

                                }

                            }
                            break;
                        case 3:
                            {
                                ArmoredEnemy ArmoredEnemy = nemico.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed == ArmoredEnemy.baseSpeed)
                                {
                                    ArmoredEnemy.speed = ArmoredEnemy.speed * Boost;

                                }

                            }
                            break;
                        case 4:
                            {
                                UndyingEnemy UndiyngEnemy = nemico.GetComponent<UndyingEnemy>();

                                if (UndiyngEnemy.speed == UndiyngEnemy.baseSpeed)
                                {
                                    UndiyngEnemy.speed = UndiyngEnemy.speed * Boost;
                                }

                            }
                            break;
                        case 6:
                            {
                                FrighteningEnemy frighteningEnemy = nemico.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed == frighteningEnemy.baseSpeed)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.speed * Boost;
                                }

                            }
                            break;
                        case 7:
                            {
                                BufferEnemy bufferEnemy = nemico.GetComponent<BufferEnemy>();

                                if (bufferEnemy.speed == bufferEnemy.baseSpeed)
                                {
                                   bufferEnemy.speed = bufferEnemy.speed * Boost;
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
       
        for (int i = 0; i > link; i++)
        {
            foreach (GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                                if (NormalEnemy.speed != NormalEnemy.baseSpeed)
                                {
                                    NormalEnemy.speed = NormalEnemy.baseSpeed;

                                }
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                                if (KamikazeEnemy.speed != KamikazeEnemy.baseSpeed)
                                {
                                    KamikazeEnemy.speed = KamikazeEnemy.baseSpeed;

                                }

                            }
                            break;
                        case 2:
                            {
                                GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                                if (GoldenEnemy.speed != GoldenEnemy.baseSpeed)
                                {
                                    GoldenEnemy.speed = GoldenEnemy.baseSpeed;

                                }

                            }
                            break;
                        case 3:
                            {
                                ArmoredEnemy ArmoredEnemy = nemico.GetComponent<ArmoredEnemy>();
                                if (ArmoredEnemy.speed != ArmoredEnemy.baseSpeed)
                                {
                                    ArmoredEnemy.speed = ArmoredEnemy.baseSpeed;

                                }

                            }
                            break;
                        case 4:
                            {
                                UndyingEnemy UndiyngEnemy = nemico.GetComponent<UndyingEnemy>();
                                if (UndiyngEnemy.speed != UndiyngEnemy.baseSpeed)
                                {
                                    UndiyngEnemy.speed = UndiyngEnemy.baseSpeed;

                                }

                            }
                            break;
                        case 6:
                            {
                                FrighteningEnemy frighteningEnemy = nemico.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed != frighteningEnemy.baseSpeed)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.baseSpeed;
                                }

                            }
                            break;
                        case 7:
                            {
                                BufferEnemy bufferEnemy = nemico.GetComponent<BufferEnemy>();

                                if (bufferEnemy.speed != bufferEnemy.baseSpeed)
                                {
                                    bufferEnemy.speed = bufferEnemy.baseSpeed;
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

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier; 


        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        SecretT.barra += SecretT.carica;
        foreach (GameObject segno in segnibufferenemy)
        {
            segno.SetActive(false);
        }

        SpeedReset();


    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5.0f);
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            SpeedBoost();
            Debug.Log("Boost");
            yield return new WaitForSeconds(2.0f);
            SpeedReset();
            Debug.Log("Reset");
        }
    }
}
