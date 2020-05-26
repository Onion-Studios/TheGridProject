using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
}
