using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemyspawnmanager : MonoBehaviour
{
    #region VARIABILI
    [HideInInspector]
    public Vector3 enemyspawnposition;
    [SerializeField]
    private GameObject[] enemyarray;
    public Dictionary<int, List<GameObject>> poolnemici = new Dictionary<int, List<GameObject>>();
    int nemicoID;
    UIManager UIManager;
    public bool cansignspawn = false;
    public int nemicoucciso = 0;
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
        RiempiDictionary(enemyarray);

        //coroutine che spawnerà i nemici per tutta la durata del gioco 
        StartCoroutine(SpawnEnemyCoroutine());

        //debug se tutto funziona nel riempimento delle dictionary
        /*foreach (KeyValuePair<int, List<GameObject>> pool in poolnemici)
        {
            Debug.Log("key: " + pool.Key);
            foreach (var value in pool.Value)
            {
                Debug.Log(value);
            }

        }*/
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
            int randomnemicoID = Random.Range(0, 7);
            int randomsegno = Random.Range(0, 2);
            int randomposition = Random.Range(0, 5);
            foreach (GameObject nemico in poolnemici[randomnemicoID])
            {
                if (nemico.activeInHierarchy == false)
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
                    nemico.transform.position = enemyspawnposition;

                    switch (randomnemicoID)
                    {
                        case 0:
                            NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                            NormalEnemy.segninormalenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                NormalEnemy.speed = 1.35f;
                            }
                            else if (randomposition == 1) 
                            {
                                NormalEnemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                NormalEnemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                NormalEnemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                NormalEnemy.speed = 1f;
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazeenemy = nemico.GetComponent<KamikazeEnemy>();
                            kamikazeenemy.segnikamikazenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                kamikazeenemy.speed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                kamikazeenemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                kamikazeenemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                kamikazeenemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                kamikazeenemy.speed = 1f;
                            }
                            break;
                        case 2:
                            GoldenEnemy goldenEnemy = nemico.GetComponent<GoldenEnemy>();
                            goldenEnemy.segnigoldenenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                goldenEnemy.speed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                goldenEnemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                goldenEnemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                goldenEnemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                goldenEnemy.speed = 1f;
                            }
                            break;
                        case 3:
                            ArmoredEnemy armoredEnemy = nemico.GetComponent<ArmoredEnemy>();
                            armoredEnemy.segniarmoredenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                armoredEnemy.speed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                armoredEnemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                armoredEnemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                armoredEnemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                armoredEnemy.speed = 1f;
                            }
                            break;
                        case 4:
                            UndyingEnemy undyingEnemy = nemico.GetComponent<UndyingEnemy>();
                            undyingEnemy.segniundyingenemy[randomsegno].gameObject.SetActive(true);
                            undyingEnemy.startingPosition = enemyspawnposition;
                            if (randomposition == 0)
                            {
                                undyingEnemy.speed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                undyingEnemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                undyingEnemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                undyingEnemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                undyingEnemy.speed = 1f;
                            }
                            break;
                        case 5:
                            MalevolentEnemy malevolentEnemy = nemico.GetComponent <MalevolentEnemy>();
                            malevolentEnemy.segnimalevolentenemy[0].gameObject.SetActive(true);
                            malevolentEnemy.position = enemyspawnposition;
                            break;
                        case 6:
                            FrighteningEnemy frighteningEnemy = nemico.GetComponent<FrighteningEnemy>();
                            frighteningEnemy.segnifrighteningenemy[randomsegno].gameObject.SetActive(true);
                            if (randomposition == 0)
                            {
                                frighteningEnemy.speed = 1.35f;
                            }
                            else if (randomposition == 1)
                            {
                                frighteningEnemy.speed = 1.27f;
                            }
                            else if (randomposition == 2)
                            {
                                frighteningEnemy.speed = 1.17f;
                            }
                            else if (randomposition == 3)
                            {
                                frighteningEnemy.speed = 1.07f;
                            }
                            else if (randomposition == 4)
                            {
                                frighteningEnemy.speed = 1f;
                            }
                            break;
                        default:
                            break;
                    }

                    nemico.SetActive(true);

                    break;
                }
                
            }

            yield return new WaitForSeconds(spawntimer);
        }
    }
    #endregion

    #region REMPIE POOL NEMICI
    public void RiempiDictionary(GameObject[] prefabarray)
    {
        foreach (GameObject enemytospawn in prefabarray)
        {
            if (enemytospawn == prefabarray[0])
            {
                nemicoID = prefabarray[0].GetComponent<NormalEnemy>().enemyID;
                List<GameObject> listanemicoNormale = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicoNormale);
                for (int i = 0; i < 5; i++)
                {
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }
            else if (enemytospawn == prefabarray[1])
            {
                nemicoID = prefabarray[1].GetComponent<KamikazeEnemy>().enemyID;
                List<GameObject> listanemicokamikaze = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicokamikaze);
                for (int i = 0; i < 5; i++)
                {
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }
            else if (enemytospawn == prefabarray[2])
            {
                nemicoID = prefabarray[2].GetComponent<GoldenEnemy>().enemyID;
                List<GameObject> listanemicogolden = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicogolden);
                for (int i = 0; i < 5; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[3])
            {
                nemicoID = prefabarray[3].GetComponent<ArmoredEnemy>().enemyID;
                List<GameObject> listanemicoarmored = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicoarmored);
                for (int i = 0; i < 5; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[4])
            {
                nemicoID = prefabarray[4].GetComponent<UndyingEnemy>().enemyID;
                List<GameObject> listanemicoundying = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicoundying);
                for (int i = 0; i < 5; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[5])
            {
                nemicoID = prefabarray[5].GetComponent<MalevolentEnemy>().enemyID;
                List<GameObject> listanemicomalevolent = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicomalevolent);
                for (int i = 0; i < 1; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
            else if (enemytospawn == prefabarray[6])
            {
                nemicoID = prefabarray[6].GetComponent<FrighteningEnemy>().enemyID;
                List<GameObject> listanemicofrightening = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemicofrightening);
                for (int i = 0; i < 5; i++)
                {
                    //Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
        }
    }
    #endregion
}
