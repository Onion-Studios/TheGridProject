﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrighteningEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 5;
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
    public GameObject[] signfrighteningenemy;
    public float baseSpeed;
    #endregion

    private void OnEnable()
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

        speed = baseSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();

        Frightening();
    }

    public void Frightening()
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
        foreach (GameObject segno in signfrighteningenemy)
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
        SecretT.bar = 0;
        enemyspawnmanager.enemykilled  = 0;
        foreach (GameObject segno in signfrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;


    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled  += 1;
        Inkstone.Ink += 10;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signfrighteningenemy)
        {
            segno.SetActive(false);
        }

        playerbehaviour.speed = playerSpeed;

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
    }
}
