using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrighteningDeath : MonoBehaviour
{
    public GameObject enemy;
    public GameObject hair;
    public float BlackToDeath = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PreDeath", 3);
    }

    void PreDeath()
    {
        ParticleSystemManager.Instance.ActivateParticle("FrighteningInkDeath");
        enemy.GetComponent<Renderer>().material.color = Color.black;
        hair.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    void Death()
    {
        enemy.SetActive(false);
        hair.SetActive(false);
    }
}
