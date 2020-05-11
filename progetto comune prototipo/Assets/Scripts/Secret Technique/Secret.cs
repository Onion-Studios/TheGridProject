using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{

    [Range(0,100)] public int bar = 0;
    public int charge;

    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;

    public float currentTime = 0f;
    public float timeMax = 5f;
    bool active = false;
    Color changeColor = Color.white;
   

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Colore();
        Timer();
    }

    
    void Timer()
    {
        
        if (bar > 100)
        {
            bar = 100;
        }

        //se la barra arriva a 100 
        if (bar == 100)
        {
            //attiva il timer la prima volta che arriva a 100
            if (active == false)
            {
                active = true;

                currentTime = timeMax;
            }
            else
            {
                //se premo E i enemy sulle lane muoiono 
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Death();
                }

                //Timer
                currentTime -= 1 * Time.deltaTime;

                if (currentTime < 0)
                {
                    currentTime = 0;
                }

                //reset barra e si disattiva la secret 
                if (currentTime == 0)
                {
                    bar = 0;

                    active = false;
                }

            }
        }
    }

    //Muoiono tutti i enemy presenti sulla lane 
    void Death()
    {
        for (int i = 0; i < 8; i++)
        {
            foreach (GameObject enemy in enemyspawnmanager.poolenemy[i])
            {
                if (enemy.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = enemy.GetComponent<NormalEnemy>();
                            normalenemy.Deathforsign();
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = enemy.GetComponent<KamikazeEnemy>();
                            kamikazenemy.Deathforsign();
                            break;
                        case 2:
                            ArmoredEnemy armoredenemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredenemy.armoredLife = 1;
                            armoredenemy.Deathforsign();
                            break;
                        case 3:
                            UndyingEnemy undyingenemy = enemy.GetComponent<UndyingEnemy>();
                            undyingenemy.Deathforsign();
                            break;
                        case 4:
                            MalevolentEnemy malevolentenemy = enemy.GetComponent<MalevolentEnemy>();
                            malevolentenemy.Deathforsign();
                            break;
                        case 5:
                            FrighteningEnemy frighteningenemy = enemy.GetComponent<FrighteningEnemy>();
                            frighteningenemy.Deathforsign();
                            break;
                        case 6:
                            BufferEnemy bufferenemy = enemy.GetComponent<BufferEnemy>();
                            bufferenemy.Deathforsign();
                            break;
                    }
                }

            }
        }

    }


    void Colore()
    {
        var cubeRenderer = this.GetComponent<Renderer>();
        changeColor = Color.Lerp(Color.white, Color.red, 0f + bar / 100f);
        cubeRenderer.material.SetColor("_Color", changeColor);

    }
}
