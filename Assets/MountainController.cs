using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainController : MonoBehaviour
{
    public int type;
    public float speed = 0.1f;
    public float spawnPosition = 6.09f;
    public bool isInstantiatedNextMountain = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject master = GameObject.Find("Master");
        MasterController masterController = master.GetComponent<MasterController>();
        if (masterController.isDead == false)
        {
            this.transform.position = new Vector3(this.transform.position.x - this.speed, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.x < this.spawnPosition && isInstantiatedNextMountain == false)
            {
                isInstantiatedNextMountain = true;
                masterController.instantiateMountain(type);
            }
        }
    }
}
