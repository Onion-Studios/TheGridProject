using System.Collections;
using UnityEngine;

public class MalevolentEnemy : MonoBehaviour
{
    #region VARIABLES
    public int enemyID = 4;
    [SerializeField]
    public float speed;
    public int inkDamage;
    public int maxInkDamage;
    public int inkstoneDamage;
    Playerbehaviour playerbehaviour;
    Enemyspawnmanager enemyspawnmanager;
    Inkstone Inkstone;
    Secret SecretT;
    PointSystem pointsystem;
    public int scoreEnemy;
    public GameObject[] signmalevolentenemy;
    public Vector3 position;
    public float spawntimer;
    public float maxspawntimer;
    private Animator malevolentAnimator;
    private bool playerDeathPlayed;

    #endregion

    private void Awake()
    {
        malevolentAnimator = GetComponentInChildren<Animator>();
    }
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



    }

    private void OnEnable()
    {
        AudioManager.Instance.PlaySound("MalevolentSpawn");
        StartCoroutine("MalevolentSound");
        malevolentAnimator.SetBool("MalevolentDeath", false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
        if (Inkstone.Ink == 0 && playerDeathPlayed == false)
        {
            PlayerDeath();
            playerDeathPlayed = true;
        }
    }

    private IEnumerator MalevolentSound()
    {
        while (this.isActiveAndEnabled)
        {
            yield return new WaitForSeconds(3.50f);
            AudioManager.Instance.PlaySound("MalevolentSpawn");
        }
    }

    public void Deathforsign()
    {
        malevolentAnimator.SetBool("MalevolentDeath", true);
        Invoke("Death", 3);
    }

    private void Death()
    {
        this.gameObject.SetActive(false);
        enemyspawnmanager.enemykilled += 1;
        Inkstone.Ink += playerbehaviour.inkGained;
        SecretT.bar += SecretT.charge;
        pointsystem.currentTimer = pointsystem.maxTimer;
        pointsystem.countercombo++;

        pointsystem.Combo();

        pointsystem.score += scoreEnemy * pointsystem.scoreMultiplier;

    }

    private void PlayerDeath()
    {
        malevolentAnimator.SetBool("MalevolentDeath", true);
    }
}
