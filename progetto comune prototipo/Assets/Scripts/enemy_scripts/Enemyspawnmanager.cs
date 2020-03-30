using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemyspawnmanager : MonoBehaviour
{
    #region VARIABILI
    public Vector3 enemyspawnposition;
    [SerializeField]
    private GameObject[] enemyarray;
    public Dictionary<int, List<GameObject>> poolnemici = new Dictionary<int, List<GameObject>>();
    int nemicoID;
    UIManager UIManager;
    public bool cansignspawn = false;
    public int nemicoucciso = 0;
    public float spawntimer = 2.5f;
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
            int randomsegnoID = Random.Range(0, 2);
            int randomnemicoID = Random.Range(0, 4);
            foreach (GameObject nemico in poolnemici[randomnemicoID])
            {
                if (nemico.activeInHierarchy == false)
                {
                    enemyspawnposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    nemico.transform.position = enemyspawnposition;
                    nemico.GetComponent<Enemybehaviour>().segnocorrispondente = randomsegnoID;
                    nemico.SetActive(true);
                    break;
                }
                
            }

            foreach (Image segno in UIManager.Dictionaryofsignsprite[randomsegnoID])
            {
                if (segno.gameObject.activeInHierarchy == false)
                {
                    segno.transform.position = enemyspawnposition + new Vector3(0f, 1.5f, 0f);
                    segno.gameObject.SetActive(true);
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
            nemicoID = enemytospawn.GetComponent<Enemybehaviour>().enemyID;

            if (nemicoID == 0)
            {
                List<GameObject> listanemico0 = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemico0);
                for (int i = 0; i < 5; i++)
                {
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }
            else if (nemicoID == 1)
            {
                List<GameObject> listanemico1 = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemico1);
                for (int i = 0; i < 5; i++)
                {
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }
            else if (nemicoID == 2)
            {
                List<GameObject> listanemico2 = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemico2);
                for (int i = 0; i < 5; i++)
                {
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }
            else if (nemicoID == 3)
            {
                List<GameObject> listanemico3 = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemico3);
                for (int i = 0; i < 5; i++)
                {
                    Vector3 posizionetospawn = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    GameObject enemyinscene = Instantiate(enemytospawn, posizionetospawn, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }
            }

        }
    }
    #endregion
}
