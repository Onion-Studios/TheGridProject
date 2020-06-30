using UnityEngine;
using UnityEngine.Playables;

public class Secret : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector]
    [Range(0, 100)] public float bar;
    public float charge;
    public float chargeLoss;
    Enemyspawnmanager enemyspawnmanager;
    public GameObject symbol;
    [HideInInspector]
    public float currentTime;
    public float timeMax;
    public bool active;
    [SerializeField]
    private ParticleSystem inkStroke;
    public ParticleSystem paintParticles;
    public PlayableDirector playableDirector;

    public GameObject bloodMoon;
    public Color defaultColorAlpha1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bar = 0;
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        bloodMoon.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        defaultColorAlpha1 = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        Timer();
    }

    void Timer()
    {
        if (bar > 100)
        {
            bar = 100;
        }

        //se la barra arriva a 100 
        if (bar == 100)
        {
            //attiva il timer la prima volta che arriva a 100
            if (active == false)
            {
                AudioManager.Instance.PlaySound("PaintingCompleted");
                paintParticles.Play();
                currentTime = timeMax;
                SymbolMovement();
            }
            else
            {
                //Timer
                if (currentTime > 0)
                {
                    currentTime -= 1 * Time.deltaTime;
                }
                //reset barra e si disattiva la secret 
                else
                {
                    currentTime = 0;
                    AudioManager.Instance.PlaySound("PaintingReset");
                    paintParticles.Stop();
                    bar = 0;

                    symbol.SetActive(false);
                    active = false;
                }
            }
        }
    }

    //Muoiono tutti i nemici presenti sulla lane 
    public void Death()
    {
        inkStroke.Play();
        AudioManager.Instance.PlaySound("Secretsignsuccessfull");
        for (int i = 0; i < 7; i++)
        {
            foreach (GameObject enemy in enemyspawnmanager.poolenemy[i])
            {
                if (enemy.activeInHierarchy == true)
                {
                    switch (i)
                    {
                        case 0:
                            NormalEnemy normalenemy = enemy.GetComponent<NormalEnemy>();
                            normalenemy.Deathforsign();
                            break;
                        case 1:
                            KamikazeEnemy kamikazenemy = enemy.GetComponent<KamikazeEnemy>();
                            kamikazenemy.Deathforsign();
                            break;
                        case 2:
                            ArmoredEnemy armoredenemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredenemy.armoredLife = 1;
                            armoredenemy.Deathforsign();
                            break;
                        case 3:
                            UndyingEnemy undyingenemy = enemy.GetComponent<UndyingEnemy>();
                            undyingenemy.Deathforsign();
                            break;
                        case 4:
                            MalevolentEnemy malevolentenemy = enemy.GetComponent<MalevolentEnemy>();
                            malevolentenemy.Deathforsign();
                            break;
                        case 5:
                            FrighteningEnemy frighteningenemy = enemy.GetComponent<FrighteningEnemy>();
                            frighteningenemy.Deathforsign();
                            break;
                        case 6:
                            BufferEnemy bufferenemy = enemy.GetComponent<BufferEnemy>();
                            bufferenemy.Deathforsign();
                            break;
                    }
                }
            }
        }
    }

    private void SymbolMovement()
    {
        playableDirector.Play();
        active = true;
    }

    void ChangeColor()
    {
        //this.renderer_.material.Lerp(startMaterial, endMaterial, 0f + bar / 100f);
        bloodMoon.GetComponent<Renderer>().material.color = Color.Lerp(bloodMoon.GetComponent<Renderer>().material.color, defaultColorAlpha1, 0f + bar / 100f);
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}
