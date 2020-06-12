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
