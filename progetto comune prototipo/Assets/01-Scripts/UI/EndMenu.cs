using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.StopAllSounds();
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

}
