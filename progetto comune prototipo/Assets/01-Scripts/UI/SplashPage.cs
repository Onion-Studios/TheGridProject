using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashPage : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
            AudioManager.Instance.PlaySound("MenuConfirm");
        }
    }
}
