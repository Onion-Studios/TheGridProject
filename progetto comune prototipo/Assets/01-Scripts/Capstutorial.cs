using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capstutorial : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text.ToUpper();
    }
}
