using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferEnemy : MonoBehaviour
{
    public bool Buff;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    public GameObject[] segniBufferEnemy;
    public GameObject[] ToBuff;
    public int segnocorrispondente;
    int EnemyID0;
    int EnemyID1;
    int EnemyID2;
    public int link = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("playerbehaviour is NULL!");
        }

        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("enemyspawnmanager is NULL!");
        }

        Inkstone = FindObjectOfType<Inkstone>();
        if (Inkstone == null)
        {
            Debug.LogError("Inkstone is NULL!");
        }

        StartCoroutine(Test());

        EnemyID0 = FindObjectOfType<NormalEnemy>().enemyID;
        EnemyID1 = FindObjectOfType<KamikazeEnemy>().enemyID;
        EnemyID2 = FindObjectOfType<GoldenEnemy>().enemyID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedBoost()
    {
        for(int i=0; i<link; i++)
        {
            foreach(GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy==true)
                {
                    if (EnemyID0==0)
                    {
                        NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                        NormalEnemy.speed = NormalEnemy.speed * 1.25f;
                    }
                    if(EnemyID1==1)
                    {
                        KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                        KamikazeEnemy.speed = KamikazeEnemy.speed * 1.25f;
                    }
                    if(EnemyID2==2)
                    {
                        GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                        GoldenEnemy.speed = GoldenEnemy.speed * 1.25f;
                    }
                }
            }
        }
    }

    public void SpeedReset()
    {
        for (int i = 0; i < link; i++)
        {
            foreach (GameObject nemico in enemyspawnmanager.poolnemici[i])
            {
                if (nemico.activeInHierarchy == true)
                {
                    if (EnemyID0 == 0)
                    {
                        NormalEnemy NormalEnemy = nemico.GetComponent<NormalEnemy>();
                        NormalEnemy.speed = NormalEnemy.speed * 0.8f;
                    }
                    if (EnemyID1 == 1)
                    {
                        KamikazeEnemy KamikazeEnemy = nemico.GetComponent<KamikazeEnemy>();
                        KamikazeEnemy.speed = KamikazeEnemy.speed * 0.8f;
                    }
                    if (EnemyID2 == 2)
                    {
                        GoldenEnemy GoldenEnemy = nemico.GetComponent<GoldenEnemy>();
                        GoldenEnemy.speed = GoldenEnemy.speed * 0.8f;
                    }
                }
            }
        }
    }

    public void Deathforsign()
    {
        Buff = false;
        SpeedReset();
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        foreach (GameObject segno in segniBufferEnemy)
        {
            segno.SetActive(false);
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5.0f);
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            SpeedBoost();
            Debug.Log("Boost");
            yield return new WaitForSeconds(2.0f);
            SpeedReset();
            Debug.Log("Reset");
        }
    }
}
