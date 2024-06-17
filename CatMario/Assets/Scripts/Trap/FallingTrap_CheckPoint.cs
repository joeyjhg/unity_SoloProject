using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap_CheckPoint : MonoBehaviour
{
    public float triggerX; // 함정을 트리거할 x 좌표
    public float fallingSpeed = 20f; // 함정이 떨어지는 속도
    private bool isFalling = false; // 함정이 떨어지고 있는지 여부
    public CheckPointManager checkPointManager;
    public bool Trigger;
    public PlayerMove player;

    public void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }
    private void Start()
    {
        if(checkPointManager.checkPointPosition == new Vector3(0, 0, 0))
            Trigger=true;
    }

    void Update()
    {
        if (checkPointManager.checkPointPosition != new Vector3(0, 0, 0)&&!Trigger)
            isFalling = true;
        if (!player.isDie)
            TriggerFallingTrap();
        if (transform.position.y > 20 || transform.position.y < -20)
            Destroy(gameObject);
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
