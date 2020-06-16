using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    void Start()
    {
        GameObject.Find("GM").GetComponent<GameMaster>().monsterCount++;
        Debug.Log(GameObject.Find("GM").GetComponent<GameMaster>().monsterCount);
        transform.LookAt(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
