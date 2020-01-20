using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterController : MonoBehaviour
{
    float masterDelta = 0;
    float treeDelta = 0;
    float treeSpan = 1.0f;
    float blockDelta = 0;
    float blockSpan = 1;

    // Prefabs
    public GameObject TreeObject1;
    public GameObject TreeObject2;
    public GameObject TreeObject3;
    public GameObject Mountain1;
    public GameObject Mountain2;
    public GameObject Block1;
    public GameObject Block2;
    public GameObject Block3;

    // non-prefabs
    public GameObject hpHearts;

    // Start is called before the first frame update
    void Start()
    {
        instantiateMountain(1);
        instantiateMountain(2);
        blockSpan = this.blockSpawnSpan();
        blockSpan = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // TREEs
        masterDelta += Time.deltaTime;
        treeDelta += Time.deltaTime;
        blockDelta += Time.deltaTime;

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

        if(blockDelta > blockSpan)
        {
            blockDelta = 0;
            blockSpan = this.blockSpawnSpan();

            (GameObject block, Vector3 pos, float scale) = this.randBlock();
            GameObject blockObject = Instantiate(block, pos, Quaternion.identity);
            blockObject.name = "block";
            //blockObject.transform.localScale = new Vector3(scale, scale, 1);
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

    // block, position, scale
    (GameObject, Vector3, float) randBlock()
    {
        switch (Random.Range(0, this.currentStage()))
        {
            case 0:
                return (this.Block1, new Vector3(10, -3.9f, 0), 3 + Random.Range(0, 1.0f));
            case 1:
                return (this.Block2, new Vector3(10, -4.14f, 0), 2 + Random.Range(0, 1.0f));
            case 2:
            default:
                return (this.Block3, new Vector3(10, -3.57f, 0), 0.15f + Random.Range(0, 0.1f * currentStage()));
        }
    }

    float blockSpawnSpan()
    {
        switch(this.currentStage())
        {
            case 1:
                return 5 + Random.Range(0, 10);
            case 2:
                return 4 + Random.Range(0, 5);
            case 3:
            default:
                return 1 + Random.Range(0, 5);
        }
    }

    int currentStage()
    {
        if (this.masterDelta > 3 * 60)
        {
            return 3;
        }

        if (this.masterDelta > 1 * 60)
        {
            return 2;
        }

        return 1;
    }

    public void DecreaseHp()
    {
        this.hpHearts.GetComponent<Image>().fillAmount -= 0.333f;
    }
}
