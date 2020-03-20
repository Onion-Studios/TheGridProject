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
    public GameManager GM;
    public int Life;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        powerupmanager = FindObjectOfType<PowerupManager>();
        managercombo = FindObjectOfType<Managercombo>();
        grigliamanager = FindObjectOfType<grigliamanager>();
    }

    // Update is called once per frame
    void Update()
    {
        //movimento ad ogni input corrisponde un metodo che fa l'azione di movimento corrispondente
        if (Input.GetKeyDown(KeyCode.D) && istanza.transform.position.x > 0.9)
        {
            miosimbolo.Add('f');
            forwardMove();
            Castraggio();
        }
        else if (Input.GetKeyDown(KeyCode.A) && istanza.transform.position.x < 3.1)
        {
            miosimbolo.Add('d');
            backmove();
            Castraggio();
        }
        else if (Input.GetKeyDown(KeyCode.S) && istanza.transform.position.z < 3.1)
        {
            miosimbolo.Add('r');
            rightmove();
            Castraggio();
        }
        else if (Input.GetKeyDown(KeyCode.W) && istanza.transform.position.z > 0.9)
        {
            miosimbolo.Add('l');
            leftmove();
            Castraggio();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            managercombo.checkcombo(miosimbolo);
            Gotocenter();
            grigliamanager.Resetcoloregriglia();
            miosimbolo.Clear();
        }
    }

    #region MOVEMENTS
    
    // il giocatore ruota e poi si muove nella direzione in cui ha ruotato

    void forwardMove()
    {
        istanza.transform.rotation = Quaternion.Euler(0, 180, 0);
        istanza.transform.Translate(Vector3.right);   
    }
    
    void backmove()
    {
        istanza.transform.rotation = Quaternion.Euler(0, 0, 0);
        istanza.transform.Translate(Vector3.right);
    }

    void rightmove()
    {
        istanza.transform.rotation = Quaternion.Euler(0, -90, 0);
        istanza.transform.Translate(Vector3.right);
    }

    void leftmove()
    {
        istanza.transform.rotation = Quaternion.Euler(0, 90, 0);
        istanza.transform.Translate(Vector3.right);
    }

    void Gotocenter()
    {
        istanza.transform.rotation = Quaternion.Euler(0, 180, 0);
        istanza.transform.position = posizionepersonaggio;
    }

#endregion
 

    // metodo spawn che istanzia il player prefab e lo pone in una variabile di riferimento
    public void Spawn()
    {
        //istanzio il player sopra al cubo sommando un vettore
        
        istanza = Instantiate(personaggio, posizionepersonaggio, Quaternion.Euler(0f, 180f, 0f), this.transform);
    }
    
    // metodo che dichiara il raggio e lo lancia in basso se trova qualcosa ci cambia il colore 
    void Castraggio()
    {
        origineraggio = (istanza.transform.position - new Vector3(0f, 0.5f,0f));
        downraycheck = new Ray(origineraggio, Vector3.down);
        float distanzaraggio = 10f;
        if(Physics.Raycast(downraycheck, out RaycastHit hit, distanzaraggio))
        {
            hit.collider.GetComponent<Renderer>().material = colorecubonuovo; 
        }
    }

    
}
