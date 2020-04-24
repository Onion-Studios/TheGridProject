using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yokaislayer : MonoBehaviour
{
    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerbehaviour.yokaislayercount > 0)
        {
            ActivateYokaiSlayer();
            ReduceYokaiSlayerCount();
        }
    }

    void ActivateYokaiSlayer()
    {
        for(int i=0; i<4; i++)
        {
            foreach(GameObject nemici in enemyspawnmanager.poolnemici[i])
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

    void ReduceYokaiSlayerCount()
    {
        playerbehaviour.yokaislayercount -= 1;
    }
}
