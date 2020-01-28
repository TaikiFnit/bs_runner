using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetController : MonoBehaviour
{
    public GameObject scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.GetComponent<Text>().text = "Score : " + MasterController.scoreInt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MasterController.scoreInt = 0;
            SceneManager.LoadScene("GameScene");
        }
    }
}
