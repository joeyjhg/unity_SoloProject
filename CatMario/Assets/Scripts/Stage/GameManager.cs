using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Vector3 checkpoints;
    public PlayerMove player;
    public SceneChange sc;
    public GameObject[] stages;
    public AudioPlay audioPlay;
    public CheckPointManager checkPointManager;

    //public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UImenuSet;
    public GameObject UIRestartBtn;
    public GameObject UIGoMainBtn;
    public GameObject UIStageSelectBtn;
    public GameObject StageManager;
    public GameObject BackGround;
    public GameObject BossBackGround;



    private void Awake()
    {
        // AudioPlay 객체 생성
        audioPlay = FindObjectOfType<AudioPlay>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
        sc = FindObjectOfType<SceneChange>();
        //BackGround.SetActive(true);
        //BossBackGround.SetActive(false);
    }

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
        if (Input.GetButtonDown("Cancel"))
        {
            if (UImenuSet.activeSelf)
            {
                UImenuSet.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                UImenuSet.SetActive(true);
                Time.timeScale = 0;
            }                
        }
    }


    void Start()
    {
        Time.timeScale = 1;
        StageManager = GameObject.Find("StageNum");
        stageIndex = StageManager.GetComponent<SceneChange>().stageNum - 1;
        
        NowStage();
        PreviousCheckPointDelete();
    }
    
    void PreviousCheckPointDelete() // 이전 체크포인트 제거
    {
        float currentX = player.transform.position.x; // 플레이어의 현재 x값 가져오기

        CheckPoint[] checkpoints = FindObjectsOfType<CheckPoint>(); // 모든 체크포인트 가져오기

        foreach (CheckPoint checkpoint in checkpoints)
        {
            if (checkpoint.transform.position.x <= currentX)
            {
                Destroy(checkpoint.gameObject); // x값이 같거나 더 낮은 체크포인트 제거
            }
        }
    }


    public void Resume()
    {
        UImenuSet.SetActive(false);
        Time.timeScale = 1;
    }

    public void NowStage()
    {
        for (int i = 0; i < stages.Length; i++)
        {            
            if (i != stageIndex)           
                stages[i].SetActive(false);            
            else            
                stages[i].SetActive(true);                        
        }
        UIStage.text = "STAGE " + (stageIndex + 1);
        PlayerReposition();              
    }
    public void NextStage()
    {
        checkPointManager.checkPointPosition = new Vector3(0, 0, 0);
        //Change Stage
        if (stageIndex < stages.Length-1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);                      

            UIStage.text = "STAGE " + (stageIndex + 1);
            sc.stageNum++;
            PlayerReposition();
        }
        else // Game Clear
        {
            //Player Contol Lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("게임 클리어");
            //Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            //btnText.text = "Game Clear";
            BtnSee();
        }

        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;

    }

    public void HealthDown()
    {
        /*if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }                        
        else
        {
            //All Health UI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Die Effect
            player.OnDie();
            //Result UI
            Debug.Log("die");
            //Retry Button UI
            BtnSee();
        }*/
        //All Health UI Off
        //UIhealth[0].color = new Color(1, 0, 0, 0.4f);
        //Player Die Effect
        player.OnDie();
        //Result UI
        Debug.Log("die");
        //Retry Button UI
        BtnSee();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {          
            //Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }           
            //health Down
            HealthDown();
        }
            
    }

    void PlayerReposition()
    {        
        if (checkPointManager.checkPointPosition != new Vector3(0, 0, 0))
        {
            player.transform.position = checkPointManager.checkPointPosition;
            player.transform.position += new Vector3(0, 1, 0);
            //Debug.Log("check");
        }
        else
        {
            player.transform.position = new Vector3(0, 1, -1);
           // Debug.Log("Ncheck");
        }        
        player.VelocityZero();
    }

    public void BtnSee()
    {
        UIRestartBtn.SetActive(true);
        UIGoMainBtn.SetActive(true);
        //UIStageSelectBtn.SetActive(true);
    }

    public void Restart()
    {
        //Time.timeScale = 1;
        //checkPointManager.checkPointPosition = new Vector3(0, 0, 0);
        StageManager.GetComponent<SceneChange>().stageNum = stageIndex + 1;
        SceneManager.LoadScene(3);
    }

    public void GoMain()
    {
        audioPlay.PlayMusic();
        checkPointManager.checkPointPosition = new Vector3(0, 0, 0);
        Destroy(GameObject.Find("StageNum"));
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("StageBGM"));
    }

    public void StageSelect()
    {
        audioPlay.PlayMusic();
        checkPointManager.checkPointPosition = new Vector3(0, 0, 0);        
        Destroy(GameObject.Find("StageNum"));
        SceneManager.LoadScene("StageSelect");
        Destroy(GameObject.Find("StageBGM"));
    }
    
}
