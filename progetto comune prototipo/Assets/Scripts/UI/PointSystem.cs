using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public float score;
    public float scoreSeconds;
    public int scoreMultiplier;
    public float currentTimer;
    public float maxTimer;
    public int threshold1, threshold2, threshold3, threshold4;
    public int countercombo = 0;
    public float floatscore;


    // Start is called before the first frame update
    void Start()
    {
        floatscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseOverTime();

        Timer();

      
    }

    void IncreaseOverTime()
    {

        score += scoreSeconds / 60;
    }

    void Timer()
    {

        if (currentTimer > 0)
        {
            currentTimer -= 1 * Time.deltaTime;
            
        }

        if(currentTimer < 0)
        {
            currentTimer = 0;

        }

        if(currentTimer == 0)
        {
            countercombo = 0;
        }

    }

    public void Combo()
    {
        if(countercombo >= 0 && countercombo < threshold1)
        {
            scoreMultiplier = 1;
        }
        if(countercombo >= threshold1 && countercombo < threshold2)
        {
            scoreMultiplier = 2;
        }
        if(countercombo >= threshold2 && countercombo < threshold3)
        {
            scoreMultiplier = 3;
        }
        if(countercombo >= threshold3 && countercombo < threshold4)
        {
            scoreMultiplier = 4; 
        }
        if(countercombo >= threshold4)
        {
            scoreMultiplier = 5;
        }
    }
}
