﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grigliamanager : MonoBehaviour
{
    int colonne = 5;
    int righe = 5;
    public GameObject cubogriglia;
    private GameObject istanzacubo;
    List<GameObject> listadicubi = new List<GameObject>();
    [SerializeField]
    Material colorebasegriglia;


    void Start()
    {
        //quando parte il programma creo la la griglia di cubi e spawno il player in centro
        //solo se le righe e le colonne sono dispari se no non abbiamo un centro nella griglia


        if (colonne % 2 == 1 && righe % 2 == 1)
        {
            Creagriglia();
            Playerbehaviour player = FindObjectOfType<Playerbehaviour>();
            player.posizionepersonaggio = new Vector3((righe - 1) / 2, 1.05f, (colonne - 1) / 2);
            player.Spawn();
        }
        else
        {
            Debug.Log("inserisci un numero di righe e colonne dispari");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo che crea la griglia riga per riga istanziando i cubi 
    void Creagriglia()
    {
        for (int c = 0; c < colonne; c++)
        {
            for (int r = 0; r < righe; r++)
            {
                Vector3 posizionerighe = new Vector3(r, 0f, c);
                istanzacubo = Instantiate(cubogriglia, posizionerighe, Quaternion.identity, this.transform);
                listadicubi.Add(istanzacubo);
            }
        }
    }

    // cerca ogni elemento della lista dei cubi della gliglia
    // e fa tornare le caselle al colore bianco originale
    public void Resetcoloregriglia()
    {
        foreach (var cube in listadicubi)
        {
            cube.GetComponent<Renderer>().material = colorebasegriglia;
        }
    }
}
