using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public void ConfirmSound()
    {
        AudioManager.Instance.PlaySound("MenuConfirm");
    }

    public void CancelSound()
    {
        AudioManager.Instance.PlaySound("MenuCancel");
    }

    public void StopMenuMusic()
    {
        AudioManager.Instance.StopSound("MenuTheme");
    }
}
