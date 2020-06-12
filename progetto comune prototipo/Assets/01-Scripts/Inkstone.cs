using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inkstone : MonoBehaviour
{
    public int maxInk = 100;
    public int Ink = 100;
    bool resetonending;
    public GameObject Layer1;
    public GameObject Layer2;
    public GameObject Layer3;
    public GameObject Layer4;
    public GameObject Layer5;
    public static int FinalScore;
    PointSystem PointSystem;
    StartEndSequence endSequence;
    

    void Start()
    {
        Layer1.SetActive(true);
        Layer2.SetActive(true);
        Layer3.SetActive(true);
        Layer4.SetActive(true);
        Layer5.SetActive(true);
        PointSystem = FindObjectOfType<PointSystem>();
        endSequence = FindObjectOfType<StartEndSequence>();
        resetonending = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInk();
    }

    void UpdateInk()
    {
        if (Ink < 0)
        {
            Ink = 0;
        }
        if (maxInk < 0)
        {
            maxInk = 0;
        }
        if (Ink == 0)
        {
            endSequence.ending = true;
            resetonending = true;
        }
        if (Ink == 1)
        {
            Layer1.SetActive(true);
            Layer2.SetActive(false);
        }
        if (Ink < 25 && Ink >= 2)
        {
            Layer2.SetActive(true);
            Layer3.SetActive(false);
            Layer4.SetActive(false);
            Layer5.SetActive(false);
        }
        if (Ink >= 25 && Ink < 50)
        {
            Layer3.SetActive(true);
            Layer4.SetActive(false);
            Layer5.SetActive(false);
        }
        if (Ink >= 50 && Ink < 75)
        {
            Layer4.SetActive(true);
            Layer5.SetActive(false);
        }
        if (Ink >= 75 && Ink <= 100)
        {
            Layer5.SetActive(true);
        }
        if (Ink > maxInk)
        {
            Ink = maxInk;
        }
        if (resetonending == true)
        {
            Ink = 0;
        }
    }
}
