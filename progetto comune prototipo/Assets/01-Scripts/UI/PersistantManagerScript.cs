using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantManagerScript : MonoBehaviour
{
    public static int HighScore;

    public void Start()
    {
        HighScore=PlayerPrefs.GetInt("HighScore");
    }
}
