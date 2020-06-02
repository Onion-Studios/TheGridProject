using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartEndSequence : MonoBehaviour
{
    #region VARIABLES
    Playerbehaviour playerbehaviour;
    GameManager GM;
    PointSystem pointSystem;
    Enemyspawnmanager enemyspawnmanager;
    int startSequencePosition;
    int endSequencePosition;
    public GameObject[] light;
    public float activateLight;
    public bool starting, ending, switchui;
    IEnumerator playerLight;
    IEnumerator lightsON;
    IEnumerator lightsOFF;
    public float lightsStopTime;
    public float closedTime;
    public GameObject tenda, tenda2;
    public float curtainspeed;
    public Vector3 closecurtain;
    public Vector3 opencurtain1, opencurtain2;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    public Vector3 centerGrid;
    bool soundNotLooping;
    public int enemynumber;
    #endregion

    void Awake()
    {
        startSequencePosition = 0;
        endSequencePosition = 0;
        starting = true;
        ending = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("Playerbehaviour is NULL!");
        }

        GM = FindObjectOfType<GameManager>();
        if (GM == null)
        {
            Debug.LogError("GameMaster is NULL!");
        }

        pointSystem = FindObjectOfType<PointSystem>();
        if(pointSystem == null)
        {
            Debug.LogError("PointSystem is NULL!");
        }

        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL!");
        }

        playerLight = null;
        switchui = true;
        lightsON = null;

        opencurtain1 = tenda.transform.position;
        opencurtain2 = tenda2.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        StartSequence();
        
    }


    void StartSequence()
    {
        switch (startSequencePosition)
        {
            case 0:
                if (playerLight == null)
                {
                    playerLight = PlayerLight();
                    StartCoroutine(playerLight);
                }
                break;
            case 1:
                if (playerLight != null)
                {
                    StopCoroutine(playerLight);
                    playerLight = null;
                }
                Bowing();
                break;
            case 2:
                SwitchUI();
                break;
            case 3:
                CloseCurtains();
                break;
            case 4:
                if (lightsON == null)
                {
                    lightsON = LightsON();
                    StartCoroutine(lightsON);
                }
                break;
            case 5:
                if (lightsON != null)
                {
                    StopCoroutine(lightsON);
                    lightsON = null;
                }
                PlayerToCenter();
                break;
            case 6:
                OpenCurtains();
                break;
            case 7:
                SwitchUI();
                break;
            case 8:
                StartUP();
                break;

        }

    }

   public void EndSequence()
   {
        switch (endSequencePosition)
        {
            case 0:
                StopAllEnemies();
                break;
            case 1:
                if(lightsOFF == null)
                {
                    lightsOFF = LightsOFF();
                    StartCoroutine(lightsOFF);
                }
                break;
            case 2:
                if(lightsOFF != null)
                {
                    StopCoroutine(lightsOFF);
                    lightsOFF = null;
                }
                Seppuku();
                break;
            case 3:
                SwitchUI();
                break;
            case 4:
                CloseCurtains();
                break;
            case 5:
                GameOver();
                break;
        }
   }

    void GameOver()
    {
        Inkstone.FinalScore = (int)pointSystem.score;
        SceneManager.LoadScene(3);
    }

    void StopAllEnemies()
    {
        ending = true;
        StopEnemiesMovement();
        endSequencePosition++;

    }

    void StopEnemiesMovement()
    {
        for (int i = 0; i < enemynumber; i++)
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
                                NormalEnemy.speed = 0;
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                KamikazeEnemy.speed = 0;
                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                ArmoredEnemy.speed = 0;
                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();
                                UndiyngEnemy.speed = 0;
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                                frighteningEnemy.speed = 0;
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                                bufferEnemy.speed = 0;
                            }
                            break;

                    }
                }
            }
        }
    }

    IEnumerator LightsOFF()
    {
        yield return new WaitForSeconds(lightsStopTime);
        light[1].SetActive(false);
        yield return new WaitForSeconds(lightsStopTime);
        light[0].SetActive(false);
        yield return new WaitForSeconds(lightsStopTime);
        light[3].SetActive(true);
        yield return new WaitForSeconds(lightsStopTime);
        endSequencePosition++;
    }

    IEnumerator PlayerLight()
    {
        yield return new WaitForSeconds(activateLight);
        light[2].SetActive(true);
        startSequencePosition++;

    }

    IEnumerator LightsON()
    {
        yield return new WaitForSeconds(closedTime);
        light[2].SetActive(false);
        light[0].SetActive(true);
        light[1].SetActive(true);
        startSequencePosition++;
    }

    void PlayerToCenter()
    {
        playerbehaviour.istanze.transform.position = centerGrid;
        playerbehaviour.istanze.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        startSequencePosition++;
    }

    void StartUP()
    {
        starting = false;

        if (soundNotLooping == false)
        {
            AudioManager.Instance.SetLoop("GongSound", false);
            AudioManager.Instance.PlaySound("GongSound");
            soundNotLooping = true;
        }

        GM.dragon1.SetActive(true);
        GM.dragon2.SetActive(false);
        GM.dragon3.SetActive(false);
        GM.dragonTimeline.Play();
        startSequencePosition++;

    }

    void CloseCurtains()
    {
        if (tenda.transform.localPosition.x > closecurtain.x)
        {
            tenda.transform.Translate(Vector3.left * curtainspeed * Time.deltaTime);
            tenda2.transform.Translate(Vector3.right * curtainspeed * Time.deltaTime);

        }
        else
        {
            tenda.transform.position = closecurtain;
            if(starting == true)
            {
                startSequencePosition++;
            }
            if(ending == true)
            {
                endSequencePosition++;
            }
        }
    }

    void OpenCurtains()
    {
        if (tenda.transform.localPosition.x < opencurtain1.x)
        {
            tenda.transform.Translate(Vector3.right * curtainspeed * Time.deltaTime);
            tenda2.transform.Translate(Vector3.left * curtainspeed * Time.deltaTime);
        }
        else
        {
            tenda.transform.position = opencurtain1;
            tenda2.transform.position = opencurtain2;
            startSequencePosition++;
        }
    }

    void SwitchUI()
    {
        switchui = !switchui;
        ink_text.gameObject.SetActive(switchui);
        score_text.gameObject.SetActive(switchui);
        counter_text.gameObject.SetActive(switchui);
        scoremultiplier_text.gameObject.SetActive(switchui);
        if (starting == true)
        {
            startSequencePosition++;
        }
        if (ending == true)
        {
            endSequencePosition++;
        }
    }


    //TODO Animazione kitsune 
    void Bowing()
    {
        startSequencePosition++;
    }

    //TODO Animazione seppuku
    void Seppuku()
    {
        endSequencePosition++;
    }
}
