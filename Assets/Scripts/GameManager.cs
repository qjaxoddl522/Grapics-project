using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject clearCam;
    public Player player;
    Vector3 playerSpawn = new Vector3(0, 0, 0);
    public static float time = 0;
    public static bool isStart;
    public GameObject[] stages;
    public int currentStage;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject resultPanel;
    public GameObject clearPanel;
    public GameObject nextPanel;
    public Text timeText;
    public Text stageText;

    public void NextStage()
    {
        isStart = false;
        GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject b in bullet)
        {
            Destroy(b);
        }
        if (currentStage < stages.Length-1)
        {
            if (currentStage != -1)
            {
                stages[currentStage].SetActive(false);
                currentStage++;
                stages[currentStage].SetActive(true);
            }
            switch (currentStage)
            {
                case -1: { currentStage++; player.transform.position = playerSpawn; stageText.text = "Stage 1"; break; }
                case 1: { playerSpawn = new Vector3(0, 0, -5); player.transform.position = playerSpawn; stageText.text = "Stage 2"; break; }
            }
            time = 0;

            menuPanel.SetActive(false);
            gamePanel.SetActive(true);
            resultPanel.SetActive(false);
            nextPanel.SetActive(false);

            menuCam.SetActive(false);
            gameCam.SetActive(true);
            clearCam.SetActive(false);

            nextPanel.SetActive(true);
            Invoke("GameStart", 2);
        }
        else
        {
            GameClear();
        }
    }

    public void GameStart()
    {
        isStart = true;
        time = 0;

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        resultPanel.SetActive(false);
        nextPanel.SetActive(false);

        player.transform.position = playerSpawn;
        player.gameObject.SetActive(true);
    }

    public void GameStop()
    {
        isStart = false;

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);

        player.gameObject.SetActive(false);
    }

    public void GameClear()
    {
        isStart = false;

        menuCam.SetActive(false);
        gameCam.SetActive(false);
        clearCam.SetActive(true);

        gamePanel.SetActive(false);
        clearPanel.SetActive(true);

        player.transform.position = new Vector3(0, 0, -5);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject.Find("Player").GetComponent<Player>().anim.SetBool("isClear", true);
    }

    public void GameEnd()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void GameMain()
    {
        isStart = false;

        stages[currentStage].SetActive(false);
        currentStage = -1;
        stages[0].SetActive(true);

        menuCam.SetActive(true);
        gameCam.SetActive(false);
        clearCam.SetActive(false);

        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);
        clearPanel.SetActive(false);

        playerSpawn = new Vector3(0, 0, 0);
        player.transform.position = playerSpawn;
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        player.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().anim.SetBool("isClear", false);
    }
    void Awake()
    {
        isStart = false;

        int setWidth = 1280; // 화면 너비
        int setHeight = 1024; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, true);
    }

    private void Update()
    {
        if (isStart)
            time += Time.deltaTime;
        if (time >= 15 && isStart)
        {
            NextStage();
        }
    }

    void LateUpdate()
    {
        float second = (time % 60);
        timeText.text = string.Format("{0:00.00}", second);
    }
}
