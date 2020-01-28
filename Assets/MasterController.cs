using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    public static int scoreInt = 0;

    // getter
    public static int getHitPoint()
    {
        return scoreInt;
    }

    float masterDelta = 0;
    float treeDelta = 0;
    float treeSpan = 1.0f;
    float blockDelta = 0;
    float blockSpan = 1;
    float[,] coinSpan = new float[3, 3] { { 4, 3, 3 }, { 3, 1.5f, 3 }, { 3, 2, 0.4f } };
    float[] coinDelta = new float[3] { 0, 0, 0 };

    // Prefabs
    public GameObject TreeObject1;
    public GameObject TreeObject2;
    public GameObject TreeObject3;
    public GameObject Mountain1;
    public GameObject Mountain2;
    public GameObject Block1;
    public GameObject Block2;
    public GameObject Block3;
    public GameObject coin;

    // non-prefabs
    public GameObject hpHearts;
    public GameObject score;

    public bool isDead = false;
    float endDelta = 0;
    float endSpan = 2.6f;

    // Start is called before the first frame update
    void Start()
    {
        instantiateMountain(1);
        instantiateMountain(2);
        blockSpan = this.blockSpawnSpan();
        blockSpan = 1;
        score.GetComponent<Text>().text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            endDelta += Time.deltaTime;
        }

        if(endDelta > endSpan)
        {
            SceneManager.LoadScene("GameSet");
        }

        score.GetComponent<Text>().text = "Score: " + scoreInt.ToString();
        // TREEs
        masterDelta += Time.deltaTime;
        treeDelta += Time.deltaTime;
        blockDelta += Time.deltaTime;

        for (int i = 0; i < coinDelta.Length; i++)
        {
            coinDelta[i] += Time.deltaTime;
        }

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

        for(int i = 0; i < coinDelta.Length; i++)
        {
            float y = 0;
            float x = 10;
            switch(i)
            {
                case 0:
                    y = Random.Range(-2.0f, -3.9f);
                    x = Random.Range(10, 12);
                    break;
                case 1:
                    y = Random.Range(-1.0f, 2.0f);
                    x = Random.Range(9, 12);
                    break;
                case 2:
                default:
                    y = Random.Range(3.5f, 7.0f);
                    x = Random.Range(9, 14);
                    break;
            }

            if(coinDelta[i] > coinSpan[this.currentStage() - 1, i])
            {
                coinDelta[i] = 0;

                Vector3 pos = new Vector3(x, y, 0);
                GameObject coinObject = Instantiate(coin, pos, Quaternion.identity);
                coinObject.name = "coin";
            }
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

    public void addScore(int s)
    {
        MasterController.scoreInt += s;
    }
}
