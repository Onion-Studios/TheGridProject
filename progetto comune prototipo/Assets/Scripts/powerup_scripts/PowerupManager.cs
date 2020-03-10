using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    public int powerupspawnati;
    public GameObject powerup_prefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PowerupSpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PowerupSpawnCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {

            Vector3 posizionesugriglia = new Vector3(Random.Range(0, 5), 1,Random.Range(0, 5));
            Vector3 posizionecentrale = new Vector3(2, 1, 2);

            if (powerupspawnati < 2 && posizionesugriglia != posizionecentrale)
            {
                Instantiate(powerup_prefab, posizionesugriglia, Quaternion.identity);
                powerupspawnati += 1;
            }
            yield return new WaitForSeconds(3f);

        }
    }

   
}
