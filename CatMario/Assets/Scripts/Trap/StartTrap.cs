using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrap : MonoBehaviour
{
    public float fallingSpeed = 20f; // 함정이 떨어지는 속도
    public PlayerMove player;

    void Update()
    {
        // 플레이어의 x 좌표와 함정을 트리거할 x 좌표를 비교하여 트리거를 발동합니다.
        if(!player.isDie)
            RightTrap();
                
    }

    void RightTrap()
    {      
            transform.Translate(Vector3.right * fallingSpeed * Time.deltaTime);
        // 함정 동작을 여기에 구현합니다.
        // 예를 들어, 함정이 떨어지는 애니메이션을 재생하거나 함정에 대한 처리를 수행할 수 있습니다.
    }
}
