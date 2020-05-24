using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Yokaislayer : MonoBehaviour
{
    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;
    bool opencurtain;
    public Vector3 closecurtain;
    public Vector3 actualPosition;
    public float currentTime = 0f;
    public float timeMax = 2f;
    bool active;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();

        
        opencurtain = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerbehaviour.yokaislayercount > 0)
        {
            ActivateYokaiSlayer();
            ReduceYokaiSlayerCount();

            TimerCurtain();

            closecurtain = transform.position;

        }
    }

    void TimerCurtain()
    {
        if (opencurtain == true)
        {
            
            if (active == false)
            {
                active = true;

                currentTime = timeMax;
                
               // transform.position = transform.position + transform.right * speed * Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.X) && playerbehaviour.yokaislayercount > 0)
                {
                
                    actualPosition = transform.position;

                    //Timer
                    currentTime -= 1 * Time.deltaTime;

                    if (currentTime < 0)
                    {
                        currentTime = 0;

                        closecurtain = transform.position;

                    }

                    //reset barra e si disattiva la secret 
                    if (currentTime == 0)
                    {
                        opencurtain = true;

                        active = false;

                        closecurtain = transform.position;
                    }
                }

            }
        }
    }

    void ActivateYokaiSlayer()
    {
        for(int i=0; i < 7; i++)
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
    }

    void ReduceYokaiSlayerCount()
    {
        playerbehaviour.yokaislayercount -= 1;
    }
}
