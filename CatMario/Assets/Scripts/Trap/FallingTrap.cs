using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    public float triggerX; // 함정을 트리거할 x 좌표
    private Transform playerTransform;
    public float fallingSpeed = 20f; // 함정이 떨어지는 속도
    public float activationDelay = 0f;

    private bool isFalling = false; // 함정이 떨어지고 있는지 여부
    public PlayerMove player;

    public void Awake()
    {        
        player = FindObjectOfType<PlayerMove>();
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
        if (!player.isDie)
            TriggerFallingTrap();
        if (transform.position.y > 20 || transform.position.y < -20)
            Destroy(gameObject);
    }

    void isFallingTrue()
    {
        isFalling = true;
    }

    void TriggerFallingTrap()
    {           
        if (isFalling)
        {
            transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
            Debug.Log("falling");
        }
            
        // 함정 동작을 여기에 구현합니다.
        // 예를 들어, 함정이 떨어지는 애니메이션을 재생하거나 함정에 대한 처리를 수행할 수 있습니다.
    }
}
