using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;


    public GameObject startUI;

    void Start()
    {
        
    }


    public void StartButtonClick()
    {
        Debug.Log("START BTN CLICK@@@@@@@@@@@@@@@@@@@@");
        //컨트롤러 비활성화
        leftController.GetComponent<LaserPointer>().enabled = false;
        leftController.GetComponent<LineRenderer>().enabled = false;
        rightController.GetComponent<LaserPointer>().enabled = false;
        rightController.GetComponent<LineRenderer>().enabled = false;

        startUI.SetActive(false);
    }

    public void GameRestartButtonClick()
    {
        Debug.Log("RESET@@@@@@@@@@@@@@@@@@@@@");
    }
}
