﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 3;
    [SerializeField]
    public float speed = 1;
    public float maxSpeed;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] segniarmoredenemy;
    public int armoredLife =2;
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
        Inkstone.Ink -= inkstoneDamage;
        foreach (GameObject segno in segniarmoredenemy)
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
        foreach (GameObject segno in segniarmoredenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforsign()
    {
        if (armoredLife == 2)
        {
            speed = maxSpeed;
            armoredLife -= 1;
            foreach (GameObject segno in segniarmoredenemy)
            {
                segno.SetActive(false);
            }
            int randomsegno = Random.Range(0, 2);
            segniarmoredenemy[randomsegno].gameObject.SetActive(true);
        }
        else
        {
            armoredLife = 2;
            enemyspawnmanager.nemicoucciso += 2;
            Inkstone.Ink += 10;
            SecretT.barra += SecretT.carica;
            foreach (GameObject segno in segniarmoredenemy)
            {
                segno.SetActive(false);
            }
            this.gameObject.SetActive(false);
        }

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy;

    }
}
