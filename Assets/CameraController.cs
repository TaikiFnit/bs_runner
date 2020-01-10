using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Black_Sheep_Run");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        if(playerPos.y > 0)
        {
            transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
        } else
        {

        }
        
    }
}
