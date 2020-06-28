using UnityEngine;
using UnityEngine.UI;

public class IntensityReset : MonoBehaviour
{
    #region Variables
    StartEndSequence startendsequence;
    Enemyspawnmanager enemyspawnmanager;
    public bool intensityReset;
    int intensityResetIndex;
    public GameObject particleCamera;
    public Image blackPanel;
    [SerializeField]
    float blackPanelAlphaSpeed;
    Color blackPanelAlpha1;
    Color blackPanelAlpha0;
    public float pippo;
    [SerializeField]
    float maxTimer;
    float timer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startendsequence = FindObjectOfType<StartEndSequence>();
        if (startendsequence == null)
        {
            Debug.LogError("StartEndSequence is NULL");
        }
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL");
        }
        blackPanelAlpha1 = new Color(0, 0, 0, 1);
        blackPanelAlpha0 = new Color(0, 0, 0, 0);
        timer = maxTimer;
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
        startendsequence.StopEnemiesMovement();
        AudioManager.Instance.PlaySound("resetgong");
        intensityResetIndex++;
    }

    void FadeToBlack()
    {
        if (blackPanel.color.a < 0.95)
        {
            blackPanel.color = Color.Lerp(blackPanel.color, blackPanelAlpha1, blackPanelAlphaSpeed * Time.deltaTime);
            pippo = blackPanel.color.a;
        }
        else
        {
            blackPanel.color = new Color(0, 0, 0, 1);
            particleCamera.SetActive(false);
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
                                foreach (GameObject segno in normalenemy.SignNormalYokai)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in normalenemy.SignIntensity1Normal)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in normalenemy.SignIntensity1PlusNormal)
                                {
                                    segno.SetActive(false);
                                }
                                normalenemy.gameObject.SetActive(false);
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemici.GetComponent<KamikazeEnemy>();
                                foreach (GameObject segno in kamikazenemy.SignIntensity1Kamikaze)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in kamikazenemy.SignIntensity1PlusKamikaze)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in kamikazenemy.SignIntensity2Kamikaze)
                                {
                                    segno.SetActive(false);
                                }
                                kamikazenemy.gameObject.SetActive(false);
                                break;
                            case 2:
                                ArmoredEnemy armoredenemy = nemici.GetComponent<ArmoredEnemy>();
                                foreach (GameObject segno in armoredenemy.SignIntensity1Armored)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in armoredenemy.SignIntensity1PlusArmored)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in armoredenemy.SignIntensity2Armored)
                                {
                                    segno.SetActive(false);
                                }
                                armoredenemy.gameObject.SetActive(false);
                                break;
                            case 3:
                                UndyingEnemy undyingenemy = nemici.GetComponent<UndyingEnemy>();
                                foreach (GameObject segno in undyingenemy.SignIntensity3Undying)
                                {
                                    segno.SetActive(false);
                                }
                                undyingenemy.gameObject.SetActive(false);
                                break;
                            case 4:
                                MalevolentEnemy malevolentenemy = nemici.GetComponent<MalevolentEnemy>();
                                malevolentenemy.gameObject.SetActive(false);
                                break;
                            case 5:
                                FrighteningEnemy frighteningenemy = nemici.GetComponent<FrighteningEnemy>();
                                foreach (GameObject segno in frighteningenemy.SignIntensity2Frightening)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in frighteningenemy.SignIntensity2PlusFrightening)
                                {
                                    segno.SetActive(false);
                                }
                                frighteningenemy.gameObject.SetActive(false);
                                break;
                            case 6:
                                BufferEnemy bufferenemy = nemici.GetComponent<BufferEnemy>();
                                foreach (GameObject segno in bufferenemy.SignIntensity1Buffer)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in bufferenemy.SignIntensity1PlusBuffer)
                                {
                                    segno.SetActive(false);
                                }
                                foreach (GameObject segno in bufferenemy.SignIntensity2Buffer)
                                {
                                    segno.SetActive(false);
                                }
                                bufferenemy.gameObject.SetActive(false);
                                break;
                        }
                    }
                }
            }
            timer = maxTimer;
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
        }
    }
}
