using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inkstone : MonoBehaviour
{
    [Range(0, 100)]public int Ink = 100;
    public GameObject Layer1;
    public GameObject Layer2;
    public GameObject Layer3;
    public GameObject Layer4;
    void Start()
    {
            Layer1.SetActive(true);
            Layer2.SetActive(true);
            Layer3.SetActive(true);
            Layer4.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Ink<1)
        {
            SceneManager.LoadScene(2);
        }
        if(Ink < 25)
        {
            Layer1.SetActive(true);
            Layer2.SetActive(false);
        }
        if (Ink > 25 && Ink < 50)
        {
            Layer2.SetActive(true);
            Layer3.SetActive(false);
        }
        if (Ink > 50 && Ink < 75)
        {
            Layer3.SetActive(true);
            Layer4.SetActive(false);
        }
        if (Ink > 75)
        {
            Layer4.SetActive(true);
        }
        if (Ink > 100)
        {
            Ink = 100;
        }
    }
}
