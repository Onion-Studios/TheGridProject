using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    public bool MaintainWidth = true;
    float DefaultWidth;

    // Start is called before the first frame update
    void Start()
    {
        DefaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(MaintainWidth)
        {
            Camera.main.orthographicSize = DefaultWidth / Camera.main.aspect;
        }
        else
        {

        }
    }
}
