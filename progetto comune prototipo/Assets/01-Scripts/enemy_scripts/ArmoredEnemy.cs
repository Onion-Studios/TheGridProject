using System.Collections;
using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 2;
    [SerializeField]
    public float speed, baseSpeedMax;
    public int inkDamage, maxInkDamage, inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    GameManager GameManager;
    Managercombo Managercombo;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    GameManager GM;
    WaveManager WM;
    public int scoreEnemy, armoredLife;
    public GameObject[] signarmoredenemy;
    public float baseSpeed, startPosition, extrapointsoverdistance, startGrid, BlackToDeath;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject armor1;
    [SerializeField]
    private GameObject armor2;
    [SerializeField]
    private GameObject armor3;
    [SerializeField]
    private GameObject armor4;
    [SerializeField]
    private GameObject bandana;
    [SerializeField]
    private ParticleSystem inkDeath;
    public ParticleSystem buffEffect;
    [SerializeField]
    private ParticleSystem inkAbsorb;
    public float stopTime, waitTime;
    IEnumerator deathforendgrid;
    bool destinationReached;
    public GameObject[] SignIntensity1Armored;
    public GameObject[] SignIntensity1PlusArmored;
    public GameObject[] SignIntensity2Armored;
    public Animator armoredAnimator;
    [SerializeField]
    private GameObject armorPiece1;
    [SerializeField]
    private GameObject armorPiece2;
    [SerializeField]
    private GameObject armorPiece3;
    [SerializeField]
    private GameObject armorPiece4;
    [SerializeField]
    private GameObject armorPiece5;
    [SerializeField]
    private GameObject armorPiece6;
    [SerializeField]
    private GameObject armorPiece7;
    private Vector3 armorPiecePos1;
    private Vector3 armorPiecePos2;
    private Vector3 armorPiecePos3;
    private Vector3 armorPiecePos4;
    private Vector3 armorPiecePos5;
    private Vector3 armorPiecePos6;
    private Vector3 armorPiecePos7;
    private Quaternion armorPieceRotation1;
    private Quaternion armorPieceRotation2;
    private Quaternion armorPieceRotation3;
    private Quaternion armorPieceRotation4;
    private Quaternion armorPieceRotation5;
    private Quaternion armorPieceRotation6;
    private Quaternion armorPieceRotation7;
    [SerializeField]
    private Transform armorPieceParent1;
    [SerializeField]
    private Transform armorPieceParent2;
    [SerializeField]
    private Transform armorPieceParent3;
    [SerializeField]
    private Transform armorPieceParent4;
    [SerializeField]
    private Transform armorPieceParent5;
    [SerializeField]
    private Transform armorPieceParent6;
    [SerializeField]
    private Transform armorPieceParent7;

    #endregion

    private void Awake()
    {
        armoredAnimator = GetComponentInChildren<Animator>();
        armorPiecePos1 = armorPiece1.transform.localPosition;
        armorPiecePos2 = armorPiece2.transform.localPosition;
        armorPiecePos3 = armorPiece3.transform.localPosition;
        armorPiecePos4 = armorPiece4.transform.localPosition;
        armorPiecePos5 = armorPiece5.transform.localPosition;
        armorPiecePos6 = armorPiece6.transform.localPosition;
        armorPiecePos7 = armorPiece7.transform.localPosition;

        armorPieceRotation1 = armorPiece1.transform.localRotation;
        armorPieceRotation2 = armorPiece2.transform.localRotation;
        armorPieceRotation3 = armorPiece3.transform.localRotation;
        armorPieceRotation4 = armorPiece4.transform.localRotation;
        armorPieceRotation5 = armorPiece5.transform.localRotation;
        armorPieceRotation6 = armorPiece6.transform.localRotation;
        armorPieceRotation7 = armorPiece7.transform.localRotation;

        armorPiece1.AddComponent<Rigidbody>();
        armorPiece2.AddComponent<Rigidbody>();
        armorPiece3.AddComponent<Rigidbody>();
        armorPiece4.AddComponent<Rigidbody>();
        armorPiece5.AddComponent<Rigidbody>();
        armorPiece6.AddComponent<Rigidbody>();
        armorPiece7.AddComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        armorPiece1.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece2.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece3.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece4.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece5.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece6.GetComponent<Rigidbody>().isKinematic = true;
        armorPiece7.GetComponent<Rigidbody>().isKinematic = true;

        armoredAnimator.SetBool("ArmorBroken", false);
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

        GameManager = FindObjectOfType<GameManager>();
        if (GameManager == null)
        {
            Debug.LogError("Gamemanager is NULL");
        }

        GM = FindObjectOfType<GameManager>();
        if (GM == null)
        {
            Debug.LogError("GameManager is NULL");
        }

        WM = FindObjectOfType<WaveManager>();
        if (WM == null)
        {
            Debug.LogError("Wave Manager is NULL");
        }

        Managercombo = FindObjectOfType<Managercombo>();
        if (Managercombo == null)
        {
            Debug.LogError("Managercombo is NULL");
        }

        speed = baseSpeed;

        startPosition = transform.position.x;

        armoredLife = 2;

        deathforendgrid = null;

        destinationReached = false;


        armorPiece1.transform.SetParent(armorPieceParent1);
        armorPiece2.transform.SetParent(armorPieceParent2);
        armorPiece3.transform.SetParent(armorPieceParent3);
        armorPiece4.transform.SetParent(armorPieceParent4);
        armorPiece5.transform.SetParent(armorPieceParent5);
        armorPiece6.transform.SetParent(armorPieceParent6);
        armorPiece7.transform.SetParent(armorPieceParent7);

        armorPiece1.SetActive(true);
        armorPiece2.SetActive(true);
        armorPiece3.SetActive(true);
        armorPiece4.SetActive(true);
        armorPiece5.SetActive(true);
        armorPiece6.SetActive(true);
        armorPiece7.SetActive(true);

    }

    private void OnDisable()
    {
        if (deathforendgrid != null)
        {
            StopCoroutine(deathforendgrid);
            deathforendgrid = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        armoredAnimator.SetFloat("CurrentPosition", transform.position.x);
        if (destinationReached == false)
        {
            Enemymove();
        }
        else
        {
            if (deathforendgrid == null)
            {
                deathforendgrid = DeathForEndGrid();
                StartCoroutine(deathforendgrid);
            }
        }

        PointOverDistance();
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (this.transform.localPosition.x > 3.75)
        {
            inkAbsorb.Play();
            AudioManager.Instance.PlaySound("Backwash");
            Invoke("DeathForEndGrid", stopTime);
            destinationReached = true;
        }
    }

    public IEnumerator DeathForEndGrid()
    {
        yield return new WaitForSeconds(waitTime);
        inkAbsorb.Stop();
        playerbehaviour.ReceiveDamage(inkstoneDamage, 0);
        //foreach segni intensity 1
        foreach (GameObject segno in SignIntensity1Armored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 1 plus
        foreach (GameObject segno in SignIntensity1PlusArmored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 2
        foreach (GameObject segno in SignIntensity2Armored)

        {

            segno.SetActive(false);

        }
        Die();
    }

    void Die()
    {
        if (deathforendgrid != null)
        {
            StopCoroutine(deathforendgrid);
            deathforendgrid = null;
        }
        this.gameObject.SetActive(false);
        ArmorReset();
    }

    public void Deathforgriglia()
    {
        inkDeath.Play();
        enemy.GetComponent<Renderer>().material.color = Color.black;
        armor1.GetComponent<Renderer>().material.color = Color.black;
        armor2.GetComponent<Renderer>().material.color = Color.black;
        armor3.GetComponent<Renderer>().material.color = Color.black;
        armor4.GetComponent<Renderer>().material.color = Color.black;
        bandana.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);

        ArmorReset();
        playerbehaviour.ReceiveDamage(inkDamage, maxInkDamage);
        //foreach segni intensity 1
        foreach (GameObject segno in SignIntensity1Armored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 1 plus
        foreach (GameObject segno in SignIntensity1PlusArmored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 2
        foreach (GameObject segno in SignIntensity2Armored)

        {

            segno.SetActive(false);

        }
        AudioManager.Instance.PlaySound("EnemyDeath");
    }
    public void Deathforsign()
    {
        if (armoredLife == 2)
        {
            speed = baseSpeedMax + GM.intensitySpeedIncrease;
            armoredLife -= 1;
            armoredAnimator.SetBool("ArmorBroken", true);

            armorPiece1.transform.SetParent(null);
            armorPiece1.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece2.transform.SetParent(null);
            armorPiece2.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece3.transform.SetParent(null);
            armorPiece3.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece4.transform.SetParent(null);
            armorPiece4.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece5.transform.SetParent(null);
            armorPiece5.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece6.transform.SetParent(null);
            armorPiece6.GetComponent<Rigidbody>().isKinematic = false;
            armorPiece7.transform.SetParent(null);
            armorPiece7.GetComponent<Rigidbody>().isKinematic = false;
            //foreach segni intensity 1
            foreach (GameObject segno in SignIntensity1Armored)

            {

                segno.SetActive(false);

            }
            //foreach segni intensity 1 plus
            foreach (GameObject segno in SignIntensity1PlusArmored)

            {

                segno.SetActive(false);

            }
            //foreach segni intensity 2
            foreach (GameObject segno in SignIntensity2Armored)

            {

                segno.SetActive(false);

            }
            int randomsegno;
            if (GM.GameIntensity != 3 || WM.TEST_WaveIntensity != 3)
            {
                randomsegno = Random.Range(0, 6);
            }
            else
            {
                randomsegno = Random.Range(0, 4);
            }
            int randomsegnofour = Random.Range(0, 4);
            switch (GameManager.GameIntensity)

            {

                case 1:

                    SignIntensity1Armored[randomsegno].gameObject.SetActive(true);

                    break;

                case 2:

                    SignIntensity1PlusArmored[randomsegno].gameObject.SetActive(true);

                    break;

                case 3:

                    SignIntensity2Armored[randomsegnofour].gameObject.SetActive(true);

                    break;

            }
        }
        else
        {
            armoredLife = 2;
            enemyspawnmanager.enemykilled += 2;
            Inkstone.Ink += playerbehaviour.inkGained;
            SecretT.bar += SecretT.charge;
            //foreach segni intensity 1
            foreach (GameObject segno in SignIntensity1Armored)

            {

                segno.SetActive(false);

            }
            //foreach segni intensity 1 plus
            foreach (GameObject segno in SignIntensity1PlusArmored)

            {

                segno.SetActive(false);

            }
            //foreach segni intensity 2
            foreach (GameObject segno in SignIntensity2Armored)

            {

                segno.SetActive(false);

            }
            inkDeath.Play();
            enemy.GetComponent<Renderer>().material.color = Color.black;
            armor1.GetComponent<Renderer>().material.color = Color.black;
            armor2.GetComponent<Renderer>().material.color = Color.black;
            armor3.GetComponent<Renderer>().material.color = Color.black;
            armor4.GetComponent<Renderer>().material.color = Color.black;
            bandana.GetComponent<Renderer>().material.color = Color.black;
            Invoke("Death", BlackToDeath);
        }
    }
    public void DeathForCollision()
    {
        this.gameObject.SetActive(false);

        enemy.GetComponent<Renderer>().material.color = Color.white;
        armor1.GetComponent<Renderer>().material.color = Color.white;
        armor2.GetComponent<Renderer>().material.color = Color.white;
        armor3.GetComponent<Renderer>().material.color = Color.white;
        armor4.GetComponent<Renderer>().material.color = Color.white;
        bandana.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Death()
    {
        armoredLife = 2;
        enemyspawnmanager.enemykilled += 2;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        //foreach segni intensity 1
        foreach (GameObject segno in SignIntensity1Armored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 1 plus
        foreach (GameObject segno in SignIntensity1PlusArmored)

        {

            segno.SetActive(false);

        }
        //foreach segni intensity 2
        foreach (GameObject segno in SignIntensity2Armored)

        {

            segno.SetActive(false);

        }
        this.gameObject.SetActive(false);

        ArmorReset();

        enemy.GetComponent<Renderer>().material.color = Color.white;
        armor1.GetComponent<Renderer>().material.color = Color.white;
        armor2.GetComponent<Renderer>().material.color = Color.white;
        armor3.GetComponent<Renderer>().material.color = Color.white;
        armor4.GetComponent<Renderer>().material.color = Color.white;
        bandana.GetComponent<Renderer>().material.color = Color.white;
        AudioManager.Instance.PlaySound("EnemyDeath");

        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += (extrapointsoverdistance + scoreEnemy) * pointsystem.scoreMultiplier;
    }

    private void ArmorReset()
    {
        armorPiece1.SetActive(false);
        armorPiece2.SetActive(false);
        armorPiece3.SetActive(false);
        armorPiece4.SetActive(false);
        armorPiece5.SetActive(false);
        armorPiece6.SetActive(false);
        armorPiece7.SetActive(false);

        armorPiece1.transform.SetParent(armorPieceParent1);
        armorPiece2.transform.SetParent(armorPieceParent2);
        armorPiece3.transform.SetParent(armorPieceParent3);
        armorPiece4.transform.SetParent(armorPieceParent4);
        armorPiece5.transform.SetParent(armorPieceParent5);
        armorPiece6.transform.SetParent(armorPieceParent6);
        armorPiece7.transform.SetParent(armorPieceParent7);

        armorPiece1.transform.localPosition = armorPiecePos1;
        armorPiece2.transform.localPosition = armorPiecePos2;
        armorPiece3.transform.localPosition = armorPiecePos3;
        armorPiece4.transform.localPosition = armorPiecePos4;
        armorPiece5.transform.localPosition = armorPiecePos5;
        armorPiece6.transform.localPosition = armorPiecePos6;
        armorPiece7.transform.localPosition = armorPiecePos7;

        armorPiece1.transform.localRotation = armorPieceRotation1;
        armorPiece2.transform.localRotation = armorPieceRotation2;
        armorPiece3.transform.localRotation = armorPieceRotation3;
        armorPiece4.transform.localRotation = armorPieceRotation4;
        armorPiece5.transform.localRotation = armorPieceRotation5;
        armorPiece6.transform.localRotation = armorPieceRotation6;
        armorPiece7.transform.localRotation = armorPieceRotation7;
    }

    void PointOverDistance()
    {
        if (this.transform.position.x < startGrid)
        {
            extrapointsoverdistance = scoreEnemy + scoreEnemy / (Mathf.Abs(startPosition) - Mathf.Abs(startGrid)) * transform.position.x;
        }
        else
        {
            extrapointsoverdistance = scoreEnemy;
        }
    }

}
