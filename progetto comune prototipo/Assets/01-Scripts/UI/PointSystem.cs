﻿using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [HideInInspector]
    public float score;
    public float[] scoreSeconds;
    [HideInInspector]
    public int scoreMultiplier;
    [HideInInspector]
    public float currentTimer;
    [SerializeField]
    float[] intensityFuseTimer;
    [HideInInspector]
    public float maxTimer;
    public int threshold1, threshold2, threshold3, threshold4;
    [HideInInspector]
    public int countercombo = 0;
    [HideInInspector]
    public float floatscore;
    StartEndSequence startEndSequence;
    public Image fuse;
    public ParticleSystem sparkle;
    private PlayableDirector sparkleDirector;
    GameManager gameManager;
    public WaveManager WM;
    private PauseMenuUI pauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
        startEndSequence = FindObjectOfType<StartEndSequence>();
        pauseMenuUI = FindObjectOfType<PauseMenuUI>();
        if (startEndSequence == null)
        {
            Debug.LogError("StartEndSequence is NULL!");
        }
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Game Manager is NULL!");
        }
        sparkleDirector = sparkle.GetComponent<PlayableDirector>();
        fuse.fillAmount = 0;
        floatscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndSequence.starting == false && startEndSequence.ending == false)
        {
            IncreaseOverTime();
        }
        Timer();
    }

    void IncreaseOverTime()
    {
        if (pauseMenuUI.IsGamePaused == false)
        {
            score += scoreSeconds[gameManager.GameIntensity - 1] / 60;
        }
    }

    void Timer()
    {
        if (WM.TEST_WaveActive == false)
        {
            maxTimer = intensityFuseTimer[gameManager.GameIntensity - 1];

        }
        else
        {
            maxTimer = intensityFuseTimer[WM.TEST_WaveIntensity - 1];
        }
        if (currentTimer == maxTimer)
        {
            sparkleDirector.Stop();
            sparkleDirector.Play();
            sparkle.Stop();
            sparkle.Play();
        }

        if (currentTimer > 0)
        {
            currentTimer -= 1 * Time.deltaTime;
            fuse.fillAmount = currentTimer / maxTimer;
        }
        else
        {
            countercombo = 0;
            Combo();
            sparkleDirector.Stop();
            sparkle.Stop();
        }
    }

    public void Combo()
    {
        if (countercombo >= 0 && countercombo < threshold1)
        {
            scoreMultiplier = 1;
        }
        if (countercombo >= threshold1 && countercombo < threshold2)
        {
            scoreMultiplier = 2;
        }
        if (countercombo >= threshold2 && countercombo < threshold3)
        {
            scoreMultiplier = 3;
        }
        if (countercombo >= threshold3 && countercombo < threshold4)
        {
            scoreMultiplier = 4;
        }
        if (countercombo >= threshold4)
        {
            scoreMultiplier = 5;
        }
    }
}
