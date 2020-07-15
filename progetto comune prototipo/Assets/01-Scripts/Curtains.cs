using UnityEngine;

public class Curtains : MonoBehaviour
{
    #region variables
    public GameObject curtainLeft, curtainRight;
    Vector3 openedCurtainLeft, openedCurtainRight;
    [SerializeField]
    Vector3 closedCurtainLeft, closedCurtainRight;
    private bool audioCurtainIsPlaying;
    [HideInInspector]
    public Animator leftCurtainAnimator;
    [HideInInspector]
    public Animator rightCurtainAnimator;
    public float openingDelay;

    #endregion

    private void Awake()
    {
        leftCurtainAnimator = curtainLeft.GetComponent<Animator>();
        rightCurtainAnimator = curtainRight.GetComponent<Animator>();
        openedCurtainLeft = curtainLeft.transform.localPosition;
        openedCurtainRight = curtainRight.transform.localPosition;
    }

    public int CloseCurtains(int counterIncrease, float curtainsSpeed)
    {
        if (curtainLeft.transform.localPosition.x > closedCurtainLeft.x)
        {
            if (audioCurtainIsPlaying == false)
            {
                AudioManager.Instance.PlaySound("Curtains");
                audioCurtainIsPlaying = true;
            }
            leftCurtainAnimator.SetBool("Closing", true);
            rightCurtainAnimator.SetBool("Closing", true);
            curtainLeft.transform.Translate(Vector3.right * curtainsSpeed * Time.deltaTime);
            curtainRight.transform.Translate(Vector3.left * curtainsSpeed * Time.deltaTime);
        }
        else
        {
            Invoke("OpeningDelay", openingDelay);
            leftCurtainAnimator.SetBool("Closing", false);

            AudioManager.Instance.StopSound("Curtains");
            audioCurtainIsPlaying = false;
            //curtainLeft.transform.localPosition = closedCurtainLeft;
            //curtainRight.transform.localPosition = closedCurtainRight;
            counterIncrease++;
        }
        return counterIncrease;
    }

    private void OpeningDelay()
    {
        rightCurtainAnimator.SetBool("Closing", false);
    }
    public int OpenCurtains(int counterIncrease, float curtainsSpeed)
    {
        if (curtainLeft.transform.localPosition.x < openedCurtainLeft.x)
        {
            if (audioCurtainIsPlaying == false)
            {
                AudioManager.Instance.PlaySound("Curtains");
                audioCurtainIsPlaying = true;
            }
            leftCurtainAnimator.SetBool("Opening", true);
            rightCurtainAnimator.SetBool("Opening", true);
            curtainLeft.transform.Translate(Vector3.left * curtainsSpeed * Time.deltaTime);
            curtainRight.transform.Translate(Vector3.right * curtainsSpeed * Time.deltaTime);
        }
        else
        {
            leftCurtainAnimator.SetBool("Opening", false);
            rightCurtainAnimator.SetBool("Opening", false);
            AudioManager.Instance.StopSound("Curtains");
            audioCurtainIsPlaying = false;
            curtainLeft.transform.localPosition = openedCurtainLeft;
            curtainRight.transform.localPosition = openedCurtainRight;
            counterIncrease++;
        }
        return counterIncrease;
    }

    public void CloseTeleport()
    {
        curtainLeft.transform.localPosition = closedCurtainLeft;
        curtainRight.transform.localPosition = closedCurtainRight;
    }
}
