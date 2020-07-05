using UnityEngine;

public class Playerbehaviour : MonoBehaviour
{
    #region VARIABILI 
    public GameObject character;
    public Vector3 playerposition;
    public float speed;
    public GameObject istanze;
    [HideInInspector]
    public Vector3 originray;
    Ray downraycheck;
    grigliamanager grigliamanager;
    Managercombo managercombo;
    Inkstone Inkstone;
    StartEndSequence startEndSequence;
    Enemyspawnmanager enemyspawnmanager;
    Yokaislayer YS;
    Secret SecretT;
    public IntensityReset intensityreset;
    public Vector3 LastCubeChecked;
    [HideInInspector]
    public int yokaislayercount;
    private string movementState;
    private float finalDestination;
    private float waitTimer;
    public float maxWaitTimer;
    public int inkGained;
    public int inkGainedIntensity1;
    public int inkGainedIntensity2;
    public int inkGainedIntensity3;
    [HideInInspector]
    public Vector3 gridCenter;
    private AudioManager audioManager;
    [HideInInspector]
    public ParticleSystem frightenedPlayer;
    [HideInInspector]
    public ParticleSystem smokeBomb;
    [HideInInspector]
    public ParticleSystem smokeBombCenter;
    private Vector3 confirmPosition;
    [HideInInspector]
    public Animator kitsuneAnimator;
    private int layerMask;
    #endregion

    // prendo le referenze che mi servono quando inizia il gioco
    void Start()
    {
        managercombo = FindObjectOfType<Managercombo>();
        if (managercombo == null)
        {
            Debug.LogError("managercombo è null!");
        }

        grigliamanager = FindObjectOfType<grigliamanager>();
        if (grigliamanager == null)
        {
            Debug.LogError("grigliamanager è null!");
        }

        Inkstone = FindObjectOfType<Inkstone>();
        if (Inkstone == null)
        {
            Debug.LogError("Inkstone is NULL!");
        }

        startEndSequence = FindObjectOfType<StartEndSequence>();
        if (startEndSequence == null)
        {
            Debug.LogError("StartEndSequence is NULL!");
        }

        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("EnemySpawnManager is NULL!");
        }

        SecretT = FindObjectOfType<Secret>();
        if (SecretT == null)
        {
            Debug.LogError("Secret is NULL!");
        }

        YS = this.gameObject.GetComponent<Yokaislayer>();

        movementState = "readystate";

