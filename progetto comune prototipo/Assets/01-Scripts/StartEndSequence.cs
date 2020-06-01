using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartEndSequence : MonoBehaviour
{
    Playerbehaviour playerbehaviour;
    GameManager GM;
    int startSequencePosition;
    public GameObject[] light;
    public float activateLight;
    public bool starting, ending, switchui;
    IEnumerator playerLight;
    IEnumerator lightsON;
    public float closedTime;
    public GameObject tenda, tenda2;
    public float curtainspeed;
    public Vector3 closecurtain;
    public Vector3 opencurtain1, opencurtain2;
    public Text ink_text, counter_text, score_text, scoremultiplier_text;
    public Vector3 centerGrid;
    bool soundNotLooping;

    void Awake()
    {
        startSequencePosition = 0;
        starting = true;
        ending = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("Playerbehaviour is NULL!");
        }

        GM = FindObjectOfType<GameManager>();
        if (GM == null)
        {
            Debug.LogError("GameMaster is NULL!");
        }

        playerLight = null;
        switchui = true;
        lightsON = null;

        opencurtain1 = tenda.transform.position;
        opencurtain2 = tenda2.transform.position;

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
                if (playerLight == null)
                {
                    playerLight = PlayerLight();
                    StartCoroutine(playerLight);
                }
                break;
            case 1:
                if (playerLight != null)
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
            case 4:
                if (lightsON == null)
                {
                    lightsON = LightsON();
                    StartCoroutine(lightsON);
                }
                break;
            case 5:
                if (lightsON != null)
                {
                    StopCoroutine(lightsON);
                    lightsON = null;
                }
                PlayerToCenter();
                break;
            case 6:
                OpenCurtains();
                break;
            case 7:
                SwitchUI();
                break;
            case 8:
                StartUP();
                break;

        }

    }

    IEnumerator PlayerLight()
    {
        yield return new WaitForSeconds(activateLight);
        light[2].SetActive(true);
        startSequencePosition++;

    }

    IEnumerator LightsON()
    {
        yield return new WaitForSeconds(closedTime);
        light[2].SetActive(false);
        light[0].SetActive(true);
        light[1].SetActive(true);
        startSequencePosition++;
    }

    void PlayerToCenter()
    {
        playerbehaviour.istanze.transform.position = centerGrid;
        playerbehaviour.istanze.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        startSequencePosition++;
    }

    void StartUP()
    {
        starting = false;

        if (soundNotLooping == false)
        {
            AudioManager.Instance.SetLoop("GongSound", false);
            AudioManager.Instance.PlaySound("GongSound");
            soundNotLooping = true;
        }

        GM.dragon1.SetActive(true);
        GM.dragon2.SetActive(false);
        GM.dragon3.SetActive(false);
        GM.dragonTimeline.Play();
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

    void OpenCurtains()
    {
        if (tenda.transform.localPosition.x < opencurtain1.x)
        {
            tenda.transform.Translate(Vector3.right * curtainspeed * Time.deltaTime);
            tenda2.transform.Translate(Vector3.left * curtainspeed * Time.deltaTime);
        }
        else
        {
            tenda.transform.position = opencurtain1;
            tenda2.transform.position = opencurtain2;
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
