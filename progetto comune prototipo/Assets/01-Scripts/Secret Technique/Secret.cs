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
    public float currentTime;
    public float timeMax;
    public bool active;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bar = 0;
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        renderer_ = GetComponent<Renderer>();
        renderer_.material = startMaterial;
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
                active = true;
                currentTime = timeMax;
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
                    bar = 0;
                    active = false;
                }
            }
        }
    }

    //Muoiono tutti i nemici presenti sulla lane 
    public void Death()
    {
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

    void Color()
    {
        this.renderer_.material.Lerp(startMaterial, endMaterial, 0f + bar / 100f);
    }
}