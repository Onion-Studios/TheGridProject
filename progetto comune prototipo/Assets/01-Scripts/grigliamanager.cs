using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grigliamanager : MonoBehaviour
{
    #region VARIABILI
    int columnns = 5;
    int lines = 5;
    public GameObject cubegrid;
    private GameObject istanzecube;
    public GameObject[] prefabcubigriglia;
    List<GameObject> listadicubi = new List<GameObject>();
    public bool[,] logicgrid = new bool[5, 5];
    public Material inksplash;
    public Material colorbasegrid;
    Managercombo managercombo;
    #endregion



    void Start()
    {
        //quando parte il programma creo la la griglia di cubi, prendo una referenza al player + null check
        //setto la posizione (Playerposition) del personaggio e spawno il player in centro
        //midlogictrue cosi il pezzo in mezzo dov'è la kitsune è attivo, referenza al manager combo + null check

        CreateGrid();
        Playerbehaviour character = FindObjectOfType<Playerbehaviour>();
        if (character == null)
        {
            Debug.LogError("il playerbehaviour è NULL!");
        }
        character.playerposition = new Vector3(-3.24f, 1.05f, 3.16f);
        character.Spawn();
        MidGridLogicTrue();
        managercombo = FindObjectOfType<Managercombo>();
        if (managercombo == null)
        {
            Debug.LogError("il managercombo è NULL!");
        }
        character.frightenedPlayer = character.GetComponentsInChildren<ParticleSystem>()[0];
    }

    void Update()
    {
        
    }

    //metodo che crea la griglia riga per riga istanziando i cubi e aggiungendo poi i cubi istanziati  
    //alla lista ''listadicubi''
    void CreateGrid()
    {
        for (int c = 0; c < columnns; c++)
        {
            for (int r = lines-1; r > -1; r--)
            {
                Vector3 posizionelines = new Vector3(r, 0.5f, c);
                istanzecube = Instantiate(prefabcubigriglia[(((int)posizionelines.z) * 5)+ (4 - (int)posizionelines.x)], posizionelines, Quaternion.identity, this.transform);
                if(r == 2 && c == 2)
                {
                    istanzecube.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = inksplash;
                    istanzecube.GetComponent<cubeprefabehaviour>().iscoloured = true;
                }
                listadicubi.Add(istanzecube);
            }
        }
    }

    // cerca ogni elemento della lista dei cubi della gliglia
    // e fa tornare le caselle al colore bianco originale
    public void ResetColorGrid()
    {
        foreach (var cube in listadicubi)
        {
            if (cube.transform.position == new Vector3(2f, 0.5f, 2f))
            {
                cube.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = inksplash;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = true;
            }
            else
            {
                cube.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = colorbasegrid;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = false;
            }
        }
    }

    //make the middle piece of the logic grid true, because 
    void MidGridLogicTrue()
    {
        logicgrid[2, 2] = true;
    }

    //reset all the piece in the logic grid , exept the center, and turn the boxes active count to zero
    public void ResetGridLogic() 
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                if(logicgrid[x, z] == true)
                {
                    if(x == 2 && z == 2)
                    {
                        logicgrid[x, z] = true;
                    }
                    else
                    {
                        logicgrid[x, z] = false;
                    }
                    
                }
                
            }
        }

        managercombo.CountBoxesActive = 0;
    }

}
