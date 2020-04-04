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
        foreach (KeyValuePair<int, List<GameObject>> pool in poolnemici)
        {
            Debug.Log("key: " + pool.Key);
            foreach (var value in pool.Value)
            {
                Debug.Log(value);
            }

        }
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
            int randomnemicoID = Random.Range(0, 3);
            int randomsegno = Random.Range(0, 4);
            foreach (GameObject nemico in poolnemici[randomnemicoID])
            {
                if (nemico.activeInHierarchy == false)
                {
                    enemyspawnposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    nemico.transform.position = enemyspawnposition;

                    switch (randomnemicoID)
                    {
                        case 0:
                            nemico.GetComponent<NormalEnemy>().segninormalenemy[randomsegno].gameObject.SetActive(true);                            
                            break;
                        case 1:
                            nemico.GetComponent<KamikazeEnemy>().segnikamikazenemy[randomsegno].gameObject.SetActive(true);
                            break;
                        case 2:
                            nemico.GetComponent<GoldenEnemy>().segnigoldenenemy[randomsegno].gameObject.SetActive(true);
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
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity, enemyparent);
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
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity, enemyparent);
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
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity, enemyparent);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
        }
    }
    #endregion
}
