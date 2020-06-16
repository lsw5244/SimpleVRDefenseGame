using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 몬스터 확인
        if(coll.gameObject.CompareTag("Monster"))
        {
            // 몬스터 제거
            Destroy(coll.gameObject);

            // 점수 올리기
            GameObject.Find("GM").GetComponent<GameMaster>().score += 100;

            // 몬스터 카운트 줄이기
            GameObject.Find("GM").GetComponent<GameMaster>().monsterCount -= 1;

        }
    }
}
