using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    //private bool firstTime = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("FirstTime"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGM", 1f);
            mixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
            slider.value = bgmVolume;
        }
        else
        {
            PlayerPrefs.SetFloat("BGM", 1f);
            mixer.SetFloat("BGM", Mathf.Log10(1f) * 20);
            slider.value = 1f;
            PlayerPrefs.SetString("FirstTime", "no");
        }
    }


    public void SetLevel(float sliderValue)
    {
        // Mixer의 볼륨 설정하기
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        mixer.SetFloat("StageBGM", Mathf.Log10(sliderValue) * 20);


        // PlayerPrefs에 BGM 값을 저장하기
        PlayerPrefs.SetFloat("BGM", sliderValue);
    }
}
