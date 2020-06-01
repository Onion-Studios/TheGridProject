using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndSequence : MonoBehaviour
{
    int startSequencePosition;
    public GameObject[] light;
    public float activateLight;
    public bool starting, ending;
    

    // Start is called before the first frame update
    void Start()
    {
        startSequencePosition = 0;
        starting = true;
        ending = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartSequence();
    }


    void StartSequence()
    {
        switch (startSequencePosition)
        {
            case 0:

                break;
        }

    }

    IEnumerator TurnonLight()
    {
        yield return new WaitForSeconds(activateLight);
        light[0].SetActive(true);
        yield return new WaitForSeconds(activateLight);
        
    }

}
