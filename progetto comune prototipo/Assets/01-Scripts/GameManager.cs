using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    public Playerbehaviour ActualPlayer;
    public int GameIntensity = 1;
    Enemyspawnmanager Enemyspawnmanager;
    public int StartIntensity2 = 15;
    public int StartIntensity3 = 25;
    public PlayableDirector dragonTimeline;
    public GameObject dragon1;
    public GameObject dragon2;
    public GameObject dragon3;

    private bool soundPlayed1;
    private bool soundPlayed2;
    private bool soundPlayed3;

    private void Awake()
    {
        ActualPlayer = FindObjectOfType<Playerbehaviour>();
        Enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
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
        if (enemykilled >= 0 && enemykilled < StartIntensity2 && soundPlayed1 == false)
        {
            GameIntensity = 1;
            AudioManager.Instance.SetLoop("ClappingSound", false);
            AudioManager.Instance.PlaySound("ClappingSound");
            soundPlayed1 = true;
            soundPlayed2 = false;
            soundPlayed3 = false;

            dragon1.SetActive(true);
            dragon2.SetActive(false);
            dragon3.SetActive(false);
            dragonTimeline.Play();

        }
        else if (enemykilled >= StartIntensity2 && enemykilled < StartIntensity3 && soundPlayed2 == false)
        {
            GameIntensity = 2;

            AudioManager.Instance.SetLoop("ClappingSound", false);
            AudioManager.Instance.PlaySound("ClappingSound");
            soundPlayed2 = true;
            soundPlayed1 = false;
            soundPlayed3 = false;

            dragon1.SetActive(false);
            dragon2.SetActive(true);
            dragon3.SetActive(false);
            dragonTimeline.Play();
        }
        else if (enemykilled >= StartIntensity3 && soundPlayed3 == false)
        {
            GameIntensity = 3;

            AudioManager.Instance.SetLoop("ClappingSound", false);
            AudioManager.Instance.PlaySound("ClappingSound");
            soundPlayed3 = true;
            soundPlayed1 = false;
            soundPlayed2 = false;

            dragon1.SetActive(false);
            dragon2.SetActive(false);
            dragon3.SetActive(true);
            dragonTimeline.Play();
        }
    }
}
