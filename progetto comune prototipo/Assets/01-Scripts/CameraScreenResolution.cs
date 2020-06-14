using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    public bool MaintainWidth = true;
    float DefaultWidth;
    float DefaultHeight;
    Vector3 CameraPos;
    [Range(-1, 1)] public int AdaptPosition;

    // Start is called before the first frame update
    void Start()
    {
        CameraPos = Camera.main.transform.position;
        DefaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
        DefaultHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(MaintainWidth)
        {
            Camera.main.orthographicSize = DefaultWidth / Camera.main.aspect;

            Camera.main.transform.position = new Vector3(CameraPos.x, AdaptPosition * (DefaultHeight - Camera.main.orthographicSize), CameraPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(AdaptPosition * (DefaultWidth - Camera.main.orthographicSize*Camera.main.aspect), CameraPos.y, CameraPos.z);
        }
    }
}
