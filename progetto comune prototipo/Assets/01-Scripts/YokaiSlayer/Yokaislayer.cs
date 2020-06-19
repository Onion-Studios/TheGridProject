using System.Collections;
using UnityEngine;
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
    bool active, switchui;
    [HideInInspector]
    public GameObject tenda, tenda2;
    private int yokaiSlayerSequenceNumber;
    public float curtainspeed;
    IEnumerator waiting;
    public float timestop;
    public GameObject signYS1, signYS2, signYS3;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL!");
        }

        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("Playerbehaviour is NULL!");
        }

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
        active = false;

        switchui = true;

        yokaiSlayerSequenceNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.C)) && playerbehaviour.yokaislayercount > 0 && active == false && startendsequence.starting == false && startendsequence.ending == false)
        {
            active = true;
        }

        if (active == true)
        {
            YokaiSlayerSequence();
        }
    }


    void YokaiSlayerSequence()
    {

        switch (yokaiSlayerSequenceNumber)
        {
            case 0:
                SwitchUI();
                break;
            case 1:
                SaveInk();
                break;
            case 2:
                yokaiSlayerSequenceNumber = curtains.CloseCurtains(yokaiSlayerSequenceNumber, curtainspeed);
                break;
            case 3:
                TimeStop();
                AudioManager.Instance.PlaySound("YokaiSlayerBrawl");
                break;
            case 4:
                ActivateYokaiSlayer();
                SignYS();
                break;
            case 5:
                if (waiting == null)
                {
                    waiting = Waiting();
                    StartCoroutine(waiting);
                }
                break;
            case 6:
                if (waiting != null)
                {
                    StopCoroutine(waiting);
                    waiting = null;
                }
                ResumeTime();
                break;
            case 7:
                yokaiSlayerSequenceNumber = curtains.OpenCurtains(yokaiSlayerSequenceNumber, curtainspeed);
                break;
            case 8:
                ReloadInk();
                break;
            case 9:
                SwitchUI();
                break;
            case 10:
                FinalizeSequence();
                break;
        }

    }

    void TimeStop()
    {
        Time.timeScale = 0f;
        yokaiSlayerSequenceNumber++;
    }

    void ResumeTime()
    {
        Time.timeScale = 1f;
        yokaiSlayerSequenceNumber++;
    }

    void SaveInk()
    {
        ink = inkStone_.Ink;
        maxInk = inkStone_.maxInk;
        yokaiSlayerSequenceNumber++;
        inkStone_.maxInk = 1000;
        inkStone_.Ink = 1000;
    }

    void ReloadInk()
    {
        inkStone_.Ink = ink;
        inkStone_.maxInk = maxInk;
        yokaiSlayerSequenceNumber++;
    }

    void SwitchUI()
    {
        switchui = !switchui;
        ink_text.gameObject.SetActive(switchui);
        score_text.gameObject.SetActive(switchui);
        counter_text.gameObject.SetActive(switchui);
        scoremultiplier_text.gameObject.SetActive(switchui);
        yokaiSlayerSequenceNumber++;
    }

    void SignYS()
    {
        if (playerbehaviour.yokaislayercount == 2)
        {
            signYS3.SetActive(false);
        }
        if (playerbehaviour.yokaislayercount == 1)
        {
            signYS2.SetActive(false);
        }
        if (playerbehaviour.yokaislayercount == 0)
        {
            signYS1.SetActive(false);
        }
    }


    IEnumerator Waiting()
    {
        yield return new WaitForSecondsRealtime(timestop);
        yokaiSlayerSequenceNumber++;

    }

    void ActivateYokaiSlayer()
    {
        playerbehaviour.yokaislayercount--;

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
        yokaiSlayerSequenceNumber++;
    }

    void FinalizeSequence()
    {

        yokaiSlayerSequenceNumber = 0;

        active = false;
    }
}
