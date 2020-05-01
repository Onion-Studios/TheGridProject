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
    #region Angoligriglia
    nodes Angolo1 = new nodes(0, 0);
    nodes Angolo2 = new nodes(4, 0);
    nodes Angolo3 = new nodes(0, 4);
    nodes Angolo4 = new nodes(4, 4);
    #endregion
    nodes estremità = new nodes(0,0);
    bool estremitàfound = false;
    int countercaselleattiveattorno = 0;
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

    public void Init()
    {
         //GameManager.Instantiate.GetPlayerBehaviur();
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
            SearchEstremità(grigliamanager.griglialogica);
            //ControlloSegnoCorretto();
        }
        else
        {
            Debug.Log("nessun estremita trovata");
        }

        if(estremitàfound == true)
        {
            ControlloSegnoCorretto();
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
                
                if (estremità.X + nodo.X >= 0 &&
                    estremità.X + nodo.X <= 4 &&
                    estremità.Z + nodo.Z >= 0 &&
                    estremità.Z + nodo.Z <= 4)
                {
                    if (grigliamanager.griglialogica[estremità.X + nodo.X, estremità.Z + nodo.Z] == true)
                    {
                        CounterCaselleGiuste++;

                        if (CounterCaselleGiuste == 4)
                        {
                            if (i == 0 || i == 1)
                            {
                                SearchAndDestroySign0Enemy();
                                estremitàfound = false;
                            }
                            else if (i == 2 || i == 3)
                            {
                                SearchAndDestroySign1Enemy();
                                estremitàfound = false;
                            }
                        }
                    }
                    else
                    {
                        estremitàfound = false;
                        CounterCaselleGiuste = 0;
                        break;
                    }
                }
            }

        }
    }

    void SearchEstremità(bool[,] griglia)
    {
        for(int x=0; x < griglia.GetLength(0); x++)
        {
            for (int z = 0; z < griglia.GetLength(1); z++)
            {
                if (griglia[x,z] == true)
                {
                    //casi angoli 
                    if (x == Angolo1.X && z == Angolo1.Z)
                    {
                        if (griglia[x + 1, z] == true || griglia[x, z + 1] == true)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                    }
                    else if (x == Angolo2.X && z == Angolo2.Z)
                    {
                        if (griglia[x, z + 1] == true || griglia[x - 1, z] == true)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                    }
                    else if (x == Angolo3.X && z == Angolo3.Z)
                    {
                        if (griglia[x + 1, z] == true || griglia[x, z - 1] == true)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                    }
                    else if (x == Angolo4.X && z == Angolo4.Z)
                    {
                        if (griglia[x, z - 1] == true || griglia[x - 1, z] == true)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                    }
                    //casi lati
                    //lato superiore
                    else if (z - 1 < 0)
                    {
                        if (griglia[x - 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x, z + 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x + 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (countercaselleattiveattorno == 1)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                        else
                        {
                            countercaselleattiveattorno = 0;
                        }
                    }
                    //lato inferiore
                    else if (z + 1 > 4)
                    {
                        if (griglia[x, z - 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x - 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x + 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (countercaselleattiveattorno == 1)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                        else
                        {
                            countercaselleattiveattorno = 0;
                        }
                    }
                    //lato destro
                    else if (x - 1 < 0)
                    {
                        if (griglia[x, z - 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x, z + 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x + 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (countercaselleattiveattorno == 1)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                        else
                        {
                            countercaselleattiveattorno++;
                        }
                    }
                    //lato sinistro
                    else if (x + 1 > 4)
                    {
                        if (griglia[x, z - 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x - 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x, z + 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (countercaselleattiveattorno == 1)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                        else
                        {
                            countercaselleattiveattorno = 0;
                        }
                    }
                    //lato interno
                    else
                    {
                        if (griglia[x, z - 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x - 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x, z + 1] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (griglia[x + 1, z] == true)
                        {
                            countercaselleattiveattorno++;
                        }
                        if (countercaselleattiveattorno == 1)
                        {
                            estremità.X = x;
                            estremità.Z = z;
                            estremitàfound = true;
                        }
                        else
                        {
                            countercaselleattiveattorno = 0;
                        }
                    }

                }
                
            }
        }
    }


    void SearchAndDestroySign0Enemy()
    {
        for (int i = 0; i < 7; i++)
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
                        case 3:
                            ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                            if (armoredenemy.segniarmoredenemy[0].activeInHierarchy == true)
                            {
                                armoredenemy.Deathforsign();
                            }
                            break;
                        case 4:
                            UndyingEnemy undyingenemy = nemicodadistruggere.GetComponent<UndyingEnemy>();
                            if (undyingenemy.segniundyingenemy[0].activeInHierarchy == true)
                            {
                                undyingenemy.Deathforsign();
                            }
                            break;
                        case 5:
                            MalevolentEnemy malevolentenemy = nemicodadistruggere.GetComponent<MalevolentEnemy>();
                            if (malevolentenemy.segnimalevolentenemy[0].activeInHierarchy == true)
                            {
                                malevolentenemy.Deathforsign();
                            }
                            break;
                        case 6:
                            FrighteningEnemy frighteningenemy = nemicodadistruggere.GetComponent<FrighteningEnemy>();
                            if (frighteningenemy.segnifrighteningenemy[0].activeInHierarchy == true)
                            {
                                frighteningenemy.Deathforsign();
                            }
                            break;
                    }

                }
            }
        }
    }

    void SearchAndDestroySign1Enemy()
    {
        for (int i = 0; i < 7; i++)
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
                        case 3:
                            ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                            if (armoredenemy.segniarmoredenemy[1].activeInHierarchy == true)
                            {
                                armoredenemy.Deathforsign();
                            }
                            break;
                        case 4:
                            UndyingEnemy undyingenemy = nemicodadistruggere.GetComponent<UndyingEnemy>();
                            if (undyingenemy.segniundyingenemy[1].activeInHierarchy == true)
                            {
                                undyingenemy.Deathforsign();
                            }
                            break;
                        case 6:
                            FrighteningEnemy frighteningenemy = nemicodadistruggere.GetComponent<FrighteningEnemy>();
                            if (frighteningenemy.segnifrighteningenemy[1].activeInHierarchy == true)
                            {
                                frighteningenemy.Deathforsign();
                            }
                            break;
                    }
                }
            }

        }
    }
}
