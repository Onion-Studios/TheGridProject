using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybehaviour : MonoBehaviour
{
    public int enemyID; 
    [SerializeField]
    float speed = 1f;
    Enemyspawnmanager enemyspawnmanager;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            
            if (this.transform.localPosition.x > -0.76)
            {
                Death();
            }
        }
 
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
        Vector3 randominitialposition = new Vector3(-9f, 1.3f, Random.Range(0, 5));
        transform.position = randominitialposition;
    }
}
