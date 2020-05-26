using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NormalEnemy"))
        {
            other.GetComponent<NormalEnemy>().Deathforgriglia();
        }
        if (other.gameObject.CompareTag("KamikazeEnemy"))
        {
            other.GetComponent<KamikazeEnemy>().Deathforgriglia();
        }
        if (other.gameObject.CompareTag("ArmoredEnemy"))
        {
            other.GetComponent<ArmoredEnemy>().Deathforgriglia();
        }
        if (other.gameObject.CompareTag("FrighteningEnemy"))
        {
            other.GetComponent<FrighteningEnemy>().Deathforgriglia();
        }

    }
}
