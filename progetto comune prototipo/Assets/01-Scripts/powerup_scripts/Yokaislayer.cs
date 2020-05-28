using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Yokaislayer : MonoBehaviour
{
    #region VARIABLES
    int ink, maxInk;
    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;
    Inkstone inkStone_;
    public Vector3 closecurtain;
    bool active, switchui;
    public GameObject tenda, tenda2;
    private int yokaiSlayerSequenceNumber;
    public float curtainspeed;
    public Vector3 opencurtain1, opencurtain2;
    IEnumerator waiting;
    public float timestop;
    public GameObject signYS1, signYS2, signYS3;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        inkStone_ = FindObjectOfType<Inkstone>();

        active = false;

        switchui = true;

        yokaiSlayerSequenceNumber = 0;

        opencurtain1 = tenda.transform.position;
        opencurtain2 = tenda2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerbehaviour.yokaislayercount > 0 && active == false)
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

         switch(yokaiSlayerSequenceNumber)
         {
              case 0:
                SwitchUI();
                  break;
            case 1:
                SaveInk();
                break;
            case 2:
                CloseCurtains();
                break;
            case 3:
                TimeStop();
                  break;
            case 4:
                ActivateYokaiSlayer();
                SignYS();
                break;
            case 5:
                if(waiting == null)
                {
                    waiting = Waiting();
                    StartCoroutine(waiting);
                }
                break;
            case 6:
                if(waiting != null)
                {
                    StopCoroutine(waiting);
                    waiting = null;
                }
                ResumeTime();
                break;
            case 7:
                OpenCurtains();
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
    
    void CloseCurtains()
    {
        if(tenda.transform.localPosition.x > closecurtain.x)
        {
           tenda.transform.Translate(Vector3.left * curtainspeed * Time.deltaTime);
           tenda2.transform.Translate(Vector3.right * curtainspeed * Time.deltaTime);

        }
        else
        {
            tenda.transform.position = closecurtain; 
            yokaiSlayerSequenceNumber++;
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
            yokaiSlayerSequenceNumber++;
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

        for (int i=0; i < 7; i++)
        {
            foreach(GameObject nemici in enemyspawnmanager.poolenemy[i])
            {
                if(nemici.activeInHierarchy == true)
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
