using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeath : MonoBehaviour
{
    public GameObject enemy;
    public GameObject fascia;
    public float BlackToDeath = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PreDeath", 3);
    }

    void PreDeath()
    {
        ParticleSystemManager.Instance.ActivateParticle("NormalInkDeath");
        enemy.GetComponent<Renderer>().material.color = Color.black;
        fascia.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    void Death()
    {
        enemy.SetActive(false);
        fascia.SetActive(false);
    }
}
