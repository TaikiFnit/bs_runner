using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    float treeDelta = 0;
    float treeSpan = 1.0f;
    public GameObject TreeObject1;
    public GameObject TreeObject2;
    public GameObject TreeObject3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Random.Range(0, 3));
        Debug.Log("Master");
        treeDelta += Time.deltaTime;
        if(treeDelta > treeSpan)
        {
            (GameObject randTree, Vector3 initialPosition) = this.randTree();

            // プレハブからインスタンスを生成
            GameObject treeObject = Instantiate(randTree, initialPosition, Quaternion.identity);

            treeObject.AddComponent<BackgroundController>();
            BackgroundController controller = treeObject.GetComponent<BackgroundController>();
            controller.speed = 0.04f;

            treeDelta = 0;
        }

    }

    (GameObject, Vector3) randTree()
    {
        
        switch (Random.Range(0, 3))
        {
            case 0:
                return (this.TreeObject1, new Vector3(10, -3.15f, 0));
            case 1:
                return (this.TreeObject2, new Vector3(10, -3.6f, 0));
            default:
                return (this.TreeObject3, new Vector3(10, -3.9f, 0));
        }
    }
}
