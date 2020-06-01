using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndSequence : MonoBehaviour
{
    int startSequencePosition;
    public GameObject[] light;
    public float activateLight;
    public bool starting, ending, switchui;
    IEnumerator playerLight;
    public GameObject tenda, tenda2;
    public float curtainspeed;
    public Vector3 closecurtain;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    void Awake()
    {
        startSequencePosition = 0;
        starting = true;
        ending = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLight = null;
        switchui = true;
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
                if(playerLight == null)
                {
                    playerLight = PlayerLight();
                    StartCoroutine(playerLight);
                }
                break;
            case 1:
                if(playerLight != null)
                {
                    StopCoroutine(playerLight);
                    playerLight = null;
                }
                Bowing();
                break;
            case 2:
                SwitchUI();
                break;
            case 3:
                CloseCurtains();
                break;

        }

    }

    IEnumerator PlayerLight()
    {
        yield return new WaitForSeconds(activateLight);
        light[2].SetActive(true);
        startSequencePosition++;
        
    }

    void CloseCurtains()
    {
        if (tenda.transform.localPosition.x > closecurtain.x)
        {
            tenda.transform.Translate(Vector3.left * curtainspeed * Time.deltaTime);
            tenda2.transform.Translate(Vector3.right * curtainspeed * Time.deltaTime);

        }
        else
        {
            tenda.transform.position = closecurtain;
            startSequencePosition++;
        }
    }

    void SwitchUI()
    {
        switchui = !switchui;
        ink_text.gameObject.SetActive(switchui);
        score_text.gameObject.SetActive(switchui);
        counter_text.gameObject.SetActive(switchui);
        scoremultiplier_text.gameObject.SetActive(switchui);
        startSequencePosition++;
    }


    //TODO Animazione kitsune 
    void Bowing()
    {
        startSequencePosition++;
    }
}
