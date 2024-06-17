using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlay : MonoBehaviour
{
    public AudioMixer mixer;
    private AudioSource audioSource;
    private GameObject[] musics;
    public StageMusic stageMusic;
    public bool stage;
    void Start()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");
        stageMusic = GetComponent<StageMusic>();
        if (musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();

        float bgmVolume = PlayerPrefs.GetFloat("BGM", 1f);
        mixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        //PlayerPrefs.SetFloat("BGM", bgmVolume); // PlayerPrefs에 저장
        //PlayerPrefs.Save();
        
        

        PlayMusic();
    }

    void Update()
    {
        if (stage)
        {
            if (stageMusic.BossStage && stage)
                audioSource.clip = stageMusic.clip[0];
            PlayMusic();
        }
    }


    public void PlayMusic()
    {
        Debug.Log("Play");
        if (audioSource.isPlaying)
            return;
        audioSource.enabled = true;
        audioSource.Play();
       // float bgmVolume = PlayerPrefs.GetFloat("BGM", 1f);
       // Debug.Log("BGM Volume: " + bgmVolume);
       // mixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
        Debug.Log("musicstop");
        Destroy(GameObject.Find("MusicSource"));
    }

    
}
