using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerupbehaviour : MonoBehaviour
{
    PowerupManager powerupManager;

    // Start is called before the first frame update
    void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            powerupManager.powerupspawnati -= 1;
            Debug.Log("ho preso il pickup");
        }
    }
}
