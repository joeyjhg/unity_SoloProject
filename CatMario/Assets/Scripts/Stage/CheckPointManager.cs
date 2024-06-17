using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체크포인트 관리자 싱글톤 클래스 예시
public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    public Vector3 checkPointPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log(instance);
            Destroy(gameObject);
        }
    }
}

// 체크포인트 위치 저장
//CheckPointManager.instance.checkPointPosition = checkPointPosition;

// 체크포인트 위치 로드
//Vector2 checkPointPosition = CheckpointManager.instance.checkPointPosition;
// 체크포인트 위치를 사용하여 플레이어를 이동시키거나 다른 작업을 수행합니다.

