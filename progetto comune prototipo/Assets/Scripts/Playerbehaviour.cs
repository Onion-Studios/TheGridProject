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
    List<char> miosimbolo = new List<char>();
    Managercombo managercombo;
    PowerupManager powerupmanager;
    Inkstone Inkstone;
    
    public int life;
    public bool reciveDamage;
    public int Gold;
    public int yokaislayercount;
    public string movementState;
    public float finalDestination;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            managercombo.checkcombo(miosimbolo);
            Gotocenter();
            grigliamanager.Resetcoloregriglia();
            miosimbolo.Clear();
        }

        MovementHandler();
    }

    void MovementHandler()
    {
       

        if(movementState == "readystate")
        {
            if (Input.GetKeyDown(KeyCode.W) && istanza.transform.position.z > 0.9)
            {
                finalDestination = istanza.transform.position.z + 1;
                movementState = "movingforward";
                miosimbolo.Add('w');
            }
            if (Input.GetKeyDown(KeyCode.S) && istanza.transform.position.z < 3.1)
            {
                finalDestination = istanza.transform.position.z - 1;
                movementState = "movingback";
                miosimbolo.Add('s');
            }
            if (Input.GetKeyDown(KeyCode.A) && istanza.transform.position.x < 3.1)
            {
                finalDestination = istanza.transform.position.x + 1;
                movementState = "movingleft";
                miosimbolo.Add('a');
            }
            if (Input.GetKeyDown(KeyCode.D) && istanza.transform.position.x > 0.9)
            {
                finalDestination = istanza.transform.position.x - 1;
                movementState = "movingright";
                miosimbolo.Add('d');
            }
        }
        else if (movementState  == "movingforward")
        {
            istanza.transform.rotation = Quaternion.Euler(0, 90, 0);

            if(istanza.transform.position.z < finalDestination)
            {
                istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
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

            if (istanza.transform.position.z > finalDestination)
            {
                istanza.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
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
        origineraggio = (istanza.transform.position - new Vector3(0f, 0.5f,0f));
        downraycheck = new Ray(origineraggio, Vector3.down);
        float distanzaraggio = 10f;
        if(Physics.Raycast(downraycheck, out RaycastHit hit, distanzaraggio))
        {
            if (hit.collider.GetComponent<cubeprefabehaviour>().iscoloured == true)
            {
                hit.collider.GetComponent<Renderer>().material = grigliamanager.colorebasegriglia;
                hit.collider.GetComponent<cubeprefabehaviour>().iscoloured = false;
            }
            else
            {
                hit.collider.GetComponent<Renderer>().material = colorecubonuovo;
                hit.collider.GetComponent<cubeprefabehaviour>().iscoloured = true;
            }
        }
    }
    #endregion

}
