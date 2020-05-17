﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 4;
    [SerializeField]
    public float speed = 1;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signmalevolentenemy;
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

        pointsystem = FindObjectOfType<PointSystem>();
        if (pointsystem == null)
        {
            Debug.LogError("PointSystem is NULL");
        }

        // transform.position = new Vector3(-1.3f, 4f, 6.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-1.3f, 4f, 6.5f);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Deathforsign();
        }
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled  += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signmalevolentenemy)
        {
            segno.SetActive(false);
        }
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
    }


}
