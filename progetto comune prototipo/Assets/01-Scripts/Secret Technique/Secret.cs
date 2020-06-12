using System.Collections;
using UnityEngine;

public class Secret : MonoBehaviour
{
    #region VARIABLES
    [Range(0, 100)] public int bar;
    public int charge;
    public Material startMaterial;
    public Material endMaterial;
    Renderer renderer_;
    Enemyspawnmanager enemyspawnmanager;
    public GameObject symbol;
    Playerbehaviour playerbehaviour;
    public float currentTime;
    public float timeMax;
    public bool active;
    [SerializeField]
    float symbolShowDuration;
    public GameObject secretSymbol;
    IEnumerator symboldisplay;
    [SerializeField]
    private ParticleSystem inkStroke;
    public ParticleSystem paintParticles;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bar = 0;
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();

        renderer_ = this.transform.Find("Painting").gameObject.GetComponent<Renderer>();
        renderer_.material = startMaterial;
        active = false;

    }

    // Update is called once per frame
    void Update()
    {
        Color();
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
                if (symboldisplay != null)
                {
                    StopCoroutine(symboldisplay);
                    symboldisplay = null;
                }
                AudioManager.Instance.PlaySound("PaintingCompleted");
                paintParticles.Play();
                active = true;
                symbol.SetActive(active);
                currentTime = timeMax;
            }
            else
            {
                //Timer
                if (currentTime > 0)
                {
                    if (symboldisplay == null)
                    {
                        symboldisplay = SymbolDisplay();
                        StartCoroutine(symboldisplay);
                    }
                    currentTime -= 1 * Time.deltaTime;
                }
                //reset barra e si disattiva la secret 
                else
                {
                    currentTime = 0;
                    AudioManager.Instance.PlaySound("PaintingReset");
                    paintParticles.Stop();
                    bar = 0;

                    active = false;
                    symbol.SetActive(active);
                }
            }
        }
    }

    //Muoiono tutti i nemici presenti sulla lane 
    public void Death()
    {
        inkStroke.Play();
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

    private IEnumerator SymbolDisplay()
    {
        secretSymbol.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(symbolShowDuration);
        Time.timeScale = 1f;
        secretSymbol.SetActive(false);
    }


    void Color()
    {
        this.renderer_.material.Lerp(startMaterial, endMaterial, 0f + bar / 100f);
    }
}
