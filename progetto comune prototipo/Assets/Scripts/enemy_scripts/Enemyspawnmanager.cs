using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawnmanager : MonoBehaviour
{
    public Vector3 enemyspawnposition;
    [SerializeField]
    private GameObject[] enemyarray;
    public Dictionary<int, List<GameObject>> poolnemici = new Dictionary<int, List<GameObject>>();
    int nemicoID;



    // Start is called before the first frame update
    void Start()
    {
        enemyspawnposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
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

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            int randomnemicoID = Random.Range(0, 2);
            foreach (GameObject nemico in poolnemici[randomnemicoID])
            {
                if (nemico.activeInHierarchy == false)
                {
                    nemico.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(Random.Range(2f, 3f));
        }
    }

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
}
