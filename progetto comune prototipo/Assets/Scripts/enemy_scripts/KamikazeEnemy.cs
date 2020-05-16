using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 1;
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
    public GameObject[] signkamikazenemy;
    public float baseSpeed;

    private Collider[] hitColliders;
    public float blastRadius;
    public float explosionPower;
    public LayerMask explosionLayers;
    public float startPosition;
    public float totalScore;
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

        startPosition = transform.position.x;

    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Enemymove();

        PointOverDistance();
    }
        

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > 2)
        {
            DeathForEndGrid();
        }
    }

    public void DeathForEndGrid()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= 10;
        foreach (GameObject segno in signkamikazenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        Inkstone.Ink -= inkDamage;
        Inkstone.maxInk -= maxInkDamage;
        SecretT.bar = 0;
        enemyspawnmanager.enemykilled  = 0;
        foreach (GameObject segno in signkamikazenemy)
        {
            segno.SetActive(false);
        }
    }

    public void Deathforsign()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled  += 1;
        Inkstone.Ink += 10;
        SecretT.bar += SecretT.charge;
        foreach (GameObject segno in signkamikazenemy)
        {
            segno.SetActive(false);
        }

       

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (totalScore + scoreEnemy) * pointsystem.scoreMultiplier;
    }

    void PointOverDistance()
    {

        if (this.transform.position.x < -0.5f)
        {

            totalScore = scoreEnemy + scoreEnemy / (Mathf.Abs(startPosition) - 0.5f) * transform.position.x;
        }
        else
        {
            totalScore = scoreEnemy;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
        ExplosionWork(col.contacts[0].point);
    }

    void ExplosionWork(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
        foreach (Collider hitCol in hitColliders)
        {
            if (hitCol.GetComponent<Rigidbody>() != null)
            {
                hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
            }
        }
    }

}


