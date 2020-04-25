using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbehaviour : MonoBehaviour
{
    #region VARIABILI 
    public GameObject personaggio;
    [HideInInspector]
    public Vector3 posizionepersonaggio;
    public float speed;
    public GameObject istanza;
    [HideInInspector]
    public Vector3 origineraggio;
    Ray downraycheck;
    [SerializeField]
    Material colorecubonuovo;
    grigliamanager grigliamanager;
    Managercombo managercombo;
    PowerupManager powerupmanager;
    Inkstone Inkstone;
    public Vector3 LastCubeChecked;

    public int life;
    public bool reciveDamage;
    public int Gold;
    public int yokaislayercount;
    public string movementState;
    public float finalDestination;
    public float zAxis;
    public float xAxis;
    #endregion

    // prendo le referenze che mi servono quando inizia il gioco
    void Start()
    {
        powerupmanager = FindObjectOfType<PowerupManager>();
        if(powerupmanager == null)
        {
            Debug.LogError("powerupmanager è null!");
        }

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

        movementState = "readystate";
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = istanza.transform.position.x;
        zAxis = istanza.transform.position.z;
        //movimento ad ogni input corrisponde un metodo che fa l'azione di movimento corrispondente
        /* if (Input.GetKeyDown(KeyCode.D) && istanza.transform.position.x > 0.9)
         {
             miosimbolo.Add('d');
             forwardMove();
             Castraggio();
             if(Inkstone.Ink > 1)
             {
                 Inkstone.Ink -= 1;
             }
         }
         else if (Input.GetKeyDown(KeyCode.A) && istanza.transform.position.x < 3.1)
         {
             miosimbolo.Add('a');
             backmove();
             Castraggio();
             if (Inkstone.Ink > 1)
             {
                 Inkstone.Ink -= 1;
             }
         }
         else if (Input.GetKeyDown(KeyCode.S) && istanza.transform.position.z < 3.1)
         {
             miosimbolo.Add('s');
             rightmove();
             Castraggio();
             if (Inkstone.Ink > 1)
             {
                 Inkstone.Ink -= 1;
             }
         }
         else if (Input.GetKeyDown(KeyCode.W) && istanza.transform.position.z > 0.9)
         {
             miosimbolo.Add('w');
             leftmove();
             Castraggio();
             if (Inkstone.Ink > 1)
             {
                 Inkstone.Ink -= 1;
             }
         }*/

        MovementHandler();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            managercombo.Checksign();
            Gotocenter();
            grigliamanager.Resetcoloregriglia();
            grigliamanager.ResetGrigliaLogica();
        }
    }

        void MovementHandler()
        {
            if (movementState == "readystate")

            {
                if (Input.GetKeyDown(KeyCode.W) && istanza.transform.position.z > 0.9)
                {
                    finalDestination = istanza.transform.position.z - 1;
                    movementState = "movingforward";

                }
                if (Input.GetKeyDown(KeyCode.S) && istanza.transform.position.z < 3.1)
                {
                    finalDestination = istanza.transform.position.z + 1;
                    movementState = "movingback";

                }
                if (Input.GetKeyDown(KeyCode.A) && istanza.transform.position.x < 3.1)
                {
                    finalDestination = istanza.transform.position.x + 1;
                    movementState = "movingleft";

                }
                if (Input.GetKeyDown(KeyCode.D) && istanza.transform.position.x > 0.9)
                {
                    finalDestination = istanza.transform.position.x - 1;
                    movementState = "movingright";

                }
            }
            else if (movementState == "movingforward")
            {
                istanza.transform.rotation = Quaternion.Euler(0, 90, 0);

                if (istanza.transform.position.z > finalDestination)
                {
                    istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanza.transform.position.z < finalDestination)
                    {
                        istanza.transform.position = new Vector3(istanza.transform.position.x, istanza.transform.position.y, finalDestination);
                    }
                    Castraggio();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "readystate";
                }
            }
            else if (movementState == "movingback")
            {
                istanza.transform.rotation = Quaternion.Euler(0, -90, 0);

                if (istanza.transform.position.z < finalDestination)
                {
                    istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanza.transform.position.z > finalDestination)
                    {
                        istanza.transform.position = new Vector3(istanza.transform.position.x, istanza.transform.position.y, finalDestination);
                    }
                    Castraggio();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "readystate";
                }
            }
            else if (movementState == "movingleft")
            {
                istanza.transform.rotation = Quaternion.Euler(0, 0, 0);

                if (istanza.transform.position.x < finalDestination)
                {
                    istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanza.transform.position.x > finalDestination)
                    {
                        istanza.transform.position = new Vector3(finalDestination, istanza.transform.position.y, istanza.transform.position.z);
                    }
                    Castraggio();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;
                    }

                    movementState = "readystate";
                }

            }
            else if (movementState == "movingright")
            {
                istanza.transform.rotation = Quaternion.Euler(0, 180, 0);


                if (istanza.transform.position.x > finalDestination)
                {
                    istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    if (istanza.transform.position.x < finalDestination)
                    {
                        istanza.transform.position = new Vector3(finalDestination, istanza.transform.position.y, istanza.transform.position.z);
                    }
                    Castraggio();
                    if (Inkstone.Ink > 1)
                    {
                        Inkstone.Ink -= 1;

                    }

                    movementState = "readystate";
                }
            }
        }


        #region MOVEMENTS

        // il giocatore ruota e poi si muove nella direzione in cui ha ruotato

        void Gotocenter()
        {
            istanza.transform.rotation = Quaternion.Euler(0, 180, 0);
            istanza.transform.position = posizionepersonaggio;
        }

        #endregion


        #region SPAWN PLAYER
        // metodo spawn che istanzia il player prefab e lo pone in una variabile di riferimento
        public void Spawn()
        {
            //istanzio il player sopra al cubo sommando un vettore

            istanza = Instantiate(personaggio, posizionepersonaggio, Quaternion.Euler(0f, 180f, 0f), this.transform);
        }

        #endregion

        #region RAYCAST CAMBIO COLORE GRIGLIA
        // metodo che dichiara il raggio e lo lancia in basso se trova qualcosa ci cambia il colore 
        void Castraggio()
        {
            origineraggio = (istanza.transform.position - new Vector3(0f, 0.5f, 0f));
            downraycheck = new Ray(origineraggio, Vector3.down);
            float distanzaraggio = 10f;
            if (Physics.Raycast(downraycheck, out RaycastHit hit, distanzaraggio))
            {
                hit.collider.GetComponent<Renderer>().material = colorecubonuovo;

                Debug.Log("cubo x: " + hit.collider.gameObject.transform.position.x + " cubo y: " + hit.collider.gameObject.transform.position.z);
                if (grigliamanager.griglialogica[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.z] == false)
                {
                    grigliamanager.griglialogica[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.z] = true;
                }
            }
        }
    
    #endregion

}
