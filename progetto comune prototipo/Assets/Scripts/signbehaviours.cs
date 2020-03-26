using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signbehaviours : MonoBehaviour
{
    public int signID;
    Enemybehaviour enemybehaviour;
    public RectTransform RectTransform;
    float signspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemybehaviour = FindObjectOfType<Enemybehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            SignMove();
        }
    }

    public void SignMove()
    {
        RectTransform.Translate(Vector3.right * signspeed * Time.deltaTime);
        if (RectTransform.position.x > -0.76f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
