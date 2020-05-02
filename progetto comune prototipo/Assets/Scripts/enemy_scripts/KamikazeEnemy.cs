using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 1;
    [SerializeField]
    public float speed = 1;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    BufferEnemy bufferenemy;
    public int scoreEnemy;
    public GameObject[] segnikamikazenemy;
    public int segnocorrispondente;
    public bool isbuffed;
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

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }

        bufferenemy = FindObjectOfType<BufferEnemy>();
        if (bufferenemy == null)
        {
            Debug.LogError("BufferEnemy is NULL");
        }

        isbuffed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > 2)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= 10;
        foreach (GameObject segno in segnikamikazenemy)
        {
            segno.SetActive(false);
        }

        if (isbuffed == true)
        {
            speed = speed / bufferenemy.Boost;
            isbuffed = false;
        }
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        SecretT.barra = 0;
        enemyspawnmanager.nemicoucciso = 0;
        foreach (GameObject segno in segnikamikazenemy)
        {
            segno.SetActive(false);
        }

        if (isbuffed == true)
        {
            speed = speed / bufferenemy.Boost;
            isbuffed = false;
        }
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        SecretT.barra += SecretT.carica;
        foreach (GameObject segno in segnikamikazenemy)
        {
            segno.SetActive(false);
        }

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;


        if (isbuffed == true)
        {
            speed = speed / bufferenemy.Boost;
            isbuffed = false;
        }
    }
}
