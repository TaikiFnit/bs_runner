using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 cameraPos = camera.transform.position;
        this.transform.position = new Vector3(cameraPos.x, cameraPos.y, 0);
    }
}
