using UnityEngine;

public class Secret : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector]
    [Range(0, 100)] public float bar;
    public float charge;
    public float chargeLoss;
    public Material startMaterial;
    public Material endMaterial;
    Renderer renderer_;
    Enemyspawnmanager enemyspawnmanager;
    public GameObject symbol;
    [HideInInspector]
    public float currentTime;
    public float timeMax;
    public bool active;
    [SerializeField]
    float symbolTranslateSpeed;
    [SerializeField]
    Vector3 secretSymbolStartPosition;
    [SerializeField]
    Vector3 secretSymbolStartRotation;
    [SerializeField]
    Vector3 secretSymbolEndPosition;
    [SerializeField]
    Vector3 secretSymbolEndRotation;
    [SerializeField]
    private ParticleSystem inkStroke;
    public ParticleSystem paintParticles;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bar = 0;
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();

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
        var pos = symbol.transform.localPosition;
        var rot = symbol.transform.localRotation;
        symbol.transform.localPosition = secretSymbolStartPosition;
        symbol.transform.localRotation = Quaternion.Euler(secretSymbolStartRotation);
        symbol.SetActive(true);
        if (pos.x < secretSymbolEndPosition.x &&
            pos.y < secretSymbolEndPosition.y &&
            pos.z < secretSymbolEndPosition.z)
        {
            symbol.transform.Translate(secretSymbolEndPosition * symbolTranslateSpeed * Time.deltaTime);
            symbol.transform.Rotate(secretSymbolEndRotation * symbolTranslateSpeed * Time.deltaTime);
        }
        else
        {
            symbol.transform.localPosition = secretSymbolEndPosition;
            symbol.transform.localRotation = Quaternion.Euler(secretSymbolEndRotation);
            active = true;
        }
    }

    void Color()
    {
        this.renderer_.material.Lerp(startMaterial, endMaterial, 0f + bar / 100f);
    }
}
