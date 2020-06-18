using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    GameMaster GM;
    Vector3 originPos;

    private void Start()
    {
        // 시작지점 위치 저장
        originPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // GameMaster컴포넌트 저장
        GM = GameObject.Find("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 몬스터 확인
        if(coll.gameObject.CompareTag("Monster"))
        {
            // 몬스터 제거
            Destroy(coll.gameObject);

            // 점수 올리기
            GM.score += 100;

            // 몬스터 카운트 줄이기
            GM.monsterCount -= 1;

            // 다른 오브젝트 재생성
            GM.RespawnAttackObject(originPos);

            // 본인 제거
            Destroy(this.gameObject);
        }
    }
}
