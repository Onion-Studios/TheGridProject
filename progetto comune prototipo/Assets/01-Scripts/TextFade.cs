using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    Text thisText;
    [SerializeField]
    float offsetSpeed;
    float offset;
    public float alphaValue;
    bool isDecreasing;

    private void Start()
    {
        thisText = this.GetComponent<Text>();
        thisText.color = new Color(1, 1, 1, 1);
        isDecreasing = true;
        offset = offsetSpeed / 60;
    }
    void Update()
    {
        LerpColor();
    }

    void LerpColor()
    {
        alphaValue = thisText.color.a;
        Decrease();
        Increase();
    }

    void Decrease()
    {
        if (thisText.color.a > 0 && isDecreasing == true)
        {
            thisText.color = new Color(1, 1, 1, thisText.color.a - offset);
            if (thisText.color.a <= 0)
            {
                thisText.color = new Color(1, 1, 1, 0);
                isDecreasing = false;
            }
        }
    }

    void Increase()
    {
        if (thisText.color.a < 1 && isDecreasing == false)
        {
            thisText.color = new Color(1, 1, 1, thisText.color.a + offset);
            if (thisText.color.a >= 1)
            {
                thisText.color = new Color(1, 1, 1, 1);
                isDecreasing = true;
            }
        }
    }
}
