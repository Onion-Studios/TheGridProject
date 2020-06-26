using UnityEngine;

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
    #region MATRIXSIGNS
    public nodes[][] NormalYokaiMatrix = new nodes[12][];
    public nodes[][] Intensity1Matrix = new nodes[12][];
    public nodes[][] Intensity1PlusMatrix = new nodes[12][];
    public nodes[][] Intensity2Matrix = new nodes[8][];
    public nodes[][] Intensity2PlusMatrix = new nodes[7][];
    public nodes[][] Intensity3Matrix = new nodes[5][];
    public nodes[][] SpecialSignMatrix = new nodes[4][];
    #endregion
    #region GRID CORNERS
    nodes Angle1 = new nodes(0, 0);
    nodes Angle2 = new nodes(4, 0);
    nodes Angle3 = new nodes(0, 4);
    nodes Angle4 = new nodes(4, 4);
    #endregion
    nodes extremity = new nodes(0, 0);
    bool foundextremity = false;
    int countactiveboxesaround = 0;
    Enemyspawnmanager enemyspawnmanager;
    UIManager UIManager;
    Playerbehaviour playerbehaviour;
    grigliamanager grigliamanager;
    GameManager Gamemanager;
    Secret secretscript;
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

        secretscript = FindObjectOfType<Secret>();
        if (secretscript == null)
        {
            Debug.LogError("secretscript è null");
        }

        Gamemanager = FindObjectOfType<GameManager>();
        if (Gamemanager == null)
        {
            Debug.LogError("gamemanager è null");
        }

        SetupMatrixSign();
    }

    public void Init()
    {
        //GameManager.Instantiate.GetPlayerBehaviur();
    }

    public void SetupMatrixSign()
    {
        #region NormalYokai
        //documento segni n1
        NormalYokaiMatrix[0] = new nodes[3] { new nodes(0, -1), new nodes(0, -2), new nodes(1, -2) };
        NormalYokaiMatrix[1] = new nodes[3] { new nodes(-1, 0), new nodes(-1, 1), new nodes(-1, 2) };
        //documento segni n2
        NormalYokaiMatrix[2] = new nodes[3] { new nodes(0, -1), new nodes(1, -1), new nodes(2, -1) };
        NormalYokaiMatrix[3] = new nodes[3] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, 1) };
        //documento segni n3
        NormalYokaiMatrix[4] = new nodes[3] { new nodes(0, 1), new nodes(-1, 1), new nodes(-2, 1) };
        NormalYokaiMatrix[5] = new nodes[3] { new nodes(1, 0), new nodes(2, 0), new nodes(2, -1) };
        //documento segni n4
        NormalYokaiMatrix[6] = new nodes[3] { new nodes(0, 1), new nodes(1, 1), new nodes(1, 2) };
        NormalYokaiMatrix[7] = new nodes[3] { new nodes(0, -1), new nodes(-1, -1), new nodes(-1, -2) };
        //documento segni n5
        NormalYokaiMatrix[8] = new nodes[3] { new nodes(-1, 0), new nodes(-1, -1), new nodes(-2, -1) };
        NormalYokaiMatrix[9] = new nodes[3] { new nodes(1, 0), new nodes(1, 1), new nodes(2, 1) };
        //documento segni n6
        NormalYokaiMatrix[10] = new nodes[3] { new nodes(0, -1), new nodes(1, -1), new nodes(1, -2) };
        NormalYokaiMatrix[11] = new nodes[3] { new nodes(1, 0), new nodes(-1, 1), new nodes(-1, -2) };
        #endregion
        #region Intensity1
        //2 casi del segno L
        Intensity1Matrix[0] = new nodes[4] { new nodes(1, 0), new nodes(2, 0), new nodes(2, -1), new nodes(2, -2) };
        Intensity1Matrix[1] = new nodes[4] { new nodes(0, 1), new nodes(0, 2), new nodes(-1, 2), new nodes(-2, 2) };
        //2 casi del segno C
        Intensity1Matrix[2] = new nodes[4] { new nodes(1, 0), new nodes(1, 1), new nodes(1, 2), new nodes(0, 2) };
        Intensity1Matrix[3] = new nodes[4] { new nodes(1, 0), new nodes(1, -1), new nodes(1, -2), new nodes(0, -2) };
        //2 casi del segno S rovesciata
        Intensity1Matrix[4] = new nodes[4] { new nodes(0, -1), new nodes(-1, -1), new nodes(-2, -1), new nodes(-2, -2) };
        Intensity1Matrix[5] = new nodes[4] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, 2) };
        //2 casi del segno L capovolto
        Intensity1Matrix[6] = new nodes[4] { new nodes(0, -1), new nodes(0, -2), new nodes(-1, -2), new nodes(-2, -2) };
        Intensity1Matrix[7] = new nodes[4] { new nodes(1, 0), new nodes(2, 0), new nodes(2, 1), new nodes(2, 2) };
        //2 casi del segno vasino (senza la cacca)
        Intensity1Matrix[8] = new nodes[4] { new nodes(0, 1), new nodes(-1, 1), new nodes(-2, 1), new nodes(-2, 0) };
        Intensity1Matrix[9] = new nodes[4] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, -1) };
        //2 casi del segno S
        Intensity1Matrix[10] = new nodes[4] { new nodes(-1, 0), new nodes(-1, 1), new nodes(-1, 2), new nodes(-2, 2) };
        Intensity1Matrix[11] = new nodes[4] { new nodes(1, 0), new nodes(1, -1), new nodes(1, -2), new nodes(2, -2) };
        #endregion
        #region Intensity1Plus
        //documento segni n1
        Intensity1PlusMatrix[0] = new nodes[5] { new nodes(0, 1), new nodes(0, 2), new nodes(-1, 2), new nodes(-2, 2), new nodes(-2, 1) };
        Intensity1PlusMatrix[1] = new nodes[5] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, 0), new nodes(2, -1) };
        //documento segni n2
        Intensity1PlusMatrix[2] = new nodes[5] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, -1), new nodes(-2, -2), new nodes(-1, -2) };
        Intensity1PlusMatrix[3] = new nodes[5] { new nodes(-1, 0), new nodes(-1, 1), new nodes(-1, 2), new nodes(0, 2), new nodes(1, 2) };
        //documento segni n3
        Intensity1PlusMatrix[4] = new nodes[5] { new nodes(1, 0), new nodes(1, 1), new nodes(2, 1), new nodes(2, 0), new nodes(2, -1) };
        Intensity1PlusMatrix[5] = new nodes[5] { new nodes(0, 1), new nodes(0, 2), new nodes(-1, 2), new nodes(-1, 1), new nodes(-2, 1) };
        //documento segni n4
        Intensity1PlusMatrix[6] = new nodes[5] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, 2), new nodes(1, 2) };
        Intensity1PlusMatrix[7] = new nodes[5] { new nodes(0, 1), new nodes(-1, 1), new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, -1) };
        //documento segni n5
        Intensity1PlusMatrix[8] = new nodes[5] { new nodes(0, -1), new nodes(-1, -1), new nodes(-1, -2), new nodes(0, -2), new nodes(1, -2) };
        Intensity1PlusMatrix[9] = new nodes[5] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, 1), new nodes(-1, 1), new nodes(-1, 2) };
        //documento segni n6
        Intensity1PlusMatrix[10] = new nodes[5] { new nodes(-1, 0), new nodes(-1, 1), new nodes(-2, 1), new nodes(-2, 2), new nodes(-1, 2) };
        Intensity1PlusMatrix[11] = new nodes[5] { new nodes(0, 1), new nodes(1, 1), new nodes(1, 0), new nodes(1, -1), new nodes(2, -1) };
        #endregion
        #region Intensity2
        //documento segni n1
        Intensity2Matrix[0] = new nodes[6] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, 1), new nodes(-1, 1), new nodes(-1, 2), new nodes(-2, 2) };
        Intensity2Matrix[1] = new nodes[6] { new nodes(0, 1), new nodes(0, 2), new nodes(1, 2), new nodes(1, 1), new nodes(1, 0), new nodes(2, 0) };
        //documento segni n2
        Intensity2Matrix[2] = new nodes[6] { new nodes(0, -1), new nodes(0, -2), new nodes(-1, -2), new nodes(-1, -1), new nodes(-2, -2), new nodes(-2, -1) };
        Intensity2Matrix[3] = new nodes[6] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, 1), new nodes(-1, 1), new nodes(0, 1), new nodes(0, 2) };
        //documento segni n3
        Intensity2Matrix[4] = new nodes[6] { new nodes(1, 0), new nodes(2, 0), new nodes(2, -1), new nodes(1, -1), new nodes(1, -2), new nodes(0, -2) };
        Intensity2Matrix[5] = new nodes[6] { new nodes(1, 0), new nodes(1, 1), new nodes(2, 1), new nodes(2, 2), new nodes(1, 2), new nodes(0, 2) };
        //documento segni n4
        Intensity2Matrix[6] = new nodes[6] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, -1), new nodes(-2, -2), new nodes(-1, -2), new nodes(0, -2) };
        Intensity2Matrix[7] = new nodes[6] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-2, 1), new nodes(-2, 2), new nodes(-1, 2), new nodes(0, 2) };
        #endregion
        #region Intensity2Plus
        //documento segni n1
        Intensity2PlusMatrix[0] = new nodes[7] { new nodes(0, -1), new nodes(0, -2), new nodes(-1, -2), new nodes(-2, -2), new nodes(-2, -1), new nodes(-2, 0), new nodes(-2, 1) };
        Intensity2PlusMatrix[1] = new nodes[7] { new nodes(0, -1), new nodes(0, -2), new nodes(0, -3), new nodes(1, -3), new nodes(2, -3), new nodes(2, -2), new nodes(2, -1) };
        //documento segni n2
        Intensity2PlusMatrix[2] = new nodes[7] { new nodes(0, -1), new nodes(1, -1), new nodes(1, -2), new nodes(0, -2), new nodes(-1, -2), new nodes(-1, -1), new nodes(-2, -1) };
        Intensity2PlusMatrix[3] = new nodes[7] { new nodes(1, 0), new nodes(1, -1), new nodes(2, -1), new nodes(3, -1), new nodes(3, 0), new nodes(2, 0), new nodes(2, 1) };
        //documento segni n3
        Intensity2PlusMatrix[4] = new nodes[7] { new nodes(1, 0), new nodes(2, 0), new nodes(2, 1), new nodes(2, 2), new nodes(2, 3), new nodes(1, 3), new nodes(1, 2) };
        //documento segni n4
        Intensity2PlusMatrix[5] = new nodes[7] { new nodes(0, -1), new nodes(1, -1), new nodes(1, -2), new nodes(2, -2), new nodes(2, -1), new nodes(2, 0), new nodes(2, 1) };
        Intensity2PlusMatrix[6] = new nodes[7] { new nodes(0, -1), new nodes(0, -2), new nodes(0, -3), new nodes(-1, -3), new nodes(-1, -2), new nodes(-2, -2), new nodes(-2, -1) };
        #endregion
        #region Intensity3
        //documento segni n1
        Intensity3Matrix[0] = new nodes[8] { new nodes(0, 1), new nodes(0, 2), new nodes(0, 3), new nodes(0, 4), new nodes(-1, 4), new nodes(-2, 4), new nodes(-2, 3), new nodes(-2, 2) };
        Intensity3Matrix[1] = new nodes[8] { new nodes(0, 1), new nodes(0, 2), new nodes(1, 2), new nodes(2, 2), new nodes(2, 1), new nodes(2, 0), new nodes(2, -1), new nodes(2, -2) };
        //documento segni n2
        Intensity3Matrix[2] = new nodes[8] { new nodes(0, -1), new nodes(0, -2), new nodes(-1, -2), new nodes(-2, -2), new nodes(-2, -1), new nodes(-2, 0), new nodes(-2, 1), new nodes(-1, 1) };
        Intensity3Matrix[3] = new nodes[8] { new nodes(-1, 0), new nodes(-1, -1), new nodes(-1, -2), new nodes(-1, -3), new nodes(0, -3), new nodes(1, -3), new nodes(1, -2), new nodes(1, -1) };
        //documento segni n3
        Intensity3Matrix[4] = new nodes[8] { new nodes(-1, 0), new nodes(-2, 0), new nodes(-3, 0), new nodes(-3, -1), new nodes(-3, -2), new nodes(-3, -3), new nodes(-2, -3), new nodes(-2, -2) };
        #endregion
        #region SpecialSign
        //2 casi segno Malevolent
        SpecialSignMatrix[0] = new nodes[6] { new nodes(1, 0), new nodes(1, 1), new nodes(1, 2), new nodes(0, 2), new nodes(-1, 2), new nodes(-1, 1) };
        SpecialSignMatrix[1] = new nodes[6] { new nodes(0, 1), new nodes(1, 1), new nodes(2, 1), new nodes(2, 0), new nodes(2, -1), new nodes(1, -1) };
        //2 casi di secret tecnique
        SpecialSignMatrix[2] = new nodes[7] { new nodes(-1, 0), new nodes(-1, -1), new nodes(-2, -1), new nodes(-2, 0), new nodes(-2, 1), new nodes(-2, 2), new nodes(1, -2) };
        SpecialSignMatrix[3] = new nodes[7] { new nodes(-1, 0), new nodes(-1, -1), new nodes(-1, -2), new nodes(-1, -3), new nodes(0, -3), new nodes(0, -2), new nodes(1, -2) };
        #endregion
    }

    public void CheckSign()
    {
        CheckCountBoxesLogicGrid();
        Searchextremity(grigliamanager.logicgrid);

        if (foundextremity == true)
        {
            CheckCorrectSign(CountBoxesActive);

        }
    }

    void CheckCountBoxesLogicGrid()
    {
        foreach (bool box in grigliamanager.logicgrid)
        {
            if (box == true)
            {
                CountBoxesActive++;
            }
        }
    }

    void Searchextremity(bool[,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int z = 0; z < grid.GetLength(1); z++)
            {
                if (grid[x, z] == true)
                {
                    //casi angoli 
                    if (x == Angle1.X && z == Angle1.Z)
                    {
                        if (grid[x + 1, z] == true ^ grid[x, z + 1] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle2.X && z == Angle2.Z)
                    {
                        if (grid[x, z + 1] == true ^ grid[x - 1, z] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle3.X && z == Angle3.Z)
                    {
                        if (grid[x + 1, z] == true ^ grid[x, z - 1] == true)
                        {
                            extremity.X = x;
                            extremity.Z = z;
                            foundextremity = true;
                        }
                    }
                    else if (x == Angle4.X && z == Angle4.Z)
                    {
                        if (grid[x, z - 1] == true ^ grid[x - 1, z] == true)
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

    void CheckCorrectSign(int caselleattive)
    {
        int countercorrectbox = 0;

        switch (caselleattive)
        {
            case 4:
                for (int i = 0; i < NormalYokaiMatrix.Length; i++)
                {
                    foreach (var nodo in NormalYokaiMatrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == NormalYokaiMatrix[0].Length)
                                {
                                    Kill4SignEnemies(i);
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
                    if (countercorrectbox == NormalYokaiMatrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < Intensity1Matrix.Length; i++)
                {
                    foreach (var nodo in Intensity1Matrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == Intensity1Matrix[0].Length)
                                {
                                    Kill5SignEnemies(i);
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
                    if (countercorrectbox == Intensity1Matrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                break;
            case 6:
                for (int i = 0; i < Intensity1PlusMatrix.Length; i++)
                {
                    foreach (var nodo in Intensity1PlusMatrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == Intensity1PlusMatrix[0].Length)
                                {
                                    Kill6SignEnemies(i);
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
                    if (countercorrectbox == Intensity1PlusMatrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                break;
            case 7:
                //prende dai segni di intensity2
                for (int i = 0; i < Intensity2Matrix.Length; i++)
                {
                    foreach (var nodo in Intensity2Matrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == Intensity2Matrix[0].Length)
                                {
                                    Kill7SignEnemies(i);
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
                    if (countercorrectbox == Intensity2Matrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                //prendi dai segni speciali, segno malevolent in questo caso primi due elementi dell'array
                for (int i = 0; i < 2; i++)
                {
                    foreach (var nodo in SpecialSignMatrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == SpecialSignMatrix[0].Length)
                                {
                                    KillMalevolentEnemy(i);
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
                    if (countercorrectbox == Intensity1Matrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                break;
            case 8:
                //prende dall'intensity 2 plus
                for (int i = 0; i < Intensity2PlusMatrix.Length; i++)
                {
                    foreach (var nodo in Intensity2PlusMatrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;

                                if (countercorrectbox == Intensity2PlusMatrix[0].Length)
                                {
                                    Kill8SignEnemies(i);
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
                    if (countercorrectbox == Intensity2PlusMatrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                //prendi dai segni speciali, segno secret in questo caso terzo e quarto elemento dell'array
                if (secretscript.active == true)
                {
                    for (int i = 2; i < 4; i++)
                    {
                        foreach (var nodo in SpecialSignMatrix[i])
                        {
                            if (extremity.X + nodo.X >= 0 &&
                                extremity.X + nodo.X <= 4 &&
                                extremity.Z + nodo.Z >= 0 &&
                                extremity.Z + nodo.Z <= 4)
                            {
                                if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                                {
                                    countercorrectbox++;

                                    if (countercorrectbox == SpecialSignMatrix[0].Length)
                                    {
                                        KillWithSecretTecnique(i);
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
                        if (countercorrectbox == Intensity1Matrix[0].Length)
                        {
                            countercorrectbox = 0;
                            break;
                        }
                    }
                }
                break;
            case 9:
                for (int i = 0; i < Intensity3Matrix.Length; i++)
                {
                    foreach (var nodo in Intensity3Matrix[i])
                    {
                        if (extremity.X + nodo.X >= 0 &&
                            extremity.X + nodo.X <= 4 &&
                            extremity.Z + nodo.Z >= 0 &&
                            extremity.Z + nodo.Z <= 4)
                        {
                            if (grigliamanager.logicgrid[extremity.X + nodo.X, extremity.Z + nodo.Z] == true)
                            {
                                countercorrectbox++;
                                if (countercorrectbox == Intensity3Matrix[0].Length)
                                {
                                    Kill9SignEnemies(i);
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
                    if (countercorrectbox == Intensity3Matrix[0].Length)
                    {
                        countercorrectbox = 0;
                        break;
                    }
                }
                break;
        }
    }

    void Kill4SignEnemies(int numerosegno)
    {
        //foreach normal enemies 4 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[0])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                NormalEnemy normalenemy = enemytodestroy.GetComponent<NormalEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (normalenemy.SignNormalYokai[0].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (normalenemy.SignNormalYokai[1].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (normalenemy.SignNormalYokai[2].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (normalenemy.SignNormalYokai[3].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (normalenemy.SignNormalYokai[4].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (normalenemy.SignNormalYokai[5].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
            }
        }
    }

    void Kill5SignEnemies(int numerosegno)
    {
        //foreach normal enemies 5 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[0])

        {

            if (enemytodestroy.activeInHierarchy == true)
            {

                NormalEnemy normalenemy = enemytodestroy.GetComponent<NormalEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (normalenemy.SignIntensity1Normal[0].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (normalenemy.SignIntensity1Normal[1].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (normalenemy.SignIntensity1Normal[2].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (normalenemy.SignIntensity1Normal[3].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (normalenemy.SignIntensity1Normal[4].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (normalenemy.SignIntensity1Normal[5].activeInHierarchy == true)

                    {

                        normalenemy.Deathforsign();

                    }

                }



            }

        }
        //foreach kamikaze enemies 5 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[1])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                KamikazeEnemy kamikazeEnemy = enemytodestroy.GetComponent<KamikazeEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[0].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[1].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[2].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[3].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[4].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (kamikazeEnemy.SignIntensity1Kamikaze[5].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
            }
        }
        //foreach armored enemies 5 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[2])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                ArmoredEnemy armoredEnemy = enemytodestroy.GetComponent<ArmoredEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (armoredEnemy.SignIntensity1Armored[0].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (armoredEnemy.SignIntensity1Armored[1].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (armoredEnemy.SignIntensity1Armored[2].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (armoredEnemy.SignIntensity1Armored[3].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (armoredEnemy.SignIntensity1Armored[4].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (armoredEnemy.SignIntensity1Armored[5].activeInHierarchy == true)
                    {
                        armoredEnemy.Deathforsign();
                    }
                }
            }
        }
        //foreach buffer enemies 5 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[6])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                BufferEnemy bufferEnemy = enemytodestroy.GetComponent<BufferEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (bufferEnemy.SignIntensity1Buffer[0].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (bufferEnemy.SignIntensity1Buffer[1].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (bufferEnemy.SignIntensity1Buffer[2].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (bufferEnemy.SignIntensity1Buffer[3].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (bufferEnemy.SignIntensity1Buffer[4].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (bufferEnemy.SignIntensity1Buffer[5].activeInHierarchy == true)
                    {
                        bufferEnemy.Deathforsign();
                    }
                }
            }
        }
    }

    void Kill6SignEnemies(int numerosegno)
    {
        //foreach normal enemies 6 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[0])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                NormalEnemy normalenemy = enemytodestroy.GetComponent<NormalEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (normalenemy.SignIntensity1PlusNormal[0].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (normalenemy.SignIntensity1PlusNormal[1].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (normalenemy.SignIntensity1PlusNormal[2].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (normalenemy.SignIntensity1PlusNormal[3].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (normalenemy.SignIntensity1PlusNormal[4].activeInHierarchy == true)
                    {
                        normalenemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (normalenemy.SignIntensity1PlusNormal[5].activeInHierarchy == true)
                    { 
                        normalenemy.Deathforsign();
                    }
                }
            }
        }
        //foreach kamikaze enemies 6 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[1])
        {
            if (enemytodestroy.activeInHierarchy == true)
            {
                KamikazeEnemy kamikazeEnemy = enemytodestroy.GetComponent<KamikazeEnemy>();
                if (numerosegno == 0 || numerosegno == 1)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[0].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 2 || numerosegno == 3)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[1].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 4 || numerosegno == 5)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[2].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 6 || numerosegno == 7)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[3].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 8 || numerosegno == 9)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[4].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
                if (numerosegno == 10 || numerosegno == 11)
                {
                    if (kamikazeEnemy.SignIntensity1PlusKamikaze[5].activeInHierarchy == true)
                    {
                        kamikazeEnemy.Deathforsign();
                    }
                }
            }
        }
        //foreach armored enemies 6 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[2])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                ArmoredEnemy armoredEnemy = enemytodestroy.GetComponent<ArmoredEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[0].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[1].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[2].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[3].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[4].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (armoredEnemy.SignIntensity1PlusArmored[5].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }



            }

        }
        //foreach buffer enemies 6 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[6])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                BufferEnemy bufferEnemy = enemytodestroy.GetComponent<BufferEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[0].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[1].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[2].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[3].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[4].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (bufferEnemy.SignIntensity1PlusBuffer[5].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }



            }

        }
    }

    void Kill7SignEnemies(int numerosegno)
    {
        //foreach frightening enemies 7 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[5])

        {

            if (enemytodestroy.activeInHierarchy == true)
            {

                FrighteningEnemy frighteningEnemy = enemytodestroy.GetComponent<FrighteningEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (frighteningEnemy.SignIntensity2Frightening[0].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (frighteningEnemy.SignIntensity2Frightening[1].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (frighteningEnemy.SignIntensity2Frightening[2].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (frighteningEnemy.SignIntensity2Frightening[3].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

            }

        }
        //foreach kamikaze enemies 7 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[1])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                KamikazeEnemy kamikazeEnemy = enemytodestroy.GetComponent<KamikazeEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[0].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[1].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[2].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[3].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[4].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (kamikazeEnemy.SignIntensity2Kamikaze[5].activeInHierarchy == true)

                    {

                        kamikazeEnemy.Deathforsign();

                    }

                }



            }

        }
        //foreach armored enemies 7 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[2])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                ArmoredEnemy armoredEnemy = enemytodestroy.GetComponent<ArmoredEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (armoredEnemy.SignIntensity2Armored[0].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (armoredEnemy.SignIntensity2Armored[1].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (armoredEnemy.SignIntensity2Armored[2].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (armoredEnemy.SignIntensity2Armored[3].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (armoredEnemy.SignIntensity2Armored[4].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (armoredEnemy.SignIntensity2Armored[5].activeInHierarchy == true)

                    {

                        armoredEnemy.Deathforsign();

                    }

                }



            }

        }
        //foreach buffer enemies 7 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[6])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                BufferEnemy bufferEnemy = enemytodestroy.GetComponent<BufferEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (bufferEnemy.SignIntensity2Buffer[0].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (bufferEnemy.SignIntensity2Buffer[1].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (bufferEnemy.SignIntensity2Buffer[2].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (bufferEnemy.SignIntensity2Buffer[3].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 8 || numerosegno == 9)

                {

                    if (bufferEnemy.SignIntensity2Buffer[4].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 10 || numerosegno == 11)

                {

                    if (bufferEnemy.SignIntensity2Buffer[5].activeInHierarchy == true)

                    {

                        bufferEnemy.Deathforsign();

                    }

                }



            }

        }
    }

    void Kill8SignEnemies(int numerosegno)
    {
        //foreach frightening enemies 8 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[5])

        {

            if (enemytodestroy.activeInHierarchy == true)
            {

                FrighteningEnemy frighteningEnemy = enemytodestroy.GetComponent<FrighteningEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (frighteningEnemy.SignIntensity2PlusFrightening[0].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (frighteningEnemy.SignIntensity2PlusFrightening[1].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4 || numerosegno == 5)

                {

                    if (frighteningEnemy.SignIntensity2PlusFrightening[2].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 6 || numerosegno == 7)

                {

                    if (frighteningEnemy.SignIntensity2PlusFrightening[3].activeInHierarchy == true)

                    {

                        frighteningEnemy.Deathforsign();

                    }

                }

            }

        }
    }

    void Kill9SignEnemies(int numerosegno)
    {
        //foreach undying enemies 9 sign
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[3])

        {

            if (enemytodestroy.activeInHierarchy == true)
            {

                UndyingEnemy undyingEnemy = enemytodestroy.GetComponent<UndyingEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    if (undyingEnemy.SignIntensity3Undying[0].activeInHierarchy == true)

                    {

                        undyingEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 2 || numerosegno == 3)

                {

                    if (undyingEnemy.SignIntensity3Undying[1].activeInHierarchy == true)

                    {

                        undyingEnemy.Deathforsign();

                    }

                }

                if (numerosegno == 4)

                {

                    if (undyingEnemy.SignIntensity3Undying[2].activeInHierarchy == true)

                    {

                        undyingEnemy.Deathforsign();

                    }

                }

            }

        }
    }

    void KillMalevolentEnemy(int numerosegno)
    {
        //foreach caso malevolent
        foreach (GameObject enemytodestroy in enemyspawnmanager.poolenemy[4])

        {

            if (enemytodestroy.activeInHierarchy == true)

            {

                MalevolentEnemy malevolentEnemy = enemytodestroy.GetComponent<MalevolentEnemy>();

                if (numerosegno == 0 || numerosegno == 1)

                {

                    malevolentEnemy.Deathforsign();

                }

            }

        }
    }

    void KillWithSecretTecnique(int numerosegno)
    {
        secretscript.Death();
    }
}
