using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Yokaislayer : MonoBehaviour
{
    #region VARIABLES
    int ink, maxInk;
    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;
    StartEndSequence startendsequence;
    Inkstone inkStone_;
    Curtains curtains;
    IntensityReset intensityReset;
    public WaveManager WM;
    public Vector3 closecurtain;
    public bool active;
    public bool playerMovement;
    [HideInInspector]
    public GameObject tenda, tenda2;
    private int yokaiSlayerSequenceNumber;
    public float curtainspeed;
    public float timeStop;
    float timer;
    public PlayableDirector[] YokaiSlayerTimelines;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    bool signMovement;
    [SerializeField]
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL!");
        }

        playerbehaviour = this.gameObject.GetComponent<Playerbehaviour>();

        startendsequence = FindObjectOfType<StartEndSequence>();
        if (startendsequence == null)
        {
            Debug.LogError("StartEndSequence is NULL!");
        }

        inkStone_ = FindObjectOfType<Inkstone>();
        if (inkStone_ == null)
        {
            Debug.LogError("Inkstone is NULL!");
        }

        curtains = FindObjectOfType<Curtains>();
        if (curtains == null)
        {
            Debug.LogError("Curtains is NULL!");
        }

        intensityReset = FindObjectOfType<IntensityReset>();
        if (intensityReset == null)
        {
            Debug.LogError("IntensityReset is NULL");
        }
        active = false;

        yokaiSlayerSequenceNumber = 0;
        signMovement = false;
        timer = timeStop;
        playerMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.C) || Input.GetAxis("RT") == 1 || Input.GetButtonDown("Square")) && playerbehaviour.yokaislayercount > 0 && active == false && startendsequence.starting == false && startendsequence.ending == false && intensityReset.intensityReset == false)
        {
            active = true;
        }

        if (active == true)
        {
            YokaiSlayerSequence();
        }
        if (active == false && yokaiSlayerSequenceNumber == 8)
        {
            PostYokaiSlayer();
        }
    }


    void YokaiSlayerSequence()
    {

        switch (yokaiSlayerSequenceNumber)
        {
            case 0:
                SaveInk();
                break;
            case 1:
                yokaiSlayerSequenceNumber = curtains.CloseCurtains(yokaiSlayerSequenceNumber, curtainspeed);
                break;
            case 2:
                ActivateYokaiSlayer(true);
                AudioManager.Instance.PlaySound("YokaiSlayerBrawl");
                break;
            case 3:
                Waiting();
                break;
            case 4:
                ReloadInk();
                ActivateYokaiSlayer(false);
                break;
            case 5:
                yokaiSlayerSequenceNumber = curtains.OpenCurtains(yokaiSlayerSequenceNumber, curtainspeed);
                break;
            case 6:
                SignMovement();
                Waiting();
                break;
            case 7:
                FinalizeSequence();
                break;
        }

    }

    void SaveInk()
    {
        playerMovement = false;
        ink = inkStone_.Ink;
        maxInk = inkStone_.maxInk;
        yokaiSlayerSequenceNumber++;
        inkStone_.maxInk = 10000;
        inkStone_.Ink = 10000;
    }

    void ReloadInk()
    {
        inkStone_.Ink = ink;
        inkStone_.maxInk = maxInk;
        yokaiSlayerSequenceNumber++;
    }

    void SignMovement()
    {
        if (signMovement == false)
        {
            YokaiSlayerTimelines[playerbehaviour.yokaislayercount - 1].Play();
            signMovement = true;
            playerMovement = true;
        }
    }


    void Waiting()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeStop;
            yokaiSlayerSequenceNumber++;
        }
    }

    void ActivateYokaiSlayer(bool advanceSequence)
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (GameObject nemici in enemyspawnmanager.poolenemy[i])
            {
                if (nemici.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = nemici.GetComponent<NormalEnemy>();
                            normalenemy.Deathforsign();
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = nemici.GetComponent<KamikazeEnemy>();
                            kamikazenemy.Deathforsign();
                            break;
                        case 2:
                            ArmoredEnemy armoredenemy = nemici.GetComponent<ArmoredEnemy>();
                            armoredenemy.armoredLife = 1;
                            armoredenemy.Deathforsign();
                            break;
                        case 3:
                            UndyingEnemy undyingenemy = nemici.GetComponent<UndyingEnemy>();
                            undyingenemy.Deathforsign();
                            break;
                        case 4:
                            MalevolentEnemy malevolentenemy = nemici.GetComponent<MalevolentEnemy>();
                            malevolentenemy.Deathforsign();
                            break;
                        case 5:
                            FrighteningEnemy frighteningenemy = nemici.GetComponent<FrighteningEnemy>();
                            frighteningenemy.Deathforsign();
                            break;
                        case 6:
                            BufferEnemy bufferenemy = nemici.GetComponent<BufferEnemy>();
                            bufferenemy.Deathforsign();
                            break;
                    }
                }
            }
        }
        playerbehaviour.yokairesettocenter();
        if (advanceSequence == true) { yokaiSlayerSequenceNumber++; }
    }

    void FinalizeSequence()
    {
        playerbehaviour.yokaislayercount--;
        yokaiSlayerSequenceNumber++;
        active = false;
        signMovement = false;
    }

    void PostYokaiSlayer()
    {
        WM.RestartWaves();
        yokaiSlayerSequenceNumber = 0;
    }
}
