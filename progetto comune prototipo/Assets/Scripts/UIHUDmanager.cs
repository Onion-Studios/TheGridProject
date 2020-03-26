using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDmanager : MonoBehaviour
{
    [SerializeField]
    Text Enemycounter_text;
    Enemyspawnmanager Enemyspawnmanager;

    // Start is called before the first frame update
    void Start()
    {
        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemycounter();
    }

    private void UpdateEnemycounter()
    {
        Enemycounter_text.text = "Counter nemici: " + Enemyspawnmanager.nemicoucciso;
    }
}
