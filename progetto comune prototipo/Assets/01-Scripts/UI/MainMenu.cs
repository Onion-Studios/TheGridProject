using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text HighScore;
    public void Start()
    {
        HighScore.text = PersistantManagerScript.HighScore.ToString();
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(4);
        AudioManager.Instance.PlaySound("MenuConfirm");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        Debug.Log("Quit");
        Application.Quit();
    }

}
