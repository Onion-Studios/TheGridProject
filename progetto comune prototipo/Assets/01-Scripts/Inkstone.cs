using UnityEngine;

public class Inkstone : MonoBehaviour
{
    public int maxInk;
    public int Ink;
    bool resetonending;
    public GameObject inkLevel;
    public Material[] inkMaterials;
    public static int FinalScore;
    [SerializeField]
    float inkStartLocalPos, inkEndLocalPos;
    float inkDistance;
    float inkOffset;
    int relativeInk;
    float relativeInkPosition;
    [SerializeField]
    float maxTimer;
    float timer;
    public ParticleSystem inkSplash;
    public StartEndSequence SES;
    public Playerbehaviour PB;
    void Start()
    {
        inkLevel.GetComponent<Renderer>().material = inkMaterials[0];
        resetonending = false;
        inkDistance = Mathf.Abs(Mathf.Abs(inkStartLocalPos) - Mathf.Abs(inkEndLocalPos));
        inkOffset = inkDistance / 100;
        relativeInk = Ink;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInk();
        UpdateInkPosition();
    }

    void UpdateInk()
    {
        if (Ink > maxInk)
        {
            Ink = maxInk;
        }
        if (maxInk < 0)
        {
            maxInk = 0;
        }
        if (Ink < 0)
        {
            Ink = 0;
        }
        if (Ink == 0)
        {
            PB.kitsuneAnimator.SetBool("MovementKeyPressed", false);
            PB.istanze.transform.rotation = Quaternion.Euler(0, -180, 0);
            SES.ending = true;
            resetonending = true;
        }
        if (resetonending == true)
        {
            Ink = 0;
        }
    }

    void UpdateInkPosition()
    {
        if (relativeInk != Ink)
        {
            if (relativeInk > Ink)
            {
                relativeInk--;
                relativeInkPosition = inkOffset * relativeInk + inkEndLocalPos;
                inkLevel.transform.localPosition = new Vector3(inkLevel.transform.localPosition.x, relativeInkPosition, inkLevel.transform.localPosition.z);
                TimerPlay();
            }
            else
            {
                relativeInk++;
                relativeInkPosition = inkOffset * relativeInk + inkEndLocalPos;
                inkLevel.transform.localPosition = new Vector3(inkLevel.transform.localPosition.x, relativeInkPosition, inkLevel.transform.localPosition.z);
                TimerPlay();
            }
            MaterialCases();
        }
    }

    void MaterialCases()
    {
        if (relativeInk >= 76 && relativeInk <= 100)
        {
            inkLevel.GetComponent<Renderer>().material = inkMaterials[0];
        }
        else if (relativeInk >= 51 && relativeInk <= 75)
        {
            inkLevel.GetComponent<Renderer>().material = inkMaterials[1];
        }
        else if (relativeInk >= 26 && relativeInk <= 50)
        {
            inkLevel.GetComponent<Renderer>().material = inkMaterials[2];
        }
        else if (relativeInk >= 2 && relativeInk <= 25)
        {
            inkLevel.GetComponent<Renderer>().material = inkMaterials[3];
        }
        else if (relativeInk == 1)
        {
            inkLevel.GetComponent<Renderer>().material = inkMaterials[4];
        }
    }

    void TimerPlay()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            timer = maxTimer;
        }
    }
}
