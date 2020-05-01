using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public float score;
    public float scoreMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseOverTime();
    }

    void IncreaseOverTime()
    {
        score = Mathf.RoundToInt(score + scoreMultiplier * Time.deltaTime);
    }
}
