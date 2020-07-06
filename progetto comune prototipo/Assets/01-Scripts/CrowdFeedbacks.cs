using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrowdFeedbacks : MonoBehaviour
{
    [SerializeField]
    private Image movingCrowd;
    [SerializeField]
    private Image stillCrowd;
    [SerializeField]
    private Image joyEffect;
    [SerializeField]
    private Image frenzyEffect;
    [SerializeField]
    private Image disappointmentMistEffect;
    [SerializeField]
    private Image disappointmentEffect;

    public void FrenzyEffect(bool active)
    {
        frenzyEffect.enabled = active;
    }

    public void DisappointmentEffect(bool active)
    {
        movingCrowd.enabled = !active;
        stillCrowd.enabled = active;
        disappointmentEffect.enabled = active;
        disappointmentMistEffect.enabled = active;
    }

    public IEnumerator JoyEffect()
    {
        if (frenzyEffect.enabled)
        {
            yield break;
        }
        joyEffect.enabled = true;
        yield return new WaitForSeconds(3);
        joyEffect.enabled = false;
    }

}
