using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Stage3Manager : MonoBehaviour
{
    public float triggerX;
    public CameraFollow cameraFollow;
    public PlayerMove playerMove;
    public GameObject bossObject;
    public GameObject Prison;
    public GameObject PrincessPrison;
    public GameManager gameManager;
    public GameObject BackGround;
    public GameObject BossBackGround;
    private float elapsedTime = 0f; // 경과한 시간을 저장하는 변수

    public bool isTrigger;
    public bool isTrigger2;

    private SpriteRenderer prisonRenderer;
    private Transform playerTransform;
    private bool bossActivated = false;
    private bool bossLanded = false;

    private float bossDescentSpeed = 5.0f;
    private float bossLandingHeight = 4.0f;
    private float bossLandingDelay = 10.0f;

    void Awake()
    {
        BackGround.SetActive(false);
        BossBackGround.SetActive(true);
    }

    private void Start()
    {
        cameraFollow.Stage3 = true;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        prisonRenderer = Prison.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float playerX = playerTransform.position.x;

        if (!bossActivated && playerX >= triggerX && playerX <= triggerX + 0.1f)
        {
            playerMove.isDie = true;
            bossActivated = true;
            StartCoroutine(BossDescentCoroutine());
            isTrigger=true; 
        }

        if (bossLanded && (elapsedTime >= bossLandingDelay) && isTrigger2)
        {
            playerMove.isDie = false;
            cameraFollow.Trigger = true;
            isTrigger2 = false;
        }
        if(playerX >= triggerX)
            elapsedTime += Time.deltaTime;

        if (gameManager.stagePoint >= 100)
            Destroy(PrincessPrison);

        Debug.Log(elapsedTime);

        /*if (isTrigger3.transform.position.y < -19)
            Debug.Log("트리거3 발동");*/
    }

    private IEnumerator BossDescentCoroutine()
    {
        Vector3 startPosition = bossObject.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, bossLandingHeight, startPosition.z);
        float elapsedTime = 2.0f;

        while (bossObject.transform.position.y > bossLandingHeight)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / bossDescentSpeed);
            bossObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        isTrigger2 = true;
        bossLanded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(Prison);
        Rigidbody2D bossRigidbody = bossObject.GetComponent<Rigidbody2D>();
        bossRigidbody.gravityScale = 0.3f;
    }

    
}


