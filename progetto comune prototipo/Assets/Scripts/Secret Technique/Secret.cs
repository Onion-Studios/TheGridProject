﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{
    [Range(0,100)] public int barra = 0;
    public int carica;

    Enemyspawnmanager enemyspawnmanager;
    Playerbehaviour playerbehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    void Death()
    {
      

    }
}
