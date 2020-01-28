using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float destorySpan = 60;
    float destoryDelta = 0;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destoryDelta += Time.deltaTime;
        GameObject master = GameObject.Find("Master");
        MasterController masterController = master.GetComponent<MasterController>();
        if (masterController.isDead == false)
        {
            this.transform.position = new Vector3(this.transform.position.x - this.speed, this.transform.position.y, this.transform.position.z);
        }

        if (destoryDelta > destorySpan)
        {
            Destroy(this.gameObject);
        }
    }
}
