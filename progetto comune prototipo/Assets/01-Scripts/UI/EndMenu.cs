using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.StopAllSounds();
        Cursor.visible = true;
    }

    public void GoToGameplay()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
        SceneManager.LoadScene(1);
    }

}
