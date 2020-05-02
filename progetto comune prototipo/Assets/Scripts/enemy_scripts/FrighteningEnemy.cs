using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrighteningEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 6;
    [SerializeField]
    public float speed = 1;
    public float playerSpeed;
    public float reduceSpeed;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] segnifrighteningenemy;
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
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();

        Frate();
    }

    public void Frate()
    {
        if(reduceSpeed != playerbehaviour.speed)
        {
            playerbehaviour.speed = reduceSpeed;

        }

    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > 4.24)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= 10;
        foreach (GameObject segno in segnifrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        SecretT.barra = 0;
        enemyspawnmanager.nemicoucciso = 0;
        foreach (GameObject segno in segnifrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        SecretT.barra += SecretT.carica;
        foreach (GameObject segno in segnifrighteningenemy)
        {
            segno.SetActive(false);
        }

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
    }
}
