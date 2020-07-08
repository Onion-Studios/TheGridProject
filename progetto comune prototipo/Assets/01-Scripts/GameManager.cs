using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    public Playerbehaviour ActualPlayer;
    public WaveManager WaveManager;
    public CrowdFeedbacks crowdFeedbacks;
    public int GameIntensity = 1;
    Enemyspawnmanager Enemyspawnmanager;
    public int StartIntensity2 = 15;
    public int StartIntensity3 = 25;
    [HideInInspector]
    public PlayableDirector dragonTimeline;
    [HideInInspector]
    public GameObject dragon1;
    [HideInInspector]
    public GameObject dragon2;
    [HideInInspector]
    public GameObject dragon3;
    [HideInInspector]
    public GameObject loadImage;
    public float[] intensitySpeed;
    [HideInInspector]
    public float intensitySpeedIncrease;

    private bool soundPlayed1;
    private bool soundPlayed2;
    private bool soundPlayed3;
    private bool firstGameStart;

    public IEnumerator joyEffectCO;


    private void Awake()
    {
        ActualPlayer = FindObjectOfType<Playerbehaviour>();
        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        WaveManager = FindObjectOfType<WaveManager>();
        firstGameStart = true;
        if (WaveManager.TEST_WaveActive == false)
        {
            ActualPlayer.inkGained = ActualPlayer.inkGainedIntensity[0];
        }
        else
        {
            ActualPlayer.inkGained = ActualPlayer.inkGainedIntensity[WaveManager.TEST_WaveIntensity - 1];
        }
        Cursor.visible = false;
    }

    private void Start()
    {
        loadImage.SetActive(true);
    }

    private void Update()
    {
        ChangeIntensity(Enemyspawnmanager.enemykilled);
    }


    public void GoToGameplay()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToEndMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMenuScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToSplashStart()
    {
        SceneManager.LoadScene(0);
    }

    public Playerbehaviour GetPlayerBehaviur()
    {
        return FindObjectOfType<Playerbehaviour>();
    }

    void ChangeIntensity(int enemykilled)
    {
        if (WaveManager.TEST_WaveActive == false)
        {
            if (enemykilled >= 0 && enemykilled < StartIntensity2 && soundPlayed1 == false)
            {
                GameIntensity = 1;
                if (firstGameStart)
                {
                    firstGameStart = false;
                }
                else
                {
                    dragon1.SetActive(true);
                    dragon2.SetActive(false);
                    dragon3.SetActive(false);
                    dragonTimeline.Play();
                    AudioManager.Instance.SetLoop("BooSound", false);
                    AudioManager.Instance.PlaySound("BooSound");
                    ActualPlayer.inkGained = ActualPlayer.inkGainedIntensity[0];
                }

                soundPlayed1 = true;
                soundPlayed2 = false;
                soundPlayed3 = false;

            }
            else if (enemykilled >= StartIntensity2 && enemykilled < StartIntensity3 && soundPlayed2 == false)
            {
                GameIntensity = 2;
                AudioManager.Instance.StopSound("MainTrack");
                AudioManager.Instance.PlaySound("Track02");

                //AudioManager.Instance.SetLoop("ClappingSound", false);
                //AudioManager.Instance.PlaySound("ClappingSound");
                if (joyEffectCO != null)
                {
                    StopCoroutine(joyEffectCO);
                    joyEffectCO = null;
                }
                joyEffectCO = crowdFeedbacks.JoyEffect();
                StartCoroutine(joyEffectCO);

                soundPlayed2 = true;
                soundPlayed1 = false;
                soundPlayed3 = false;

                dragon1.SetActive(false);
                dragon2.SetActive(true);
                dragon3.SetActive(false);
                dragonTimeline.Play();
                ActualPlayer.inkGained = ActualPlayer.inkGainedIntensity[1];
            }
            else if (enemykilled >= StartIntensity3 && soundPlayed3 == false)
            {
                GameIntensity = 3;
                AudioManager.Instance.StopSound("Track02");
                AudioManager.Instance.PlaySound("Track03");

                //AudioManager.Instance.SetLoop("ClappingSound", false);
                //AudioManager.Instance.PlaySound("ClappingSound");

                if (joyEffectCO != null)
                {
                    StopCoroutine(joyEffectCO);
                    joyEffectCO = null;
                }
                joyEffectCO = crowdFeedbacks.JoyEffect();
                StartCoroutine(joyEffectCO);

                soundPlayed3 = true;
                soundPlayed1 = false;
                soundPlayed2 = false;

                dragon1.SetActive(false);
                dragon2.SetActive(false);
                dragon3.SetActive(true);
                dragonTimeline.Play();
                ActualPlayer.inkGained = ActualPlayer.inkGainedIntensity[2];
            }
        }
        else if (WaveManager.TEST_WaveActive == true)
        {
            GameIntensity = WaveManager.TEST_WaveIntensity;
        }
        intensitySpeedIncrease = intensitySpeed[GameIntensity - 1];
    }

}
