using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSlash : MonoBehaviour
{
    public float speed = 0f;
    public float destroyDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0);
        Destroy(gameObject, destroyDelay);
    }
}
