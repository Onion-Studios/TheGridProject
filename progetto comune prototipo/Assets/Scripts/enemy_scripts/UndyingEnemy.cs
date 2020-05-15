using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 3;
    [SerializeField]
    public float speed = 1;
    public int inkDamage = 20;
    public int maxInkDamage = 10;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signundyingenemy;
    public float endPosition;
    public float currentTime;
    public float maxTime;
    public float attackTimer;
    public float maxAttacktimer;
    public bool repelled;
    public Vector3 startingPosition;
    public float pushSpeed;
    public float baseSpeed;
    #endregion

    private void OnEnable()
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

        speed = baseSpeed;

        currentTime = maxTime;
        repelled = false;

    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DeathForTimer();

        if(repelled == false)
        {
            
           Enemymove();
        }
        else
        {
            UndyingRepelled();
        }

    }

    public void Enemymove()
    {

        if (this.transform.localPosition.x > endPosition)
        {
            DeathForStartGrid();
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    

    public void DeathForStartGrid()
    {
        //this.gameObject.SetActive(false);

        attackTimer -= 1 * Time.deltaTime;

        if (attackTimer < 0)
        {
            attackTimer = 0;
        }

        if(attackTimer == 0)
        {
            attackTimer = maxAttacktimer;

            Inkstone.Ink -= inkDamage;
            Inkstone.maxInk -= maxInkDamage;
            SecretT.bar = 0;
            enemyspawnmanager.enemykilled  = 0;

           

        }
    }

    public void UndyingRepelled()
    {
        transform.Translate(Vector3.left * pushSpeed * Time.deltaTime);
        if (this.transform.localPosition.x < startingPosition.x)
        {
            repelled = false;
            speed = baseSpeed;
        }
    }

    public void Deathforsign()
    {
        repelled = true;
        attackTimer = 0;
     
    }

    
    public void DeathForTimer()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = 0;
        }

        if(currentTime == 0)
        {
            this.gameObject.SetActive(false);
            attackTimer = 0;
            currentTime = maxTime;
            enemyspawnmanager.enemykilled  += 1;
            Inkstone.Ink += 10;
            SecretT.bar += SecretT.charge;
            foreach (GameObject segno in signundyingenemy)
            {
                segno.SetActive(false);

            }

            pointsystem.currentTimer = pointsystem.maxTimer;
            pointsystem.countercombo++;

            pointsystem.Combo();

            pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;
        }

       
    }

}
