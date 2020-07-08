using UnityEngine;
using UnityEngine.UI;

public class IntensityReset : MonoBehaviour
{
    #region Variables
    Enemyspawnmanager enemyspawnmanager;
    [SerializeField]
    Inkstone inkStone;
    Playerbehaviour PB;
    public bool intensityReset;
    int intensityResetIndex;
    public GameObject particleCamera;
    public Image blackPanel;
    public float blackPanelAlphaSpeed;
    Color blackPanelAlpha1;
    Color blackPanelAlpha0;
    [SerializeField]
    float maxTimer;
    float timer;
    [SerializeField]
    float maxWaitTimer;
    float waitTimer;
    bool inkSplashIsPlaying;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL");
        }
        PB = this.gameObject.GetComponent<Playerbehaviour>();
        blackPanelAlpha1 = new Color(0, 0, 0, 1);
        blackPanelAlpha0 = new Color(0, 0, 0, 0);
        timer = maxTimer;
        waitTimer = maxWaitTimer;
        inkSplashIsPlaying = false;
    }

    private void Update()
    {
        IntensityResetSequence();
    }

    void IntensityResetSequence()
    {
        if (intensityReset == true)
        {
            switch (intensityResetIndex)
            {
                case 0:
                    PreBlackScreen();
                    break;
                case 1:
                    FadeToBlack();
                    break;
                case 2:
                    DeactivateEnemies();
                    break;
                case 3:
                    FadeToTransparent();
                    break;
            }
        }
    }

    void PreBlackScreen()
    {
        StopEnemiesMovement();
        AudioManager.Instance.PlaySound("Resetgong");
        if (inkSplashIsPlaying == false)
        {
            inkStone.inkSplash.Play();
            inkSplashIsPlaying = true;
        }
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            inkSplashIsPlaying = false;
            waitTimer = maxWaitTimer;
            intensityResetIndex++;
        }
    }

    void StopEnemiesMovement()
    {
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
                                {
                                    NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                                    NormalEnemy.speed = 0;
                                    NormalEnemy.normalAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                            case 1:
                                {
                                    KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                    KamikazeEnemy.speed = 0;
                                    KamikazeEnemy.kamikazeAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                            case 2:
                                {
                                    ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                    ArmoredEnemy.speed = 0;
                                    ArmoredEnemy.armoredAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                            case 3:
                                {
                                    UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();
                                    UndiyngEnemy.speed = 0;
                                    UndiyngEnemy.undyingAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                            case 5:
                                {
                                    FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                                    frighteningEnemy.speed = 0;
                                    frighteningEnemy.frighteningAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                            case 6:
                                {
                                    BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                                    bufferEnemy.speed = 0;
                                    bufferEnemy.bufferAnimator.SetFloat("SpeedMultiplier", 0);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }

    void FadeToBlack()
    {
        if (blackPanel.color.a < 0.95)
        {
            blackPanel.color = Color.Lerp(blackPanel.color, blackPanelAlpha1, blackPanelAlphaSpeed * Time.deltaTime);
        }
        else
        {
            blackPanel.color = new Color(0, 0, 0, 1);
            particleCamera.SetActive(false);
            inkStone.inkSplash.Play();
            intensityResetIndex++;
        }
    }

    void DeactivateEnemies()
    {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            AudioManager.Instance.StopSound("Track02");
            AudioManager.Instance.StopSound("Track03");
            AudioManager.Instance.PlaySound("MainTrack");
            timer = 0;
            for (int i = 0; i < 7; i++)
            {
                foreach (GameObject nemici in enemyspawnmanager.poolenemy[i])
                {
                    if (nemici.activeInHierarchy == true)
                    {
                        switch (i)
                        {
                            case 0:
                                NormalEnemy normalenemy = nemici.GetComponent<NormalEnemy>();
                                normalenemy.TrueDeath();
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemici.GetComponent<KamikazeEnemy>();
                                kamikazenemy.TrueDeath();
                                break;
                            case 2:
                                ArmoredEnemy armoredenemy = nemici.GetComponent<ArmoredEnemy>();
                                armoredenemy.TrueDeath();
                                break;
                            case 3:
                                UndyingEnemy undyingenemy = nemici.GetComponent<UndyingEnemy>();
                                undyingenemy.TrueDeath();
                                break;
                            case 4:
                                MalevolentEnemy malevolentenemy = nemici.GetComponent<MalevolentEnemy>();
                                malevolentenemy.TrueDeath();
                                break;
                            case 5:
                                FrighteningEnemy frighteningenemy = nemici.GetComponent<FrighteningEnemy>();
                                frighteningenemy.TrueDeath();
                                break;
                            case 6:
                                BufferEnemy bufferenemy = nemici.GetComponent<BufferEnemy>();
                                bufferenemy.TrueDeath();
                                break;
                        }
                    }
                }
            }
            timer = maxTimer;
            PB.ResetToCenter();
            intensityResetIndex++;
        }
    }

    void FadeToTransparent()
    {
        if (blackPanel.color.a > 0.05)
        {
            blackPanel.color = Color.Lerp(blackPanel.color, blackPanelAlpha0, blackPanelAlphaSpeed * Time.deltaTime);
        }
        else
        {
            blackPanel.color = new Color(0, 0, 0, 0);
            particleCamera.SetActive(true);
            intensityResetIndex = 0;
            intensityReset = false;
            enemyspawnmanager.enemykilled = 0;
        }
    }
}
