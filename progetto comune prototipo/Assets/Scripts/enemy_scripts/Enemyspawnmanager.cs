using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawnmanager : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector]
    public Vector3 enemyspawnposition;
    [SerializeField]
    private GameObject[] enemyarray;
    public Dictionary<int, List<GameObject>> poolenemy = new Dictionary<int, List<GameObject>>();
    int enemyID;
    UIManager UIManager;
    public bool cansignspawn = false;
    public int enemykilled = 0;
    public float spawntimer = 2.5f;
    public Transform enemyparent;
    public float[] positionpossible = new float[5];
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //uimanager reference con nullcheck annesso
        UIManager = FindObjectOfType<UIManager>();
        if (UIManager == null)
        {
            Debug.LogError("UImanager is NULL!");
        }

        //metodo che riempie le dictionary pool di nemici 
        FillDictionary(enemyarray);

        //coroutine che spawnerà i nemici per tutta la durata del gioco 
        //StartCoroutine(SpawnEnemyCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region SPAWN ENEMY ROUTINE
    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            int randomenemyID = Random.Range(0, 7);
            int randomsegno = Random.Range(0, 6);
            int randomposition = Random.Range(0, 5);
            foreach (GameObject enemy in poolenemy[randomenemyID])
            {
                if (enemy.activeInHierarchy == false)
                {
                    if (randomposition == 0)
                    {
                        enemyspawnposition = new Vector3(positionpossible[randomposition], 1.3f, 0f);
                    }
                    else if (randomposition == 1)
                    {
                        enemyspawnposition = new Vector3(positionpossible[randomposition], 1.3f, 1f);
                    }
                    else if (randomposition == 2)
                    {
                        enemyspawnposition = new Vector3(positionpossible[randomposition], 1.3f, 2f);
                    }
                    else if (randomposition == 3)
                    {
                        enemyspawnposition = new Vector3(positionpossible[randomposition], 1.3f, 3f);
                    }
                    else if (randomposition == 4)
                    {
                        enemyspawnposition = new Vector3(positionpossible[randomposition], 1.3f, 4f);
                    }
                    enemy.transform.position = enemyspawnposition;

                    switch (randomenemyID)
                    {
                        case 0:
                            NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                            NormalEnemy.signnormalenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                NormalEnemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                NormalEnemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                NormalEnemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                NormalEnemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                NormalEnemy.baseSpeed = 1f;
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazeenemy = enemy.GetComponent<KamikazeEnemy>();
                            kamikazeenemy.signkamikazenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                kamikazeenemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                kamikazeenemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                kamikazeenemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                kamikazeenemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                kamikazeenemy.baseSpeed = 1f;
                            }
                            break;
                        case 2:
                            ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredEnemy.signarmoredenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                armoredEnemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                armoredEnemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                armoredEnemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                armoredEnemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                armoredEnemy.baseSpeed = 1f;
                            }
                            break;
                        case 3:
                            UndyingEnemy undyingEnemy = enemy.GetComponent<UndyingEnemy>();
                            undyingEnemy.signundyingenemy[randomsegno].gameObject.SetActive(true);
                            undyingEnemy.startingPosition = enemyspawnposition;
                            if (randomposition == 0)
                            {
                                undyingEnemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                undyingEnemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                undyingEnemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                undyingEnemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                undyingEnemy.baseSpeed = 1f;
                            }
                            break;
                        case 4:
                            MalevolentEnemy malevolentEnemy = enemy.GetComponent <MalevolentEnemy>();
                            malevolentEnemy.signmalevolentenemy[0].gameObject.SetActive(true);
                            malevolentEnemy.position = enemyspawnposition;
                            break;
                        case 5:
                            FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                            frighteningEnemy.signfrighteningenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                frighteningEnemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                frighteningEnemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                frighteningEnemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                frighteningEnemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                frighteningEnemy.baseSpeed = 1f;
                            }
                            break;
                        case 6:
                            BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                            bufferEnemy.signbufferenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                bufferEnemy.baseSpeed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                bufferEnemy.baseSpeed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                bufferEnemy.baseSpeed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                bufferEnemy.baseSpeed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                bufferEnemy.baseSpeed = 1f;
                            }
                            break;
                        default:
                            break;
                    }

                    enemy.SetActive(true);

                    break;
                }

            }

            yield return new WaitForSeconds(spawntimer);
        }
    }
    #endregion

    #region FILL ENEMIES POOL
    public void FillDictionary(GameObject[] prefabarray)
    {
        foreach (GameObject enemytospawn in prefabarray)
        {
            if (enemytospawn == prefabarray[0])
            {
                enemyID = prefabarray[0].GetComponent<NormalEnemy>().enemyID;
                List<GameObject> listaenemyNormale = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemyNormale);
                for (int i = 0; i < 10; i++)
                {
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }

            }
            else if (enemytospawn == prefabarray[1])
            {
                enemyID = prefabarray[1].GetComponent<KamikazeEnemy>().enemyID;
                List<GameObject> listaenemykamikaze = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemykamikaze);
                for (int i = 0; i < 10; i++)
                {
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }

            }
            else if (enemytospawn == prefabarray[2])
            {
                enemyID = prefabarray[2].GetComponent<ArmoredEnemy>().enemyID;
                List<GameObject> listaenemyarmored = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemyarmored);
                for (int i = 0; i < 10; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[3])
            {
                enemyID = prefabarray[3].GetComponent<UndyingEnemy>().enemyID;
                List<GameObject> listaenemyundying = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemyundying);
                for (int i = 0; i < 10; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[4])
            {
                enemyID = prefabarray[4].GetComponent<MalevolentEnemy>().enemyID;
                List<GameObject> listaenemymalevolent = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemymalevolent);
                for (int i = 0; i < 1; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[5])
            {
                enemyID = prefabarray[5].GetComponent<FrighteningEnemy>().enemyID;
                List<GameObject> listaenemyfrightening = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemyfrightening);
                for (int i = 0; i < 10; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }
            }
            else if(enemytospawn == prefabarray[6])
            {
                enemyID = prefabarray[6].GetComponent<BufferEnemy>().enemyID;
                List<GameObject> listaenemybuffer = new List<GameObject>();
                poolenemy.Add(enemyID, listaenemybuffer);
                for(int i = 0; i < 10; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolenemy[enemyID].Add(enemyinscene);
                }
            }
        }
    }
    #endregion
}
