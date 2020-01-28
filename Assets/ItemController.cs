using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
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
        if (destoryDelta > destorySpan)
        {
            Destroy(this.gameObject);
        }
    }

    public void fetched()
    {
        Destroy(this.gameObject);
    }
}