        waitTimer = maxWaitTimer;
        audioManager = AudioManager.Instance;
        layerMask = 1 << 11;
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndSequence.starting == false && startEndSequence.ending == false && YS.active == false)
        {
            MovementHandler();
            if (movementState == "movingforward" || movementState == "movingback" || movementState == "movingleft" || movementState == "movingright")
            {
                kitsuneAnimator.SetBool("MovementKeyPressed", true);
            }

            if (movementState == "waitstate")
            {
                Invoke("DashRecoveryTime", 0.1f);
            }
        }
    }

    private void DashRecoveryTime()
    {
        kitsuneAnimator.SetBool("MovementKeyPressed", false);
    }

    void MovementHandler()
    {
        gridCenter = new Vector3(2f, istanze.transform.position.y, 2f);
        if (PauseMenu.GameIsPaused == false)
        {
            if (movementState == "readystate")
            {

                if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && istanze.transform.position.z > 0.9)
                {
                    audioManager.PlaySound("PlayerMovement");
                    finalDestination = istanze.transform.position.z - 1;
                    movementState = "movingforward";

                }
                if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && istanze.transform.position.z < 3.1)
                {
                    audioManager.PlaySound("PlayerMovement");
                    finalDestination = istanze.transform.position.z + 1;
                    movementState = "movingback";

                }
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && istanze.transform.position.x < 3.1)
                {
                    audioManager.PlaySound("PlayerMovement");
                    finalDestination = istanze.transform.position.x + 1;
                    movementState = "movingleft";

                }
                if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && istanze.transform.position.x > 0.9)
                {
                    audioManager.PlaySound("PlayerMovement");
                    finalDestination = istanze.transform.position.x - 1;
                    movementState = "movingright";

                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
                {
                    confirmPosition = istanze.transform.position;
                    smokeBomb.transform.position = confirmPosition;
                    smokeBomb.Play();
                    //smokeBomb.transform.SetParent(null);
                    smokeBombCenter.Play();
                    managercombo.CheckSign();
                    istanze.transform.rotation = Quaternion.Euler(0, 180, 0);
                    istanze.transform.position = gridCenter;
                    grigliamanager.ResetColorGrid();
                    grigliamanager.ResetGridLogic();
                    AudioManager.Instance.PlaySound("ConfirmSound");
                    //smokeBomb.transform.SetParent(istanze.transform);
                }
            }
            else if (movementState == "waitstate")
            {
                if (waitTimer > 0)
                {
                    waitTimer -= 1 * Time.deltaTime;
                }
                else
                {
                    waitTimer = 0;
                    waitTimer = maxWaitTimer;
                    movementState = "readystate";
                }
            }
            else if (movementState == "movingforward")
            {
                istanze.transform.rotation = Quaternion.Euler(0, 90, 0);

                if (istanze.transform.position.z > finalDestination)
                {
                    istanze.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanze.transform.position.z < finalDestination)
                    {
                        istanze.transform.position = new Vector3(istanze.transform.position.x, istanze.transform.position.y, finalDestination);
                    }
                    ColorGrid();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "waitstate";
                }
            }
            else if (movementState == "movingback")
            {
                istanze.transform.rotation = Quaternion.Euler(0, -90, 0);

                if (istanze.transform.position.z < finalDestination)
                {
                    istanze.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanze.transform.position.z > finalDestination)
                    {
                        istanze.transform.position = new Vector3(istanze.transform.position.x, istanze.transform.position.y, finalDestination);
                    }
                    ColorGrid();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "waitstate";
                }
            }
            else if (movementState == "movingleft")
            {
                istanze.transform.rotation = Quaternion.Euler(0, 0, 0);

                if (istanze.transform.position.x < finalDestination)
                {
                    istanze.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanze.transform.position.x > finalDestination)
                    {
                        istanze.transform.position = new Vector3(finalDestination, istanze.transform.position.y, istanze.transform.position.z);
                    }
                    ColorGrid();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "waitstate";
                }
            }
            else if (movementState == "movingright")
            {
                istanze.transform.rotation = Quaternion.Euler(0, 180, 0);


                if (istanze.transform.position.x > finalDestination)
                {
                    istanze.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanze.transform.position.x < finalDestination)
                    {
                        istanze.transform.position = new Vector3(finalDestination, istanze.transform.position.y, istanze.transform.position.z);
                    }
                    ColorGrid();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;

                    }

                    movementState = "waitstate";
                }


            }
        }

    }

    public void ReceiveDamage(int inkDamage, int maxInkDamage, bool isUndying)
    {
        if (maxInkDamage == 0)
        {
            Inkstone.Ink -= inkDamage;
        }
        else
        {
            Inkstone.maxInk -= maxInkDamage;
            if (Inkstone.Ink > Inkstone.maxInk)
            {
                Inkstone.Ink = Inkstone.maxInk;
            }
            Inkstone.Ink -= inkDamage;
            AudioManager.Instance.PlaySound("Playertakedamage");
            if (Inkstone.Ink > 0 && YS.active == false && isUndying == false)
            {
                intensityreset.intensityReset = true;
            }
            if (SecretT.bar == 100)
            {
                AudioManager.Instance.PlaySound("PlayerGetsHit");
                SecretT.paintParticles.Stop();
                SecretT.active = false;
                SecretT.currentTime = SecretT.timeMax;
                SecretT.symbol.SetActive(false);
            }
            else
            {
                // Insert THUD Sound
            }
            SecretT.bar -= SecretT.chargeLoss;
            if (SecretT.bar < 0)
            {
                SecretT.bar = 0;
            }
        }
    }

    #region SPAWN PLAYER
    // metodo spawn che istanzia il player prefab e lo pone in una variabile di riferimento
    public void Spawn()
    {
        //istanzio il player sopra al cubo sommando un vettore

        istanze = Instantiate(character, playerposition, Quaternion.Euler(0f, -90f, 0f), this.transform);
    }

    #endregion

    #region RAYCAST CHANGE COLOR GRID
    // metodo che dichiara il raggio e lo lancia in basso se trova qualcosa ci cambia il colore 
    void ColorGrid()
    {
        originray = (istanze.transform.position - new Vector3(0f, 0.5f, 0f));
        downraycheck = new Ray(originray, Vector3.down);
        float raydistance = 10f;
        if (Physics.Raycast(downraycheck, out RaycastHit hit, raydistance, layerMask))
        {
            hit.collider.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = grigliamanager.inksplash;

            if (grigliamanager.logicgrid[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.z] == false)
            {
                grigliamanager.logicgrid[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.z] = true;
            }
        }
    }

    #endregion

}
