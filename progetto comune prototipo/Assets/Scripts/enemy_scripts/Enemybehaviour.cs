using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemybehaviour : MonoBehaviour
{
    #region VARIABILI
    public int enemyID; 
    [SerializeField]
    public float speed;
    Enemyspawnmanager enemyspawnmanager;
    public int segnocorrispondente;
    Playerbehaviour Playerbehaviour;
    bool kamikazedestroy = false;
    public enum intensitylevel
    {
        intensita1 = 0,
        intensita2 = 1,
        intensita3 = 2,
    }
    signbehaviours signbehaviours;
    #endregion

    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        Playerbehaviour = FindObjectOfType<Playerbehaviour>();
        signbehaviours = FindObjectOfType<signbehaviours>();
    }

    void Update()
    {
        if(this.gameObject.activeInHierarchy == true)
        {
            Enemymove();
        }

        if(enemyspawnmanager.nemicoucciso >= 0 && enemyspawnmanager.nemicoucciso < 15)
        {
            Statecheck(intensitylevel.intensita1);
        }
        else if(enemyspawnmanager.nemicoucciso >= 15 && enemyspawnmanager.nemicoucciso < 25)
        {
            Statecheck(intensitylevel.intensita2);
        }
        else if(enemyspawnmanager.nemicoucciso >= 25)
        {
            Statecheck(intensitylevel.intensita3);
        }
    }


    public void Deathforgriglia()
    {
        this.gameObject.SetActive(false);
        if(enemyID != 3)
        {
            enemyspawnmanager.nemicoucciso = 0;
        }
        Vector3 randominitialposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
        transform.position = randominitialposition;
        if(enemyID == 2)
        {
            Playerbehaviour.life -= 2;
        }
        else if (enemyID == 3)
        {
            
        }
        else
        {
            Playerbehaviour.life -= 1;
        }
        
    }

    public void Deathforsign()
    {
        enemyspawnmanager.nemicoucciso += 1;
        this.gameObject.SetActive(false);
        Vector3 randominitialposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
        transform.position = randominitialposition;
    }

    public void Enemymove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.localPosition.x > -0.76)
        {
            Deathforgriglia();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "enemy2")
        {
            if(other.gameObject.tag == "enemy0" | other.gameObject.tag == "enemy1")
            {
                other.gameObject.SetActive(false);
            }
        }
        
    }*/

    public void Statecheck(intensitylevel intensità)
    {
        switch (intensità)
        {
            case intensitylevel.intensita1:
                speed = 1f;
                signbehaviours.signspeed = 1f;
                break;
            case intensitylevel.intensita2:
                speed = 1.4f;
                signbehaviours.signspeed = 1.4f;
                break;
            case intensitylevel.intensita3:
                speed = 2f;
                signbehaviours.signspeed = 2f;
                break;
            default:
                break;
        }
    }
}
