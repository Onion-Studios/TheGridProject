using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grigliamanager : MonoBehaviour
{
    #region VARIABILI
    int colonne = 5;
    int righe = 5;
    public GameObject cubogriglia;
    private GameObject istanzacubo;
    List<GameObject> listadicubi = new List<GameObject>();
    public Material colorebasegriglia;
    [SerializeField]
    Material colorecentrogriglia;
    Material colorebasegriglia;
    public GameManager GM;
    public int life;
    public bool reciveDamage;
    #endregion



    void Start()
    {
        //quando parte il programma creo la la griglia di cubi prendo una referenza al player
        //setto la posizione del personaggio e spawno il player in centro
        //solo se le righe e le colonne sono dispari se no non abbiamo un centro nella griglia


        if (colonne % 2 == 1 && righe % 2 == 1)
        {
            Creagriglia();
            Playerbehaviour player = FindObjectOfType<Playerbehaviour>();
            player.posizionepersonaggio = new Vector3(2, 1.05f, 2);
            player.Spawn();
        }
        else
        {
            Debug.Log("inserisci un numero di righe e colonne dispari");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo che crea la griglia riga per riga istanziando i cubi e aggiungendo poi i cubi istanziati  
    //alla lista ''listadicubi''
    void Creagriglia()
    {
        for (int c = 0; c < colonne; c++)
        {
            for (int r = 0; r < righe; r++)
            {
                Vector3 posizionerighe = new Vector3(r, 0f, c);
                istanzacubo = Instantiate(cubogriglia, posizionerighe, Quaternion.identity, this.transform);
                if(r == 2 && c == 2)
                {
                    istanzacubo.GetComponent<MeshRenderer>().material = colorecentrogriglia;
                    istanzacubo.GetComponent<cubeprefabehaviour>().iscoloured = true;
                }
                listadicubi.Add(istanzacubo);
            }
        }
    }

    // cerca ogni elemento della lista dei cubi della gliglia
    // e fa tornare le caselle al colore bianco originale
    public void Resetcoloregriglia()
    {
        foreach (var cube in listadicubi)
        {
            if (cube.transform.position == new Vector3(2f, 0, 2f))
            {
                cube.GetComponent<Renderer>().material = colorecentrogriglia;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = true;
            }
            else
            {
                cube.GetComponent<Renderer>().material = colorebasegriglia;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = false;
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (reciveDamage)
        {
            if (life <= 1)
            {
                life -= 1;
                GM.UI.SetCurrentPlayerLife();
                GM.UI.ActiveUI();
            }
            else
            {
                life -= 1;
                reciveDamage = false;
                transform.position = new Vector3(27, 0, 0);
                GM.UI.SetCurrentPlayerLife();
                GM.UI.ActiveUI();

            }
        }
    }*/
}
