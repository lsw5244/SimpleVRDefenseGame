using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public GameObject startUI;
    public GameObject gameInUI;

    public GameObject monster;

    public Vector3 spawnPoint;

    void Start()
    {
        
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
            spawnPoint = new Vector3(Random.Range(-10, 9), 0, Random.Range(2, 8));
            GameObject newMonster = Instantiate(monster, spawnPoint, Quaternion.identity);
            
        }

    }
}
