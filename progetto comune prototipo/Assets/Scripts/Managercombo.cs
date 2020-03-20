using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Managercombo : MonoBehaviour
{
    //le combo di tasti che equivalgono ad un simbolo 2 per ora
    List<char> Combo1 = new List<char>() {'s', 's', 'a', 'a'};
    List<char> Combo2 = new List<char>() {'d', 's', 's', 'a'};
    Enemyspawnmanager enemyspawnmanager;
    Enemybehaviour enemybehaviour;
    UIManager UIManager;
    

    private void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        enemybehaviour = FindObjectOfType<Enemybehaviour>();
        UIManager = FindObjectOfType<UIManager>();    
    }

    private void Update()
    {
        
    }

    // il metodo vede se la combo tracciata è uguale a una che produce un effetto
    public void checkcombo(List<char> yourcombo)
    {
        if (yourcombo.SequenceEqual(Combo1))
        {
            for(int i=0; i<2; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true && nemicodadistruggere.GetComponent<Enemybehaviour>().segnocorrispondente == 0)
                    {
                        nemicodadistruggere.SetActive(false);
                      
                    }
                }

                foreach(Image segno in UIManager.Dictionaryofsignsprite[0])
                {
                    if(segno.gameObject.activeInHierarchy == true)
                    {
                        segno.gameObject.SetActive(false);
                    }
                }

                Debug.Log("ho attivato la combo1 e ho distrutto i nemici con il segno 0");
            }
        }
        else if (yourcombo.SequenceEqual(Combo2))
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true && nemicodadistruggere.GetComponent<Enemybehaviour>().segnocorrispondente == 1)
                    {
                        nemicodadistruggere.SetActive(false);
                   
                    }
                }

                foreach (Image segno in UIManager.Dictionaryofsignsprite[1])
                {
                    if (segno.gameObject.activeInHierarchy == true)
                    {
                        segno.gameObject.SetActive(false);
                    }
                }

                Debug.Log("ho attivato la combo1 e ho distrutto i nemici con il segno 1");
            }
        }
        else
        {
            Debug.Log("il segno tracciato non è un simbolo valido");
        }

    }
}
