using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokebomb : MonoBehaviour
{
    public float currentTime = 0f;
    public float maxTimeValue = 0f;
    public GameObject player;
    public ParticleSystem jetGeyser;

    private void Start()
    {
        currentTime = maxTimeValue;
        jetGeyser = GetComponent<ParticleSystem>();
        jetGeyser.Play();
    }
    private void Update()
    {
        if (currentTime <= maxTimeValue - 0.2)
        {
            jetGeyser.Stop();
        }
        if (currentTime <= maxTimeValue - 0.4)
        {
            player.SetActive(false);
        }
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        if (currentTime <= 0 )
        {
            jetGeyser.Play();
            currentTime = maxTimeValue;
            player.SetActive(true);
        }
    }
}
