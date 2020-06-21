using UnityEngine;

public class TEMPSTART : MonoBehaviour
{
    public static int forceStartIntensity;
    MainMenu MM;

    private void Start()
    {
        MM = FindObjectOfType<MainMenu>();
        forceStartIntensity = 0;
    }
    public void StartAtIntensity1()
    {
        forceStartIntensity = 1;
        startTheGame();

    }
    public void StartAtIntensity2()
    {
        forceStartIntensity = 2;
        startTheGame();
    }
    public void StartAtIntensity3()
    {
        forceStartIntensity = 3;
        startTheGame();
    }

    void startTheGame()
    {
        MM.GoToLevel();
        AudioManager.Instance.PlaySound("MenuConfirm");
        AudioManager.Instance.StopSound("MenuTheme");
    }
}
