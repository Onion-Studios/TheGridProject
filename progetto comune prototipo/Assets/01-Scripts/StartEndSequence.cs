using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartEndSequence : MonoBehaviour
{
    #region VARIABLES
    Playerbehaviour playerbehaviour;
    GameManager GM;
    PointSystem pointSystem;
    Enemyspawnmanager enemyspawnmanager;
    Curtains curtains;
    [HideInInspector]
    public int startSequencePosition;
    [HideInInspector]
    int endSequencePosition;
    public GameObject[] lightObjects;
    public float activateLight;
    public bool starting, ending, skipping;
    IEnumerator lightsON;
    IEnumerator lightsOFF;
    IEnumerator blackScreen;
    IEnumerator loading;
    public float lightsStopTime;
    public float closedTime;
    public GameObject tenda, tenda2;
    public float curtainspeed;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    public Vector3 centerGrid;
    bool soundNotLooping;
    public int enemynumber;
    public GameObject endImage;
    public float time;
    public Vector3 closeCurtain2;
    public float LoadingTime;
    public GameObject LoadImage;
    private bool startSequencePositionPlayed;
    private bool endSequencePositionPlayed;
    [SerializeField]
    private GameObject particlesCamera;
    [SerializeField]
    private GameObject mainCamera;
    private Vector3 finalPosition;
    [SerializeField]
    private Image crowd;
    private Color alpha1Crowd;
    [SerializeField]
    private GameObject skipButton;
    float timer;
    float timePlayed;
    public static int secondsPlayed, minutesPlayed, hoursPlayed;
    [SerializeField]
    private Text randomCuriosity;
    [SerializeField]
    private string[] curiosityArray;
    #endregion

    void Awake()
    {
        crowd.color = new Color(1, 1, 1, 0);
        alpha1Crowd = new Color(1, 1, 1, 1);
        startSequencePosition = 0;
        endSequencePosition = 0;
        starting = true;
        ending = false;
        skipping = false;
        timePlayed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("Playerbehaviour is NULL!");
        }

        GM = FindObjectOfType<GameManager>();
        if (GM == null)
        {
            Debug.LogError("GameMaster is NULL!");
        }

        pointSystem = FindObjectOfType<PointSystem>();
        if (pointSystem == null)
        {
            Debug.LogError("PointSystem is NULL!");
        }

        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL!");
        }

        curtains = FindObjectOfType<Curtains>();
        if (curtains == null)
        {
            Debug.LogError("Curtains is NULL!");
        }
        lightsON = null;
        loading = null;

        int randomCuriosityNumber = Random.Range(0, curiosityArray.Length - 1);
        randomCuriosity.text = curiosityArray[randomCuriosityNumber].ToUpper();
    }

    void Update()
    {
        StartSequence();
        if (skipping == false && startSequencePosition <= 3 && starting == true && Input.GetKeyDown(KeyCode.Return) && startSequencePosition >= 1)
        {
            skipping = true;
        }
        Skip();
        EndSequence();
        CountTimePlayed();
    }

    void CountTimePlayed()
    {
        int seconds;
        int minutes;
        int hours;
        if (starting == false && ending == false)
        {
            timePlayed += Time.deltaTime;
        }
        else if (starting == false && ending == true)
        {
            seconds = (int)timePlayed;
            if (seconds >= 60)
            {
                minutes = seconds / 60;
                for (int i = 0; i < minutes; i++)
                {
                    seconds -= 60;
                }
                if (minutes >= 60)
                {
                    hours = minutes / 60;
                    for (int i = 0; i < hours; i++)
                    {
                        minutes -= 60;
                    }
                    secondsPlayed = seconds;
                    minutesPlayed = minutes;
                    hoursPlayed = hours;
                }
                else
                {
                    secondsPlayed = seconds;
                    minutesPlayed = minutes;
                    hoursPlayed = 0;
                }
            }
            else
            {
                secondsPlayed = seconds;
                minutesPlayed = 0;
                hoursPlayed = 0;
            }
        }
    }

    void StartSequence()
    {
        if (starting == true)
        {
            switch (startSequencePosition)
            {
                case 0:
                    if (loading == null)
                    {
                        loading = Loading();
                        StartCoroutine(loading);
                    }
                    break;
                case 1:
                    if (loading != null)
                    {
                        StopCoroutine(loading);
                        loading = null;
                    }
                    PlayerLight();
                    break;
                case 2:
                    Bowing();
                    break;
                case 3:
                    ReduceCameraZoom();
                    startSequencePosition = curtains.CloseCurtains(startSequencePosition, curtainspeed);
                    break;
                case 4:
                    if (lightsON == null)
                    {
                        lightsON = LightsON();
                        StartCoroutine(lightsON);
                    }
                    crowd.color = Color.Lerp(crowd.color, alpha1Crowd, 3 * Time.deltaTime);
                    break;
                case 5:
                    if (lightsON != null)
                    {
                        StopCoroutine(lightsON);
                        lightsON = null;
                    }
                    PlayerToCenter();
                    break;
                case 6:
                    startSequencePosition = curtains.OpenCurtains(startSequencePosition, curtainspeed);
                    break;
                case 7:
                    StartUP();
                    break;
            }
        }
    }
    #region Start Sequence Functions
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(LoadingTime);
        LoadImage.SetActive(false);
        skipButton.SetActive(true);
        AudioManager.Instance.PlaySound("Yoo");
        AudioManager.Instance.PlaySound("MainTrack");
        startSequencePosition++;
        timer = activateLight;
    }
    void PlayerLight()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            lightObjects[2].SetActive(true);
            if (skipping == false)
            {
                AudioManager.Instance.PlaySound("Spotlight");
            }
            startSequencePosition++;
        }
    }
    void Bowing()
    {
        if (startSequencePositionPlayed == false)
        {
            Invoke("StartSequencePosition", 3);
            startSequencePositionPlayed = true;
        }
        playerbehaviour.kitsuneAnimator.SetBool("Bowing", true);
    }
    void ReduceCameraZoom()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, particlesCamera.transform.position, 3 * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, particlesCamera.transform.rotation, 3 * Time.deltaTime);
        skipButton.SetActive(false);
    }
    IEnumerator LightsON()
    {
        yield return new WaitForSeconds(closedTime);
        lightObjects[2].SetActive(false);
        lightObjects[0].SetActive(true);
        lightObjects[1].SetActive(true);
        startSequencePosition++;
    }
    void PlayerToCenter()
    {
        playerbehaviour.istanze.transform.position = centerGrid;
        playerbehaviour.istanze.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        startSequencePosition++;
    }
    void StartUP()
    {
        starting = false;

        if (soundNotLooping == false)
        {
            AudioManager.Instance.SetLoop("GongSound", false);
            AudioManager.Instance.PlaySound("GongSound");
            soundNotLooping = true;
        }

        GM.dragon1.SetActive(true);
        GM.dragon2.SetActive(false);
        GM.dragon3.SetActive(false);
        GM.dragonTimeline.Play();
        startSequencePosition++;

    }
    void StartSequencePosition()
    {

        playerbehaviour.kitsuneAnimator.SetBool("Bowing", false);
        startSequencePosition++;
    }
    #endregion

    public void EndSequence()
    {
        if (ending == true)
        {
            switch (endSequencePosition)
            {
                case 0:
                    crowd.enabled = false;
                    StopAllEnemies();
                    break;
                case 1:
                    if (lightsOFF == null)
                    {
                        lightsOFF = LightsOFF();
                        StartCoroutine(lightsOFF);
                    }
                    finalPosition = new Vector3(playerbehaviour.istanze.transform.position.x, playerbehaviour.istanze.transform.position.y + 3, playerbehaviour.istanze.transform.position.z + 5);
                    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, finalPosition, 3 * Time.deltaTime);
                    break;
                case 2:
                    if (lightsOFF != null)
                    {
                        StopCoroutine(lightsOFF);
                        lightsOFF = null;
                    }
                    Seppuku();
                    AudioManager.Instance.PlaySound("Seppuku");
                    break;
                case 3:
                    endSequencePosition = curtains.CloseCurtains(endSequencePosition, curtainspeed);
                    break;
                case 4:
                    if (blackScreen == null)
                    {
                        blackScreen = BlackScreen();
                        StartCoroutine(blackScreen);
                    }
                    break;
                case 5:
                    if (blackScreen != null)
                    {
                        StopCoroutine(blackScreen);
                        blackScreen = null;
                    }
                    GameOver();
                    break;
            }
        }
    }

    #region End Sequence Functions
    void StopAllEnemies()
    {
        ending = true;
        StopEnemiesMovement();
        endSequencePosition++;
    }
    public void StopEnemiesMovement()
    {
        for (int i = 0; i < enemynumber; i++)
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
                                NormalEnemy.gameObject.SetActive(false);
                            }
                            break;
                        case 1:
                            {
                                KamikazeEnemy KamikazeEnemy = enemy.GetComponent<KamikazeEnemy>();
                                KamikazeEnemy.speed = 0;
                                KamikazeEnemy.kamikazeAnimator.SetFloat("SpeedMultiplier", 0);
                                KamikazeEnemy.gameObject.SetActive(false);
                            }
                            break;
                        case 2:
                            {
                                ArmoredEnemy ArmoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                ArmoredEnemy.speed = 0;
                                ArmoredEnemy.armoredAnimator.SetFloat("SpeedMultiplier", 0);
                                ArmoredEnemy.gameObject.SetActive(false);
                                ArmoredEnemy.armorPiece1.SetActive(false);
                                ArmoredEnemy.armorPiece2.SetActive(false);
                                ArmoredEnemy.armorPiece3.SetActive(false);
                                ArmoredEnemy.armorPiece4.SetActive(false);
                                ArmoredEnemy.armorPiece5.SetActive(false);
                                ArmoredEnemy.armorPiece6.SetActive(false);
                                ArmoredEnemy.armorPiece7.SetActive(false);

                            }
                            break;
                        case 3:
                            {
                                UndyingEnemy UndiyngEnemy = enemy.GetComponent<UndyingEnemy>();
                                UndiyngEnemy.speed = 0;
                                UndiyngEnemy.undyingAnimator.SetFloat("SpeedMultiplier", 0);
                                UndiyngEnemy.gameObject.SetActive(false);
                            }
                            break;
                        case 5:
                            {
                                FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                                frighteningEnemy.speed = 0;
                                frighteningEnemy.frighteningAnimator.SetFloat("SpeedMultiplier", 0);
                                frighteningEnemy.gameObject.SetActive(false);
                            }
                            break;
                        case 6:
                            {
                                BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                                bufferEnemy.speed = 0;
                                bufferEnemy.bufferAnimator.SetFloat("SpeedMultiplier", 0);
                                bufferEnemy.gameObject.SetActive(false);
                            }
                            break;
                    }
                }
            }
        }
    }
    IEnumerator LightsOFF()
    {
        //yield return new WaitForSeconds(lightsStopTime);
        lightObjects[1].SetActive(false);
        lightObjects[2].SetActive(false);
        //yield return new WaitForSeconds(lightsStopTime);
        lightObjects[0].SetActive(false);
        particlesCamera.SetActive(false);
        //yield return new WaitForSeconds(lightsStopTime);
        lightObjects[3].transform.position = new Vector3(playerbehaviour.istanze.transform.position.x, lightObjects[3].transform.position.y, playerbehaviour.istanze.transform.position.z);
        lightObjects[3].SetActive(true);
        AudioManager.Instance.PlaySound("Spotlight");
        yield return new WaitForSeconds(lightsStopTime);
        endSequencePosition++;
    }
    void Seppuku()
    {
        if (endSequencePositionPlayed == false)
        {
            Invoke("EndSequencePosition", 5);
            endSequencePositionPlayed = true;
        }
        AudioManager.Instance.StopAllSounds();
        playerbehaviour.kitsuneAnimator.SetBool("Dead", true);


    }
    IEnumerator BlackScreen()
    {
        yield return new WaitForSeconds(time);
        endImage.SetActive(true);
        yield return new WaitForSeconds(time);
        endSequencePosition++;
    }
    void GameOver()
    {
        Inkstone.FinalScore = (int)pointSystem.score;
        SceneManager.LoadScene(3);
    }
    void EndSequencePosition()
    {
        endSequencePosition++;
    }
    #endregion
    void Skip()
    {
        if (skipping == true)
        {
            AudioManager.Instance.StopSound("Yoo");
            curtains.CloseTeleport();

            lightObjects[2].SetActive(false);
            lightObjects[0].SetActive(true);
            lightObjects[1].SetActive(true);

            mainCamera.transform.SetPositionAndRotation(particlesCamera.transform.position, particlesCamera.transform.rotation);
            crowd.color = alpha1Crowd;

            startSequencePosition = 3;
            skipping = false;
        }
    }
}
