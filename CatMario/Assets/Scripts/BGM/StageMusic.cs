using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StageMusic : MonoBehaviour
{
    //public AudioMixer mixer;
    private AudioSource audioSource;
    [SerializeField] public AudioClip[] clip;
    public GameObject StageManager;
    private int stageIndex;
    public bool BossStage;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        StageManager = GameObject.Find("StageNum");
        stageIndex = StageManager.GetComponent<SceneChange>().stageNum - 1;
        if (stageIndex == 2)
        {
            //audioSource.clip = clip[0];
            BossStage = true;
        }

    }
    void Update()
    {
        stageIndex = StageManager.GetComponent<SceneChange>().stageNum - 1;
        if (stageIndex == 2)
        {
            //audioSource.clip = clip[0];
            BossStage = true;
        }
    }



}
