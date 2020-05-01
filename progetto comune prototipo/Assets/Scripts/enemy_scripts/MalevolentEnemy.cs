using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 0;
    [SerializeField]
    public float speed = 1;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    public GameObject[] segnimalevolentenemy;
    public Vector3 position;
    public float spawntimer;
    public float maxspawntimer;
    #endregion

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

        SecretT = FindObjectOfType<Secret>();
        if (SecretT == null)
        {
            Debug.LogError("Secret is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (this.transform.localPosition.x > position.x)
        {
           
        }
    }



    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkstoneDamage;
        foreach (GameObject segno in segnimalevolentenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        SecretT.barra = 0;
        enemyspawnmanager.nemicoucciso = 0;
        foreach (GameObject segno in segnimalevolentenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        SecretT.barra += SecretT.carica;
        foreach (GameObject segno in segnimalevolentenemy)
        {
            segno.SetActive(false);
        }
    }

    public void SpawnTimer()
    {
        spawntimer -= 1 * Time.deltaTime;

        if (spawntimer < 0)
        {
            spawntimer = 0;
        }

        if (spawntimer == 0)
        {
            this.gameObject.SetActive(false);
           
            spawntimer = maxspawntimer;
            enemyspawnmanager.nemicoucciso += 1;
            Inkstone.Ink += 10;
            SecretT.barra += SecretT.carica;
            foreach (GameObject segno in segnimalevolentenemy)
            {
                segno.SetActive(false);
            }
        }
    }
}
