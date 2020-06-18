using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    GameMaster GM;
    Vector3 originPos;
    bool isAttack = false;

    private void Start()
    {
        // 시작지점 위치 저장
        originPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // GameMaster컴포넌트 저장
        GM = GameObject.Find("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 테이블일 경우 리턴
        if(coll.gameObject.CompareTag("Table"))
        {
            return;
        }

        // 몬스터 확인
        if(coll.gameObject.CompareTag("Monster"))
        {
            // 겹처서 공격 안되도록 함
            if (!isAttack)
            {             
                isAttack = true;

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
        // 바닥에 그냥 떨어졌을 때
        if (coll.gameObject.CompareTag("Ground"))
        {
            // 다른 오브젝트 재생성
            GM.RespawnAttackObject(originPos);

            // 본인 제거
            Destroy(this.gameObject);
        }
    }
}
