using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    public GameObject Top;
    public GameObject Left;
    public GameObject Right;
    public Stage3Manager stage3Manager;

    private bool prisonActivated = false;

    private Vector3 initialTopPosition;
    private Vector3 initialLeftPosition;
    private Vector3 initialRightPosition;
    private Vector3 initialPosition;

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
        initialTopPosition = Top.transform.position;
        initialLeftPosition = Left.transform.position;
        initialRightPosition = Right.transform.position;

        Top.transform.position = initialPosition + Vector3.up * 20.0f;
        Left.transform.position = initialPosition + Vector3.left * 20.0f;
        Right.transform.position = initialPosition + Vector3.right * 20.0f;

        // 초기에 감옥을 투명하게 설정
       // SetPrisonTransparent();
    }

    void Update()
    {
        if (stage3Manager.isTrigger && !prisonActivated)
        {
            StartCoroutine(ActivatePrison());
        }
    }

    IEnumerator ActivatePrison()
    {
        yield return new WaitForSeconds(4f); // 4초 대기

        // 감옥을 원래 위치로 이동
        StartCoroutine(MovePrison(Top, initialTopPosition));
        StartCoroutine(MovePrison(Left, initialLeftPosition));
        StartCoroutine(MovePrison(Right, initialRightPosition));

        prisonActivated = true;
    }

    IEnumerator MovePrison(GameObject prison, Vector3 targetPosition)
    {
        float moveDuration = 1f; // 감옥 이동에 걸리는 시간
        Vector3 initialPosition = prison.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            prison.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        prison.transform.position = targetPosition;
    }

    void SetPrisonTransparent()
    {
        Color transparentColor = new Color(1f, 1f, 1f, 0f);
        SpriteRenderer topRenderer = Top.GetComponent<SpriteRenderer>();
        SpriteRenderer leftRenderer = Left.GetComponent<SpriteRenderer>();
        SpriteRenderer rightRenderer = Right.GetComponent<SpriteRenderer>();

        topRenderer.color = transparentColor;
        leftRenderer.color = transparentColor;
        rightRenderer.color = transparentColor;
    }
}

