using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Managercombo : MonoBehaviour
{
    #region Variabili
    #region COMBODATABASE
    //le combo di tasti che equivalgono ad un simbolo 2 per ora
    List<char> Combo1 = new List<char>() { 's', 's', 'a', 'a' }; // segno 1 nel gdd numero 5
    List<char> Combo2 = new List<char>() { 'd', 'd', 'w', 'w' }; // speculare segno 1
    List<char> Combo3 = new List<char>() { 'd', 's', 's', 'a' }; // segno 2 nel gdd numero 4
    List<char> Combo4 = new List<char>() { 'd', 'w', 'w', 'a' }; // speculare segno 2
    List<char> Combo5 = new List<char>() { 'a', 'w', 'w', 'd' }; // segno 3 nel gdd numero 1
    List<char> Combo6 = new List<char>() { 'a', 's', 's', 'd' }; // speculare segno 3
    List<char> Combo7 = new List<char>() { 'a', 'a', 'w', 'w' }; // segno 4 nel gdd numero 2
    List<char> Combo8 = new List<char>() { 's', 's', 'd', 'd' }; // speculare segno 4
    List<char> Combo9 = new List<char>() { 'a', 's', 'a', 's' }; // segno 5 nel gdd numero 3
    List<char> Combo10 = new List<char>() { 'w', 'd', 'w', 'd' }; // speculare segno 5
    List<char> Combo11 = new List<char>() { 'd', 's', 'd', 's' }; // segno 6 nel gdd numero 6
    List<char> Combo12 = new List<char>() { 'w', 'a', 'w', 'a' }; // speculare segno 6
    #endregion
    Enemyspawnmanager enemyspawnmanager;
    UIManager UIManager;
    Playerbehaviour playerbehaviour;
    #endregion

    private void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if (enemyspawnmanager == null)
        {
            Debug.LogError("enemyspawnmanager è null");
        }

        UIManager = FindObjectOfType<UIManager>();
        if (UIManager == null)
        {
            Debug.LogError("UImanager è null");
        }

        playerbehaviour = FindObjectOfType<Playerbehaviour>();
        if (playerbehaviour == null)
        {
            Debug.LogError("playerbehaviour è null");
        }

    }

    public void Init()
    {
         //GameManager.Instantiate.GetPlayerBehaviur();
    }

    private void Update()
    {

    }

    // il metodo vede se la combo tracciata è uguale a una che produce un effetto
    public void checkcombo(List<char> yourcombo)
    {
        if (yourcombo.SequenceEqual(Combo1) | yourcombo.SequenceEqual(Combo2))
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true)
                    {
                        switch (i)
                        {
                            case 0:
                                NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                                if (normalenemy.segninormalenemy[0].activeInHierarchy == true)
                                {
                                    normalenemy.Deathforsign();
                                }
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                                if (kamikazenemy.segnikamikazenemy[0].activeInHierarchy == true)
                                {
                                    kamikazenemy.Deathforsign();
                                }
                                break;
                            case 2:
                                GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                                if (goldenenemy.segnigoldenenemy[0].activeInHierarchy == true)
                                {
                                    goldenenemy.Deathforsign();
                                }
                                break;
                            case 3:
                                ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                                if (armoredenemy.segniarmoredenemy[0].activeInHierarchy == true)
                                {
                                    armoredenemy.Deathforsign();
                                }
                                break;
                            case 4:
                                LastWillEnemy lastwillenemy = nemicodadistruggere.GetComponent<LastWillEnemy>();
                                if (lastwillenemy.segnilastwillenemy[0].activeInHierarchy == true)
                                {
                                    lastwillenemy.Deathforsign();
                                }
                                break;
                        }

                    }
                }
            }
        }
        else if (yourcombo.SequenceEqual(Combo3) | yourcombo.SequenceEqual(Combo4))
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true)
                    {
                        switch (i)
                        {
                            case 0:
                                NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                                if (normalenemy.segninormalenemy[1].activeInHierarchy == true)
                                {
                                    normalenemy.Deathforsign();
                                }
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                                if (kamikazenemy.segnikamikazenemy[1].activeInHierarchy == true)
                                {
                                    kamikazenemy.Deathforsign();
                                }
                                break;
                            case 2:
                                GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                                if (goldenenemy.segnigoldenenemy[1].activeInHierarchy == true)
                                {
                                    goldenenemy.Deathforsign();
                                }
                                break;
                            case 3:
                                ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                                if (armoredenemy.segniarmoredenemy[1].activeInHierarchy == true)
                                {
                                    armoredenemy.Deathforsign();
                                }
                                break;
                            case 4:
                                LastWillEnemy lastwillenemy = nemicodadistruggere.GetComponent<LastWillEnemy>();
                                if (lastwillenemy.segnilastwillenemy[1].activeInHierarchy == true)
                                {
                                    lastwillenemy.Deathforsign();
                                }
                                break;
                        }
                    }
                }

            }
        }
        else if (yourcombo.SequenceEqual(Combo5) | yourcombo.SequenceEqual(Combo6))
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true)
                    {
                        switch (i)
                        {
                            case 0:
                                NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                                if (normalenemy.segninormalenemy[3].activeInHierarchy == true)
                                {
                                    normalenemy.Deathforsign();
                                }
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                                if (kamikazenemy.segnikamikazenemy[3].activeInHierarchy == true)
                                {
                                    kamikazenemy.Deathforsign();
                                }
                                break;
                            case 2:
                                GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                                if (goldenenemy.segnigoldenenemy[3].activeInHierarchy == true)
                                {
                                    goldenenemy.Deathforsign();
                                }
                                break;
                            case 3:
                                ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                                if (armoredenemy.segniarmoredenemy[3].activeInHierarchy == true)
                                {
                                    armoredenemy.Deathforsign();
                                }
                                break;
                            case 4:
                                LastWillEnemy lastwillenemy = nemicodadistruggere.GetComponent<LastWillEnemy>();
                                if (lastwillenemy.segnilastwillenemy[3].activeInHierarchy == true)
                                {
                                    lastwillenemy.Deathforsign();
                                }
                                break;
                        }

                    }
                }
            }
        }
        if (yourcombo.SequenceEqual(Combo7) | yourcombo.SequenceEqual(Combo8))
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                {
                    if (nemicodadistruggere.activeInHierarchy == true)
                    {
                        switch (i)
                        {
                            case 0:
                                NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                                if (normalenemy.segninormalenemy[2].activeInHierarchy == true)
                                {
                                    normalenemy.Deathforsign();
                                }
                                break;
                            case 1:
                                KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                                if (kamikazenemy.segnikamikazenemy[2].activeInHierarchy == true)
                                {
                                    kamikazenemy.Deathforsign();
                                }
                                break;
                            case 2:
                                GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                                if (goldenenemy.segnigoldenenemy[2].activeInHierarchy == true)
                                {
                                    goldenenemy.Deathforsign();
                                }
                                break;
                            case 3:
                                ArmoredEnemy armoredenemy = nemicodadistruggere.GetComponent<ArmoredEnemy>();
                                if (armoredenemy.segniarmoredenemy[2].activeInHierarchy == true)
                                {
                                    armoredenemy.Deathforsign();
                                }
                                break;
                            case 4:
                                LastWillEnemy lastwillenemy = nemicodadistruggere.GetComponent<LastWillEnemy>();
                                if (lastwillenemy.segnilastwillenemy[2].activeInHierarchy == true)
                                {
                                    lastwillenemy.Deathforsign();
                                }
                                break;
                        }

                    }
                }
            }
        }
        else
        {
            Debug.Log("il segno tracciato non è un simbolo valido");
        }

    }

    /*public ABC _abc = ABC.a;
    public void test(ABC _name) {
        switch (_name)
        {
            case ABC.a:
            case ABC.b:
                for (int i = 0; i < 3; i++)
                {
                    foreach (GameObject nemicodadistruggere in enemyspawnmanager.poolnemici[i])
                    {
                        if (nemicodadistruggere.activeInHierarchy == true)
                        {
                            switch (i)
                            {
                                case 0:
                                    NormalEnemy normalenemy = nemicodadistruggere.GetComponent<NormalEnemy>();
                                    if (normalenemy.segninormalenemy[0].activeInHierarchy == true)
                                    {
                                        normalenemy.Deathforsign();
                                    }
                                    break;
                                case 1:
                                    KamikazeEnemy kamikazenemy = nemicodadistruggere.GetComponent<KamikazeEnemy>();
                                    if (kamikazenemy.segnikamikazenemy[0].activeInHierarchy == true)
                                    {
                                        kamikazenemy.Deathforsign();
                                    }
                                    break;
                                case 2:
                                    GoldenEnemy goldenenemy = nemicodadistruggere.GetComponent<GoldenEnemy>();
                                    if (goldenenemy.segnigoldenenemy[0].activeInHierarchy == true)
                                    {
                                        goldenenemy.Deathforsign();
                                    }
                                    break;
                            }

                        }
                    }
                }
                break;
            case ABC.c:
                break;
            default:
                break;
        }
    }

    public enum ABC{a,b,c }*/
}
