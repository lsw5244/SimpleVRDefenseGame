using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public GameObject startUI;
    public GameObject gameInUI;
    public GameObject gameoverUI;

    public GameObject[] monsters;

    private Vector3 spawnPoint;
    [HideInInspector]
    public int monsterCount = 0;

    private void Update()
    {
        if(monsterCount >= 10) //10마리 쌓임
        {
            GameOver();
        }
    }




    public void StartButtonClick()  //시작버튼 클릭
    {
        Debug.Log("START BTN CLICK@@@@@@@@@@@@@@@@@@@@");
        //컨트롤러 비활성화
        leftController.GetComponent<LaserPointer>().enabled = false;
        leftController.GetComponent<LineRenderer>().enabled = false;
        rightController.GetComponent<LaserPointer>().enabled = false;
        rightController.GetComponent<LineRenderer>().enabled = false;
        //시작 UI비활성화
        startUI.SetActive(false);
        //게임 시작 UI활성화
        gameInUI.SetActive(true);

        StartCoroutine(SpawnMonster());
    }

    public void GameRestartButtonClick()
    {
        Debug.Log("RESET@@@@@@@@@@@@@@@@@@@@@");
    }


    IEnumerator SpawnMonster()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            if(Random.Range(1, 101) >= 60)   // 약 60%확률로 정면스폰 40%확률로 측면 스폰
            {
                spawnPoint = new Vector3(Random.Range(-10, 9), 0, Random.Range(2, 8));  //정면 스폰 장소 설정
            }
            else
            {
                spawnPoint = new Vector3(Random.Range(3, 9), 0, Random.Range(-10, 9));  //측면 스폰 장소 설정
            }

            GameObject newMonster = Instantiate(monsters[(Random.Range(1, 100) % 2)], spawnPoint, Quaternion.identity);  // 랜덤 몬스터 생성
            
        }
    }

    void GameOver()
    {

    }
}
