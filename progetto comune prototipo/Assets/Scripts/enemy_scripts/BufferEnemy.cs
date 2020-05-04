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
    public int scoreEnemy;
    public GameObject[] segnibufferenemy;
    public GameObject[] ToBuff;
    public int segnocorrispondente;
    public int link = 5;
    public float Boost = 5f;
    public float Reset = 0.8f;
    public float speed;
    public float endPosition;
    public float basespeed;
    #endregion

    // Start is called before the first frame update
    void Start()
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

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }

        //StartCoroutine(Test());

        speed = basespeed;
       
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
        for(int i=0; i < link; i++)
        {
            foreach(GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy==true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                                NormalEnemy.speed = NormalEnemy.speed * Boost;
                               
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                                KamikazeEnemy.speed = KamikazeEnemy.speed * Boost;
                             
                            }
                            break;
                        case 2:
                            {
                                GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                                GoldenEnemy.speed = GoldenEnemy.speed * Boost;
                            
                            }
                            break;
                        case 3:
                            {
                                ArmoredEnemy ArmoredEnemy = nemico.GetComponent<ArmoredEnemy>();
                                ArmoredEnemy.speed = ArmoredEnemy.speed * Boost;
                                
                            }
                            break;
                        case 4:
                            {
                                UndyingEnemy UndiyngEnemy = nemico.GetComponent<UndyingEnemy>();
                                UndiyngEnemy.speed = UndiyngEnemy.speed * Boost;
                          
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = nemico.GetComponent<FrighteningEnemy>();

                                if(frighteningEnemy.speed == frighteningEnemy.baseSpeed)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.speed * Boost;
                                }
                                
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = nemico.GetComponent<BufferEnemy>();
                                if(bufferEnemy.speed == bufferEnemy.basespeed)
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
        for (int i = 0; i < link; i++)
        {
            foreach (GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy == true)
                {
                    switch(i)
                    {
                        case 0:
                            {
                                NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                                NormalEnemy.speed = NormalEnemy.speed * (1/Boost);
                              
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                                KamikazeEnemy.speed = KamikazeEnemy.speed * (1 / Boost);
                               
                            }
                            break;
                        case 2:
                            {
                                GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                                GoldenEnemy.speed = GoldenEnemy.speed * (1 / Boost);
                               
                            }
                            break;
                        case 3:
                            {
                                ArmoredEnemy ArmoredEnemy = nemico.GetComponent<ArmoredEnemy>();
                                ArmoredEnemy.speed = ArmoredEnemy.speed * (1 / Boost);
                               
                            }
                            break;
                        case 4:
                            {
                                UndyingEnemy UndiyngEnemy = nemico.GetComponent<UndyingEnemy>();
                                UndiyngEnemy.speed = UndiyngEnemy.speed * (1 / Boost);
                              
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = nemico.GetComponent<FrighteningEnemy>();

                                if (frighteningEnemy.speed != frighteningEnemy.baseSpeed)
                                {
                                    frighteningEnemy.speed = frighteningEnemy.baseSpeed;
                                }
                              
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = nemico.GetComponent<BufferEnemy>();
                                if(bufferEnemy.speed != bufferEnemy.basespeed)
                                {
                                    bufferEnemy.speed = bufferEnemy.basespeed;
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
        SpeedReset();
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        foreach (GameObject segno in segnibufferenemy)
        {
            segno.SetActive(false);
        }

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
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
