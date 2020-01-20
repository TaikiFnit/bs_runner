using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSheepController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 320.0f;
    public int hp;
    bool isInvincible = false;
    float invicibleTime = 1.3f;
    float deltaInvicible = 0;

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
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.animator.SetTrigger("RunTrigger");
        this.jumpState = JumpState.Running;
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
            default:
          
                break;
        }
    }

    void onDamaged()
    {
        if (this.deltaInvicible > this.invicibleTime)
        {
            this.isInvincible = false;
        }

        if (isInvincible == false)
        {
            this.hp = this.hp - 1;
            switch (this.hp)
            {
                case 0:
                    this.animator.SetTrigger("DeadTrigger");
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

            // masterにお知らせ
            GameObject master = GameObject.Find("Master");
            MasterController masterController =  master.GetComponent<MasterController>();
            masterController.DecreaseHp();
        }
        
    }
}
