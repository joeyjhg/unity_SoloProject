using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MaxSpeed;
    public float jumpPower;
    public GameManager gameManager;
    public CheckPointManager checkPointManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public AudioClip audioCheck;

    CapsuleCollider2D capsulecollider;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    Animator anim;
    bool isFly = false; //공중점프 x
    public bool isDie = false;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
            case "CHECK":
                audioSource.clip = audioCheck;
                break;
        }
        audioSource.Play();
    }

    void Update()
    {
        //Debug.Log(gameManager.checkReach);
        //Jump
        if (Time.timeScale != 0 && Input.GetButtonDown("Jump") && !anim.GetBool("isJumping") && !isFly && !isDie)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");

        }

        if (Time.timeScale != 0 && Input.GetButtonUp("Horizontal") &! isDie)    //Stop Speed
        {
          rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Time.timeScale != 0 && Input.GetButton("Horizontal") &! isDie)  //Direction Sprite
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
               // Debug.Log(Input.GetAxisRaw("Horizontal"));
            }
        }


        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
            anim.SetBool("isWalking", true);
    }
    void FixedUpdate()
    {
        if (!isDie)
        {
            //Move By Key Control
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > MaxSpeed) //Right Max Speed
                rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < MaxSpeed * (-1)) //Left Max Speed
                rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);
        }
        

        //Debug.Log(rigid.velocity.y);
        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            
            isFly = true;
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                Debug.Log("rayHit.collider != null");
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log("rayHit.distance < 0.5f");
                    anim.SetBool("isJumping", false);
                    isFly = false;
                }
                
            }
            
        }
       // Debug.Log(isFly);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemySpike")
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y && collision.gameObject.tag == "Enemy")
            {
                OnAttack(collision.transform);
            }
            else //Damaged
                OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            PlaySound("ITEM");
            //Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            if(isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;

            //Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            PlaySound("FINISH");
            // Next Stage
            gameManager.NextStage();
        }

        else if (collision.gameObject.tag == "CheckPoint")
        {
            CheckPoint checkpoint = collision.gameObject.GetComponent<CheckPoint>();
            if (checkpoint != null)
            {
                checkPointManager.checkPointPosition = checkpoint.transform.position;
                Debug.Log(checkPointManager.checkPointPosition);
            }
            PlaySound("CHECK");
        }



    }

    void OnAttack(Transform enemy)
    {
        PlaySound("ATTACK");
        // Point
        gameManager.stagePoint += 100;
        // Reaction Force
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos)
    {
        PlaySound("DAMAGED");
        // Health Down
        gameManager.HealthDown();
        // Change Layer (Immortal Active)
        gameObject.layer = 11;
        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);

        // Animation
        anim.SetTrigger("doDamaged");
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        PlaySound("DIE");
        //Sprite Alpha 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsulecollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        isDie=true;
        Debug.Log(isDie);
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
