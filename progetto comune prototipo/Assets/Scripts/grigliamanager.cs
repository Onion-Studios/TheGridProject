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
    List<GameObject> listadicubi = new List<GameObject>();
    public bool[,] logicgrid = new bool[5, 5];
    public Material colorbasegrid;
    [SerializeField]
    Material colormidgrid;
    Managercombo managercombo;
    #endregion



    void Start()
    {
        //quando parte il programma creo la la griglia di cubi prendo una referenza al player
        //setto la posizione del personaggio e spawno il player in centro
        //solo se le lines e le columnns sono dispari se no non abbiamo un centro nella griglia
        if (columnns % 2 == 1 && lines % 2 == 1)
        {
            CreateGrid();
            Playerbehaviour character = FindObjectOfType<Playerbehaviour>();
            if(character == null)
            {
                Debug.LogError("il playerbehaviour è NULL!");
            }
            character.playerposition = new Vector3(2, 1.05f, 2);
            character.Spawn();
        }
        else
        {
            Debug.Log("inserisci un numero di lines e columnns dispari");
        }

        MidGridLogicTrue();
        managercombo = FindObjectOfType<Managercombo>();
        if (managercombo == null)
        {
            Debug.LogError("il managercombo è NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo che crea la griglia riga per riga istanziando i cubi e aggiungendo poi i cubi istanziati  
    //alla lista ''listadicubi''
    void CreateGrid()
    {
        for (int c = 0; c < columnns; c++)
        {
            for (int r = 0; r < lines; r++)
            {
                Vector3 posizionelines = new Vector3(r, 0f, c);
                istanzecube = Instantiate(cubegrid, posizionelines, Quaternion.identity, this.transform);
                if(r == 2 && c == 2)
                {
                    istanzecube.GetComponent<MeshRenderer>().material = colormidgrid;
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
            if (cube.transform.position == new Vector3(2f, 0, 2f))
            {
                cube.GetComponent<Renderer>().material = colormidgrid;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = true;
            }
            else
            {
                cube.GetComponent<Renderer>().material = colorbasegrid;
                cube.GetComponent<cubeprefabehaviour>().iscoloured = false;
            }
        }
    }

    void MidGridLogicTrue()
    {
        logicgrid[2, 2] = true;
    }

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
