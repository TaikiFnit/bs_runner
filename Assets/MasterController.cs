using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterController : MonoBehaviour
{
    float treeDelta = 0;
    float treeSpan = 1.0f;

    // Prefabs
    public GameObject TreeObject1;
    public GameObject TreeObject2;
    public GameObject TreeObject3;
    public GameObject Mountain1;
    public GameObject Mountain2;

    // non-prefabs
    public GameObject hpHearts;

    // Start is called before the first frame update
    void Start()
    {
        instantiateMountain(1);
        instantiateMountain(2);
    }

    // Update is called once per frame
    void Update()
    {
        // TREEs
        treeDelta += Time.deltaTime;
        if(treeDelta > treeSpan)
        {
            (GameObject randTree, Vector3 initialPosition) = this.randTree();

            // プレハブからインスタンスを生成
            GameObject treeObject = Instantiate(randTree, initialPosition, Quaternion.identity);

            treeObject.AddComponent<BackgroundController>();
            BackgroundController controller = treeObject.GetComponent<BackgroundController>();
            controller.speed = 0.03f;

            treeDelta = 0;
        }
    }

    public void instantiateMountain(int type)
    {
        (GameObject Mountain, Vector3 pos, float speed, float position) = getMoutainInfo(type);
        GameObject mountain = Instantiate(Mountain, pos, Quaternion.identity);
        mountain.AddComponent<MountainController>();
        MountainController controller = mountain.GetComponent<MountainController>();
        controller.speed = speed;
        controller.spawnPosition = position;
        controller.type = type;
    }

    (GameObject, Vector3, float, float) getMoutainInfo(int type)
    {
        switch (type)
        {
            case 1:
                return (Mountain1, new Vector3(14, -3.65f, 0), 0.02f, 6.09f);
            default:
                return (Mountain2, new Vector3(14, -2.46f, 0), 0.01f, 7.23f);
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

    public void DecreaseHp()
    {
        this.hpHearts.GetComponent<Image>().fillAmount -= 0.333f;
    }
}
