using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrowdFeedbacks : MonoBehaviour
{
    [SerializeField]
    private Image movingCrowd;
    [SerializeField]
    private GameObject stillCrowd;
    [SerializeField]
    private Image joyEffect;
    [SerializeField]
    private Image frenzyEffect;
    [SerializeField]
    private GameObject disappointmentMistEffect;
    [SerializeField]
    private GameObject disappointmentEffect;

    public void FrenzyEffect(bool active)
    {
        frenzyEffect.enabled = active;
    }

    public IEnumerator DisappointmentEffect()
    {
        if (frenzyEffect.enabled)
        {
            yield break;
        }
        else if (joyEffect.enabled)
        {
            yield break;
        }
        ActivateDisappointmentEffect();
        yield return new WaitForSecondsRealtime(1.1f);
        DeactivateDisappointmentEffect();
    }

    private void ActivateDisappointmentEffect()
    {
        movingCrowd.enabled = false;
        stillCrowd.SetActive(true);
        disappointmentEffect.SetActive(true);
        disappointmentMistEffect.SetActive(true);
    }

    private void DeactivateDisappointmentEffect()
    {
        movingCrowd.enabled = true;
        stillCrowd.SetActive(false);
        disappointmentEffect.SetActive(false);
        disappointmentMistEffect.SetActive(false);
    }
    public IEnumerator JoyEffect()
    {
        if (frenzyEffect.enabled)
        {
            yield break;
        }
        joyEffect.enabled = true;
        yield return new WaitForSecondsRealtime(3);
        joyEffect.enabled = false;
    }

}
