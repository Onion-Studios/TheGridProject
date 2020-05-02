using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentEnemy : MonoBehaviour
{
    #region VARIABILI
    public int enemyID = 5;
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
    BufferEnemy bufferenemy;
    public int scoreEnemy;
    public GameObject[] segnimalevolentenemy;
    public Vector3 position;
    public float spawntimer;
    public float maxspawntimer;
    public bool isbuffed;
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

        bufferenemy = FindObjectOfType<BufferEnemy>();
        if (bufferenemy == null)
        {
            Debug.LogError("BufferEnemy is NULL");
        }

        isbuffed = false;

        // transform.position = new Vector3(-1.3f, 4f, 6.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-1.3f, 4f, 6.5f);
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.nemicoucciso += 1;
        Inkstone.Ink += 10;
        SecretT.barra += SecretT.carica;
        foreach (GameObject segno in segnimalevolentenemy)
        {
            segno.SetActive(false);
        }
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;

        if (isbuffed == true)
        {
            speed = speed / bufferenemy.Boost;
            isbuffed = false;
        }
    }

}
