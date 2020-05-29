using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferDeath : MonoBehaviour
{
    public GameObject enemy;
    public GameObject hat;
    public float BlackToDeath = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PreDeath", 3);
    }

    void PreDeath()
    {
        ParticleSystemManager.Instance.ActivateParticle("BufferInkDeath");
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hat.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    void Death()
    {
        enemy.SetActive(false);
        hat.SetActive(false);
    }
}
