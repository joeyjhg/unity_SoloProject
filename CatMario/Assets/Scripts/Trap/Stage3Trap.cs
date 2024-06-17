using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Trap : MonoBehaviour
{
    public float triggerX =8; // 함정을 트리거할 x 좌표
    private Transform playerTransform;
    public float fallingSpeed = 5f; // 함정이 떨어지는 속도
    public float activationDelay = 0f;
    public bool VectorDown;
    public bool VectorUp;
    public bool VectorRight;
    public bool VectorLeft;
    public PlayerMove playermove;

    private bool isFalling = false; // 함정이 떨어지고 있는지 여부
    private void Awake()
    {
        playermove = FindObjectOfType<PlayerMove>();
    }
    
    private void Start()
    {
        // 플레이어 객체의 Transform 컴포넌트를 가져옵니다.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 플레이어의 x 좌표와 함정을 트리거할 x 좌표를 비교하여 트리거를 발동합니다.
        float playerX = playerTransform.position.x;

        if (playerX >= triggerX && playerX <= triggerX + 0.1f)
            Invoke("isFallingTrue", activationDelay);

        if (VectorDown)
            TriggerFallingTrap(Vector3.down);
        if (VectorUp)
            TriggerFallingTrap(Vector3.up);
        if (VectorRight)
            TriggerFallingTrap(Vector3.right);
        if (VectorLeft)
            TriggerFallingTrap(Vector3.left);




        if (VectorDown || VectorUp)
            if (transform.position.y > 20 || transform.position.y < -20)
                Destroy(gameObject);

        if (VectorRight || VectorLeft)
            if (transform.position.x > 30 || transform.position.x < -25)
                Destroy(gameObject);
    }

    void isFallingTrue()
    {
        isFalling = true;
    }

    void TriggerFallingTrap(Vector3 vectorDirection)
    {
        if (isFalling && !playermove.isDie) 
        {
            transform.Translate(vectorDirection * fallingSpeed * Time.deltaTime);
            //Debug.Log("falling");
        }

        // 함정 동작을 여기에 구현합니다.
        // 예를 들어, 함정이 떨어지는 애니메이션을 재생하거나 함정에 대한 처리를 수행할 수 있습니다.
    }
}


