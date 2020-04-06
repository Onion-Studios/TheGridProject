using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    public int enemyID = 1;
    [SerializeField]
    public float speed = 1;
    public int damage = 2;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    public GameObject[] segnikamikazenemy;
    public int segnocorrispondente;


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

    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > -0.76)
        {
            Deathforgriglia();
        }
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        playerbehaviour.life -= damage;
        enemyspawnmanager.nemicoucciso = 0;
        foreach (GameObject segno in segnikamikazenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        foreach (GameObject segno in segnikamikazenemy)
        {
            segno.SetActive(false);
        }
    }
}
