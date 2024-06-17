using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public static GameManager game;
    public SceneChange sc;
    public void GameScnesCtrl()
    {
        switch (this.gameObject.name)
        {
            case "Start":
                SceneManager.LoadScene("StageSelect");
                break;
            case "Option":
                SceneManager.LoadScene("Option");
                break;
            case "Back":
                SceneManager.LoadScene("MainMenu");
                break;
            case "Quit":    //apk에서는 종료됨
                Application.Quit();
                break;
            case "Stage1":
                sc.stageNum = 1;
                sc.call();             
                break;
            case "Stage2":
                sc.stageNum = 2;
                sc.call();
                break;
            case "Stage3":
                sc.stageNum = 3;
                sc.call();
                break;
        }
        
    }
}
