using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    float destorySpan = 15;
    float destoryDelta = 0;
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
        if(masterController.isDead == false)
        {
            this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
        }

        if(destoryDelta > destorySpan)
        {
            Destroy(this.gameObject);
        }
    }
}
