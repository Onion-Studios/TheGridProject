using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{

    [Range(0,100)] public int barra = 0;
    public int carica;

    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;

    public float currentTime = 0f;
    public float timeMax = 5f;
    bool active = false;
    Color cambiaColore = Color.white;
   

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
        
        if (barra > 100)
        {
            barra = 100;
        }

        //se la barra arriva a 100 
        if (barra == 100)
        {
            //attiva il timer la prima volta che arriva a 100
            if (active == false)
            {
                active = true;

                currentTime = timeMax;
            }
            else
            {
                //se premo E i nemici sulle lane muoiono 
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
                    barra = 0;

                    active = false;
                }

            }
        }
    }

    //Muoiono tutti i nemici presenti sulla lane 
    void Death()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (GameObject nemici in enemyspawnmanager.poolnemici[i])
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
                            GoldenEnemy goldenenemy = nemici.GetComponent<GoldenEnemy>();
                            goldenenemy.Deathforsign();
                            break;
                        case 3:
                            ArmoredEnemy armoredenemy = nemici.GetComponent<ArmoredEnemy>();
                            armoredenemy.Deathforsign();
                            break;
                    }
                }

            }
        }

    }



    void Colore()
    {
        var cubeRenderer = this.GetComponent<Renderer>();
        cambiaColore = Color.Lerp(Color.white, Color.red, 0f + barra / 100f);
        cubeRenderer.material.SetColor("_Color", cambiaColore);

    }
}
