using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Managercombo : MonoBehaviour
{
    #region COMBODATABASE
    //le combo di tasti che equivalgono ad un simbolo 2 per ora
    List<char> Combo1 = new List<char>() {'s', 's', 'a', 'a'}; // segno 1 nel gdd numero 5
    List<char> Combo2 = new List<char>() {'d', 'd', 'w', 'w'}; // speculare segno 1
    List<char> Combo3 = new List<char>() {'d', 's', 's', 'a'}; // segno 2 nel gdd numero 4
    List<char> Combo4 = new List<char>() {'d', 'w', 'w', 'a'}; // speculare segno 2
    List<char> Combo5 = new List<char>() {'a', 'w', 'w', 'd'}; // segno 3 nel gdd numero 1
    List<char> Combo6 = new List<char>() {'a', 's', 's', 'd'}; // speculare segno 3
    List<char> Combo7 = new List<char>() {'a', 'a', 'w', 'w'}; // segno 4 nel gdd numero 2
    List<char> Combo8 = new List<char>() {'s', 's', 'd', 'd'}; // speculare segno 4
    List<char> Combo9 = new List<char>() {'a', 's', 'a', 's'}; // segno 5 nel gdd numero 3
    List<char> Combo10 = new List<char>() {'w', 'd', 'w', 'd'}; // speculare segno 5
    List<char> Combo11 = new List<char>() {'d', 's', 'd', 's'}; // segno 6 nel gdd numero 6
    List<char> Combo12 = new List<char>() {'w', 'a', 'w', 'a'}; // speculare segno 6
    #endregion
    Enemyspawnmanager enemyspawnmanager;
    Enemybehaviour enemybehaviour;
    UIManager UIManager;
    Playerbehaviour playerbehaviour;
    

    private void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        enemybehaviour = FindObjectOfType<Enemybehaviour>();
        UIManager = FindObjectOfType<UIManager>();
        playerbehaviour = FindObjectOfType<Playerbehaviour>();
    }

    private void Update()
    {
        
    }

    // il metodo vede se la combo tracciata è uguale a una che produce un effetto
    public void checkcombo(List<char> yourcombo)
    {
        if (yourcombo.SequenceEqual(Combo1) | yourcombo.SequenceEqual(Combo2))
        {
            for(int i=0; i<4; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true && nemicodadistruggere.GetComponent<Enemybehaviour>().segnocorrispondente == 0)
                    {
                        nemicodadistruggere.GetComponent<Enemybehaviour>().Deathforsign();
                        
                        if(i == 3)
                        {
                            playerbehaviour.Gold += 10;
                        }
                      
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
        else if (yourcombo.SequenceEqual(Combo3) | yourcombo.SequenceEqual(Combo4))
        {
            for (int i = 0; i < 4; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true && nemicodadistruggere.GetComponent<Enemybehaviour>().segnocorrispondente == 1)
                    {
                        nemicodadistruggere.GetComponent<Enemybehaviour>().Deathforsign();
                        
                        if (i == 3)
                        {
                            playerbehaviour.Gold += 10;
                        }

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
