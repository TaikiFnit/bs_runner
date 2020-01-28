using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackSheepController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 340.0f;
    public int hp;
    bool isInvincible = false;
    float invicibleTime = 1.3f;
    float deltaInvicible = 0;
    int jumpCount = 0;

    enum JumpState
    {
        Staying,
        Running,
        Jumping,
        Downing
    }

    JumpState jumpState = JumpState.Running;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.deltaInvicible += Time.deltaTime;
        if (jumpState != JumpState.Downing)
        {
            if(this.rigid2D.velocity.y < -3)
            {
                this.animator.SetTrigger("JumpDownTrigger");
                this.jumpState = JumpState.Downing;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpState != JumpState.Downing)
        {
            if (jumpCount < 2)
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                this.jumpCount++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.animator.SetTrigger("RunTrigger");
        this.jumpState = JumpState.Running;
        this.jumpCount = 0;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        this.jumpState = JumpState.Jumping;
        this.animator.SetTrigger("JumpTrigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        
        switch (collision.name)
        {
            case "block":
            case "block1":
            case "block2":
            case "block3":
                onDamaged();
                break;
            case "coin":
                fetchCoin(collision.gameObject);
                break;
            default:
          
                break;
        }
    }

    void fetchCoin(GameObject coin)
    {
        ItemController coinController = coin.GetComponent<ItemController>();
        coinController.fetched();
        GameObject master = GameObject.Find("Master");
        MasterController masterController = master.GetComponent<MasterController>();
        masterController.addScore(10);
    }

    void onDamaged()
    {
        if (this.deltaInvicible > this.invicibleTime)
        {
            this.isInvincible = false;
        }

        if (isInvincible == false)
        {
            // masterにお知らせ
            GameObject master = GameObject.Find("Master");
            MasterController masterController = master.GetComponent<MasterController>();
            masterController.DecreaseHp();


            this.hp = this.hp - 1;
            switch (this.hp)
            {
                case 0:
                    this.animator.SetTrigger("DeadTrigger");
                    masterController.isDead = true;
                    break;
                case 1:
                    this.animator.SetTrigger("Damaged2Trigger");
                    this.isInvincible = true;
                    this.deltaInvicible = 0;
                    break;
                case 2:
                    this.animator.SetTrigger("Damaged1Trigger");
                    this.isInvincible = true;
                    this.deltaInvicible = 0;
                    break;
            }
        }
    }
}
