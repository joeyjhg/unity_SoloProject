using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    public GameObject StageManager;
    private AudioSource audioSource;
    public int stageIndex;
    public bool isBoss;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StageManager = GameObject.Find("StageNum");
        stageIndex = StageManager.GetComponent<SceneChange>().stageNum - 1;
        if (stageIndex == 2)
        {
            audioSource.Play();
            Debug.Log("보스음악");
            if(!isBoss)
            {
                audioSource.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
