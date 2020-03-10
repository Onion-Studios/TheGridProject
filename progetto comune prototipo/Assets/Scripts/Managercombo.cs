using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Managercombo : MonoBehaviour
{
    //le combo di tasti che equivalgono ad un simbolo 2 per ora
    List<char> Combo1 = new List<char>() {'d', 'd', 'r'};
    List<char> Combo2 = new List<char>() {'f', 'f', 'l'};
    Enemyspawnmanager enemyspawnmanager;

    private void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    private void Update()
    {
        
    }

    // il metodo vede se la combo tracciata è uguale a una che produce un effetto
    public void checkcombo(List<char> yourcombo)
    {
        if (yourcombo.SequenceEqual(Combo1))
        {
            foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[0])
            {
                if (nemicodadistruggere.activeInHierarchy == true)
                {
                    nemicodadistruggere.SetActive(false);
                    Vector3 randomposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    nemicodadistruggere.transform.position = randomposition;
                }
            }
            Debug.Log("ho attivato la combo1 e ho distrutto il nemico viola");
        }
        else if (yourcombo.SequenceEqual(Combo2))
        {
            foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[1])
            {
                if (nemicodadistruggere.activeInHierarchy == true)
                {
                    nemicodadistruggere.SetActive(false);
                    Vector3 randomposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
                    nemicodadistruggere.transform.position = randomposition;
                }
            }

            Debug.Log("ho attivato la combo2 e ho distrutto il nemico nero");
        }
        else
        {
            Debug.Log("il segno tracciato non è un simbolo valido");
        }

    }
}
