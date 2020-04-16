using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public struct nodes
{
    public int X;
    public int Z;
    public nodes(int x, int z)
    {
        X = x;
        Z = z;
    }

}
public class Managercombo : MonoBehaviour
{
    #region Variabili
    public nodes[][] MatriceIrerregolareSegni = new nodes[12][];   
    Enemyspawnmanager enemyspawnmanager;
    UIManager UIManager;
    Playerbehaviour playerbehaviour;
    grigliamanager grigliamanager;
    public int CountCaselleAttivate = 0;
    #endregion

    private void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("enemyspawnmanager è null");
        }

        UIManager = FindObjectOfType<UIManager>();
        if (UIManager == null)
        {
            Debug.LogError("UImanager è null");
        }

        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("playerbehaviour è null");
        }

        grigliamanager = FindObjectOfType<grigliamanager>();
        if (grigliamanager == null)
        {
            Debug.LogError("grigliamanager è null");
        }

        setupsegnimatrice();
    }

    private void Update()
    {
        
    }

    public void setupsegnimatrice()
    {
        //2 casi del segno L
        MatriceIrerregolareSegni[0] = new nodes[4] { new nodes(1, 0), new nodes(2, 0), new nodes(2, -1), new nodes(2, -2) };
        MatriceIrerregolareSegni[1] = new nodes[4] { new nodes(0, 1), new nodes(0, 2), new nodes(-1, 2), new nodes(-2, 2) };
        //2 casi del segno C
        MatriceIrerregolareSegni[2] = new nodes[4] { new nodes(1, 0), new nodes(1, 1), new nodes(1, 2), new nodes(0, 2) };
        MatriceIrerregolareSegni[3] = new nodes[4] { new nodes(1, 0), new nodes(1, -1), new nodes(1, -2), new nodes(0, -2) };

    }

    public void Checksign()
    {
        CheckCountCaselleGrigliaLogica();

        if(CountCaselleAttivate == 5)
        {
            ControlloSegnoCorretto();
        }
        else
        {
            Debug.Log("nessun segno valido tracciato");
        }
    }

    void CheckCountCaselleGrigliaLogica()
    {
        foreach(bool casella in grigliamanager.griglialogica)
        {
            if(casella == true)
            {
                CountCaselleAttivate++;
            }
        }
    }

    void ControlloSegnoCorretto()
    {
        int CounterCaselleGiuste = 0;

        for (int i = 0; i < 4; i++) 
        {
            if (CounterCaselleGiuste == 4)
            {
                CounterCaselleGiuste = 0;
                break;
            }

            foreach (var nodo in MatriceIrerregolareSegni[i])
            {
                
                if ((int)playerbehaviour.LastCubeChecked.x + nodo.X >= 0 &&
                    (int)playerbehaviour.LastCubeChecked.x + nodo.X <= 4 &&
                    (int)playerbehaviour.LastCubeChecked.z + nodo.Z >= 0 &&
                    (int)playerbehaviour.LastCubeChecked.z + nodo.Z <= 4)
                {
                    if (grigliamanager.griglialogica[(int)playerbehaviour.LastCubeChecked.x + nodo.X, (int)playerbehaviour.LastCubeChecked.z + nodo.Z] == true)
                    {
                        CounterCaselleGiuste++;

                        if (CounterCaselleGiuste == 4)
                        {
                            if (i == 0 || i == 1)
                            {
                                SearchAndDestroySign0Enemy();
                            }
                            else if (i == 2 || i == 3)
                            {
                                SearchAndDestroySign1Enemy();
                            }
                        }
                    }
                    else
                    {
                        CounterCaselleGiuste = 0;
                        break;
                    }
                }
            }

        }
    }

    void SearchAndDestroySign0Enemy()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
            {
                if (nemicodadistruggere.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                            if (normalenemy.segninormalenemy[0].activeInHierarchy == true)
                            {
                                normalenemy.Deathforsign();
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                            if (kamikazenemy.segnikamikazenemy[0].activeInHierarchy == true)
                            {
                                kamikazenemy.Deathforsign();
                            }
                            break;
                        case 2:
                            GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                            if (goldenenemy.segnigoldenenemy[0].activeInHierarchy == true)
                            {
                                goldenenemy.Deathforsign();
                            }
                            break;
                    }

                }
            }
        }
    }

    void SearchAndDestroySign1Enemy()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
            {
                if (nemicodadistruggere.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                            if (normalenemy.segninormalenemy[1].activeInHierarchy == true)
                            {
                                normalenemy.Deathforsign();
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                            if (kamikazenemy.segnikamikazenemy[1].activeInHierarchy == true)
                            {
                                kamikazenemy.Deathforsign();
                            }
                            break;
                        case 2:
                            GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                            if (goldenenemy.segnigoldenenemy[1].activeInHierarchy == true)
                            {
                                goldenenemy.Deathforsign();
                            }
                            break;
                    }
                }
            }

        }
    }
}
