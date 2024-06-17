using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float jumpPower;
    Rigidbody2D rigid;
    bool isFly = false; //공중점프 x
    private Transform playerTransform;
    public float triggerX;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float playerX = playerTransform.position.x;
        if(playerX > triggerX)
        {
            if (Time.timeScale != 0 && Input.GetButtonDown("Jump") && isFly == false)
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
      
        
    }
    void FixedUpdate()
    {

        if (rigid.velocity.y < 0)
        {

            isFly = true;
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                Debug.Log("colliderNnull");
                if (rayHit.distance < 1f)
                {

                    isFly = false;
                    Debug.Log("isFlyFalse");
                }

            }

        }
        // Debug.Log(isFly);
    }
}
