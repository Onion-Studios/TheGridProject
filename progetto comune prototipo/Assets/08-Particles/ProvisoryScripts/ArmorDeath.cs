using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDeath : MonoBehaviour
{
    public GameObject enemy;
    public float BlackToDeath = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PreDeath", 3);
    }

    void PreDeath()
    {
        ParticleSystemManager.Instance.ActivateParticle("ArmoredInkDeath");
        enemy.GetComponent<Renderer>().material.color = Color.black;
        Invoke("Death", BlackToDeath);
    }
    void Death()
    {
        enemy.SetActive(false);
    }
}
