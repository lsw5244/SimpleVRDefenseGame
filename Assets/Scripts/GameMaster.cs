using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Text enermyCountText;
    public Text scoreText;
    public Text gameOverScoreText;

    public GameObject leftController;
    public GameObject rightController;

    public GameObject startUI;
    public GameObject gameInUI;
    public GameObject gameoverUI;

    public GameObject[] monsters;
    public GameObject[] attackObjects;
    public Transform[] attackObjectsSpawnPosition;

    private Vector3 spawnPoint;
    [HideInInspector]
    public int monsterCount = 0;
    public int score = 0;
    bool isGameOver = false;

    private void Update()
    {
        if(monsterCount >= 10) //10마리 쌓임 (게임오버)
        {
            GameOver();
        }
        // 유닛 카운트 증가
        enermyCountText.text = monsterCount + "/10";
        scoreText.text = score + "";
    }


    public void StartButtonClick()  //시작버튼 클릭
    {
        // LineRenderer 및 LaserPointer 활성화
        leftController.GetComponent<LaserPointer>().enabled = false;
        leftController.GetComponent<LineRenderer>().enabled = false;
        rightController.GetComponent<LaserPointer>().enabled = false;
        rightController.GetComponent<LineRenderer>().enabled = false;
        //시작 UI비활성화
        startUI.SetActive(false);
        //게임 시작 UI활성화
        gameInUI.SetActive(true);
        // 몬스터 생성 시작
        StartCoroutine(SpawnMonster());

        // 공격 오브젝트 6개 생성
        foreach(Transform tr in attackObjectsSpawnPosition)
        {
            int idx = Random.Range(0, 100) % attackObjects.Length;
            Instantiate(attackObjects[idx], tr.position, Quaternion.identity);
        }
    }

    public void GameRestartButtonClick()
    {
        //게임 다시 시작(초기화)
        monsterCount = 0;
        score = 0;
        isGameOver = false;

        // LineRenderer 및 LaserPointer 활성화
        leftController.GetComponent<LaserPointer>().enabled = false;
        leftController.GetComponent<LineRenderer>().enabled = false;
        rightController.GetComponent<LaserPointer>().enabled = false;
        rightController.GetComponent<LineRenderer>().enabled = false;
        // UI변경
        gameInUI.SetActive(true);
        gameoverUI.SetActive(false);
        // 몬스터 생성 시작
        StartCoroutine(SpawnMonster());

    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1f);

        while (!isGameOver)
        {
            if(Random.Range(1, 101) >= 60)   // 약 60%확률로 정면스폰 40%확률로 측면 스폰
            {
                spawnPoint = new Vector3(Random.Range(-10, 9), 0, Random.Range(2, 8));  //정면 스폰 장소 설정
            }
            else
            {
                spawnPoint = new Vector3(Random.Range(3, 9), 0, Random.Range(-10, 9));  //측면 스폰 장소 설정
            }

            GameObject newMonster = Instantiate(monsters[(Random.Range(1, 100) % 2)], spawnPoint, Quaternion.identity);  // 랜덤 몬스터 생성
            yield return new WaitForSeconds(1f);
        }
    }

    void GameOver()
    {
        // 게임 오버 및 코루틴 종료
        isGameOver = true;
        StopAllCoroutines();
        // 생성된 몬스터 제거
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject monster in monsters)
        {
            Destroy(monster.gameObject);
        }
        // UI변경
        gameInUI.SetActive(false);
        gameoverUI.SetActive(true);
        // 점수 표시
        gameOverScoreText.text = "Score : " + score;
        // LineRenderer 및 LaserPointer 활성화
        leftController.GetComponent<LaserPointer>().enabled = true;
        leftController.GetComponent<LineRenderer>().enabled = true;
        rightController.GetComponent<LaserPointer>().enabled = true;
        rightController.GetComponent<LineRenderer>().enabled = true;
    }
    // 공격 오브젝트 재생성
    public void RespawnAttackObject(Vector3 pos)
    {
        Instantiate(attackObjects[Random.Range(0, 100) % attackObjects.Length], pos, Quaternion.identity);
    }
}
