using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTrap_Trigger : MonoBehaviour
{
    public float triggerX; // 함정을 트리거할 x 좌표
    private Transform playerTransform;
    public float fallingSpeed = 20f; // 함정이 떨어지는 속도

    private bool isFalling = false; // 함정이 떨어지고 있는지 여부
    private bool isTrigger = false;
    public PlayerMove player;
    public CheckPointManager checkPointManager;

    public void Awake()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }
    private void Start()
    {
        // 플레이어 객체의 Transform 컴포넌트를 가져옵니다.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (checkPointManager.checkPointPosition != new Vector3(0, 0, 0))
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float playerX = playerTransform.position.x;

        if (playerX >= triggerX || isTrigger)
        {
            Debug.Log("TriggerFlying");
            isFalling = true;
        }
            
        if (isTrigger && !player.isDie || playerX >= triggerX)
            TriggerFallingTrap();

        //if (transform.position.y > 20 || transform.position.y < -20)
            //Destroy(gameObject);
    }

    void TriggerFallingTrap()
    {
        if (isFalling)
            transform.Translate(Vector3.left * fallingSpeed * Time.deltaTime);
        // 함정 동작을 여기에 구현합니다.
        // 예를 들어, 함정이 떨어지는 애니메이션을 재생하거나 함정에 대한 처리를 수행할 수 있습니다.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트를 검사하여 함정을 활성화합니다.
        isTrigger = true;
        Debug.Log("Trigger");
    }
}
