using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text HighScore;
    public void Start()
    {
        HighScore.text = PersistantManagerScript.HighScore.ToString("D9");
    }

}
