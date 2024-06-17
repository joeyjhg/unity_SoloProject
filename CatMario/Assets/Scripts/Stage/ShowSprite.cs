using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSprite : MonoBehaviour
{
    public GameObject spriteObject; // 스프라이트 렌더러 컴포넌트를 참조하기 위한 변수
    public GameManager gameManager;

    private void Start()
    {
        spriteObject.SetActive(false);
    }

    private void Update()
    {        
        // stagePoint가 1800 이상인 경우 스프라이트 활성화
        if (gameManager.stagePoint >= 1800)
        {
            Debug.Log("1800");
            spriteObject.SetActive(true);
        }
    }
}
