using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemybehaviour : MonoBehaviour
{
    #region VARIABILI
    public int enemyID; 
    [SerializeField]
    float speed = 1f;
    Enemyspawnmanager enemyspawnmanager;
    public int segnocorrispondente; 
    #endregion

    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        Playerbehaviour = FindObjectOfType<Playerbehaviour>();
    }

    void Update()
    {
        if(this.gameObject.activeInHierarchy == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            
            if (this.transform.localPosition.x > -0.76)
            {
                Death();
                Playerbehaviour.life -= 1;
            }
        }
    }


    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso = 0;
        Vector3 randominitialposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
        transform.position = randominitialposition;
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Vector3 randominitialposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
        transform.position = randominitialposition;
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > -0.76)
        {
            Deathforgriglia();
        }
    }
}
