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
    #region VARIABLES
    public nodes[][] IrregularSignMatrix = new nodes[12][];
    #region GRID CORNERS
    nodes Angle1 = new nodes(0, 0);
    nodes Angle2 = new nodes(4, 0);
    nodes Angle3 = new nodes(0, 4);
    nodes Angle4 = new nodes(4, 4);
    #endregion
    nodes extremity = new nodes(0,0);
    bool foundextremity = false;
    int countactiveboxesaround = 0;
    Enemyspawnmanager enemyspawnmanager;
    UIManager UIManager;
    Playerbehaviour playerbehaviour;
    grigliamanager grigliamanager;
    public int CountBoxesActive = 0;
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

        SetupMatrixSign();
    }

    public void Init()
    {
         //GameManager.Instantiate.GetPlayerBehaviur();
    }

    private void Update()
    {

    }

    

    public void SetupMatrixSign()
    {
        //2 casi del segno L
        IrregularSignMatrix[0] = new nodes[4] { new nodes(1, 0), new nodes(2, 0), new nodes(2, -1), new nodes(2, -2) };
        IrregularSignMatrix[1] = new nodes[4] { new nodes(0, 1), new nodes(0, 2), new nodes(-1, 2), new nodes(-2, 2) };
        //2 casi del segno C
        IrregularSignMatrix[2] = new nodes[4] { new nodes(1, 0), new nodes(1, 1), new nodes(1, 2), new nodes(0, 2) };
        IrregularSignMatrix[3] = new nodes[4] { new nodes(1, 0), new nodes(1, -1), new nodes(1, -2), new nodes(0, -2) };
        //2 casi del segno S rovesciata
        IrregularSignMatrix[4] = new nodes[4] { new nodes(0, -1), new nodes(-1, -1), new nodes(-2, -1), new nodes(-2, -2) };
        IrregularSignMatrix[5] = new nodes[4] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, 2) };
        //2 casi del segno L capovolto
        IrregularSignMatrix[6] = new nodes[4] { new nodes(0, -1), new nodes(0, -2), new nodes(-1, -2), new nodes(-2, -2) };
        IrregularSignMatrix[7] = new nodes[4] { new nodes(1, 0), new nodes(2, 0), new nodes(2, 1), new nodes(2, 2) };
        //2 casi del segno vasino (senza la cacca)
        IrregularSignMatrix[8] = new nodes[4] { new nodes(0, 1), new nodes(-1, 1), new nodes(-2, 1), new nodes(-2, 0) };
        IrregularSignMatrix[9] = new nodes[4] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, -1) };
        //2 casi del segno S
        IrregularSignMatrix[10] = new nodes[4] { new nodes(-1, 0), new nodes(-1, 1), new nodes(-1, 2), new nodes(-2, 2) };
        IrregularSignMatrix[11] = new nodes[4] { new nodes(1, 0), new nodes(1, -1), new nodes(1, -2), new nodes(2, -2) };
        //1 caso segno Malevolent
       // IrregularSignMatrix[12] = new nodes[6] { new nodes(1, 0), new nodes(1, 1), new nodes(1, 2), new nodes(0, 2), new nodes(-1, 2), new nodes(-1,1)};

    }

    public void CheckSign()
    {
        CheckCountBoxesLogicGrid();

        if(CountBoxesActive == 5)
        {
            Searchextremity(grigliamanager.logicgrid);
        }
        else
        {
            Debug.Log("nessun estremita trovata");
        }

        if(foundextremity == true)
        {
            CheckCorrectSign();
        }

    }

    void CheckCountBoxesLogicGrid()
    {
        foreach(bool box in grigliamanager.logicgrid)
        {
            if(box == true)
            {
                CountBoxesActive++;
            }
        }
    }

    void CheckCorrectSign()
    {
        int countercorrectbox = 0;

        for (int i = 0; i < 13; i++) 
        {
            if (countercorrectbox == 4)
            {
                countercorrectbox = 0;
                break;
            }

            foreach (var nodo in IrregularSignMatrix[i])
            {
                
                if (extremity.X + nodo.X >= 0 &&
                    extremity.X + nodo.X <= 4 &&
                    extremity.Z + nodo.Z >= 0 &&
                    extremity.Z + nodo.Z <= 4)
                {
                    if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                    {
                        countercorrectbox++;

                        if (countercorrectbox == 4)
                        {
                            if (i == 0 || i == 1)
                            {
                                SearchAndDestroy(0);
                                foundextremity = false;
                            }
                            else if (i == 2 || i == 3)
                            {
                                SearchAndDestroy(1);
                                foundextremity = false;
                            }
                            else if (i == 4 || i == 5)
                            {
                                SearchAndDestroy(2);
                                foundextremity = false;
                            }
                            else if (i == 6 || i == 7)
                            {
                                SearchAndDestroy(3);
                                foundextremity = false;
                            }
                            else if (i == 8 || i == 9)
                            {
                                SearchAndDestroy(4);
                                foundextremity = false;
                            }
                            else if (i == 10 || i == 11)
                            {
                                SearchAndDestroy(5);
                                foundextremity = false;
                            }
                            /*else if(i == 12)
                            {
                                SearchAndDestroy(6);
                                foundextremity = false;
                            }*/
                        }
                    }
                    else
                    {
                        foundextremity = false;
                        countercorrectbox = 0;
                        break;
                    }
                }
            }

        }
    }

    void Searchextremity(bool[,] grid)
    {
        for(int x=0; x < grid.GetLength(0); x++)
        {
            for (int z = 0; z < grid.GetLength(1); z++)
            {
                if (grid[x,z] == true)
                {
                    //casi angoli 
                    if (x == Angle1.X && z == Angle1.Z)
                    {
                        if (grid[x + 1, z] == true || grid[x, z + 1] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle2.X && z == Angle2.Z)
                    {
                        if (grid[x, z + 1] == true || grid[x - 1, z] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle3.X && z == Angle3.Z)
                    {
                        if (grid[x + 1, z] == true || grid[x, z - 1] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle4.X && z == Angle4.Z)
                    {
                        if (grid[x, z - 1] == true || grid[x - 1, z] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    //casi lati
                    //lato superiore
                    else if (z - 1 < 0)
                    {
                        if (grid[x - 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x, z + 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x + 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (countactiveboxesaround == 1)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                        else
                        {
                            countactiveboxesaround = 0;
                        }
                    }
                    //lato inferiore
                    else if (z + 1 > 4)
                    {
                        if (grid[x, z - 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x - 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x + 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (countactiveboxesaround == 1)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                        else
                        {
                            countactiveboxesaround = 0;
                        }
                    }
                    //lato destro
                    else if (x - 1 < 0)
                    {
                        if (grid[x, z - 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x, z + 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x + 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (countactiveboxesaround == 1)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                        else
                        {
                            countactiveboxesaround++;
                        }
                    }
                    //lato sinistro
                    else if (x + 1 > 4)
                    {
                        if (grid[x, z - 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x - 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x, z + 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (countactiveboxesaround == 1)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                        else
                        {
                            countactiveboxesaround = 0;
                        }
                    }
                    //lato interno
                    else
                    {
                        if (grid[x, z - 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x - 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x, z + 1] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (grid[x + 1, z] == true)
                        {
                            countactiveboxesaround++;
                        }
                        if (countactiveboxesaround == 1)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                        else
                        {
                            countactiveboxesaround = 0;
                        }
                    }

                }
                
            }
        }
    }


    void SearchAndDestroy(int valoreIndice)
    {
        for (int i = 0; i < 8; i++)
        {
            foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[i])
            {
                if (enemytodestroy.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = enemytodestroy.GetComponent<NormalEnemy>();
                            if (normalenemy.signnormalenemy[valoreIndice].activeInHierarchy == true)
                            {
                                normalenemy.Deathforsign();
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = enemytodestroy.GetComponent<KamikazeEnemy>();
                            if (kamikazenemy.signkamikazenemy[valoreIndice].activeInHierarchy == true)
                            {
                                kamikazenemy.Deathforsign();
                            }
                            break;
                        case 2:
                            ArmoredEnemy armoredenemy = enemytodestroy.GetComponent<ArmoredEnemy>();
                            if (armoredenemy.signarmoredenemy[valoreIndice].activeInHierarchy == true)
                            {
                                armoredenemy.Deathforsign();
                            }
                            break;
                        case 3:
                            UndyingEnemy undyingenemy = enemytodestroy.GetComponent<UndyingEnemy>();
                            if (undyingenemy.signundyingenemy[valoreIndice].activeInHierarchy == true)
                            {
                                undyingenemy.Deathforsign();
                            }
                            break;
                        case 4:
                            MalevolentEnemy malevolentenemy = enemytodestroy.GetComponent<MalevolentEnemy>();
                            if (malevolentenemy.signmalevolentenemy[valoreIndice].activeInHierarchy == true)
                            {
                                malevolentenemy.Deathforsign();
                            }
                            break;
                        case 5:
                            FrighteningEnemy frighteningenemy = enemytodestroy.GetComponent<FrighteningEnemy>();
                            if (frighteningenemy.signfrighteningenemy[valoreIndice].activeInHierarchy == true)
                            {
                                frighteningenemy.Deathforsign();
                            }
                            break;
                        case 6:
                            BufferEnemy bufferenemy = enemytodestroy.GetComponent<BufferEnemy>();
                            if (bufferenemy.signbufferenemy[valoreIndice].activeInHierarchy == true)
                            {
                                bufferenemy.Deathforsign();
                            }
                            break;
                    }

                }
            }
        }
    }
}