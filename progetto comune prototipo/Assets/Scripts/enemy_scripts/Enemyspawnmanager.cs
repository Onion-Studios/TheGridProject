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
    public int nemicospawnato = 0;
    #endregion 


    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        if (UIManager == null)
        {
            Debug.LogError("UImanager is NULL!");
        }
        RiempiDictionary(enemyarray);
        StartCoroutine(SpawnEnemyCoroutine());

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
            int randomnemicoID = Random.Range(0, 2);
            foreach (GameObject nemico in poolnemici[randomnemicoID])
            {
                if (nemico.activeInHierarchy == false)
                {
                    enemyspawnposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    nemico.transform.position = enemyspawnposition;
                    nemico.GetComponent<Enemybehaviour>().segnocorrispondente = randomsegnoID;
                    nemico.SetActive(true);
                    nemicospawnato += 1;
                    break;
                }
                
            }

            if (nemicospawnato > 0 )
            {   
                foreach (Image segno in UIManager.Dictionaryofsignsprite[randomsegnoID])
                {
                    if (segno.gameObject.activeInHierarchy == false)
                    {
                        segno.transform.position = enemyspawnposition + new Vector3(0f, 1.5f, 0f);
                        segno.gameObject.SetActive(true);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(Random.Range(2f, 3f));
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
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }
            else if (nemicoID == 1)
            {
                List<GameObject> listanemico1 = new List<GameObject>();
                poolnemici.Add(nemicoID, listanemico1);
                for (int i = 0; i < 5; i++)
                {
                    GameObject enemyinscene = Instantiate(enemytospawn, Vector3.zero, Quaternion.identity);
                    poolnemici[nemicoID].Add(enemyinscene);
                }

            }

        }
    }
    #endregion
}
