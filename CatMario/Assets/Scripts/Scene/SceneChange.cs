using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int stageNum;
    public GameObject stageNumObject;
    public GameObject realstageNum;
    public AudioPlay audioPlay;

    private void Awake()
    {
        // AudioPlay 객체 생성
        audioPlay = FindObjectOfType<AudioPlay>();
    }

    public void call()
    {
        //Debug.Log(stageNum);
        DontDestroyOnLoad(stageNumObject);
        audioPlay.StopMusic();
        SceneManager.LoadScene("Stage");
    }

    
}
