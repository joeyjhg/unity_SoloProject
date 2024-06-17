using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform 컴포넌트

    public float fixedY; // 고정할 y축 위치
    public bool Stage3,Trigger,CameraTrigger;
    private float PlayerX;

    void Start()
    {
        // 초기에 플레이어의 y축 위치를 고정시킴
        //fixedY = transform.position.y;
    }

    public void LateUpdate()
    {
        // 플레이어의 x, y, z 위치로 카메라 위치를 설정
        if (!Stage3)
            transform.position = new Vector3(player.position.x, fixedY, -10);
        else
        {
            if (!Trigger)
            {
                transform.position = new Vector3(player.position.x + 4, fixedY, -10);
            }
            else // 보스전 카메라고정
            {
                if (!CameraTrigger)
                    PlayerXVector();
                transform.position = new Vector3(PlayerX + 4, fixedY, -10);
            }
                

        }

    }
    void PlayerXVector()
    {
        PlayerX = player.position.x;
        CameraTrigger = true;
    }



}

