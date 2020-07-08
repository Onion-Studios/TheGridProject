using UnityEngine;
using UnityEngine.UI;

public class LoadingIcon : MonoBehaviour
{
    public Slider bar;
    public float maxTime;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= 0 && currentTime < 5)
        {
            currentTime += 1 * Time.deltaTime;
            bar.value = currentTime / 5;
        }
        else
        {
            currentTime = 5;
            bar.enabled = false;
        }
    }
}
