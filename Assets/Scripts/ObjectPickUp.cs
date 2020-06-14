using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectPickUp : MonoBehaviour
{
    public SteamVR_Action_Boolean grip;

    private SteamVR_Behaviour_Pose myHand = null;

    private Transform myTransform = null;
    private Rigidbody myRigidbody = null;

    private Rigidbody currentRigidbody = null;

    private List<Rigidbody> contactRigidbody = new List<Rigidbody>();
    void Start()
    {
        myHand = GetComponent<SteamVR_Behaviour_Pose>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        //그립 버튼 눌렀을 때
        if (grip.GetStateDown(myHand.inputSource))
        {
            //오브젝트를 컨트롤러에 부착
            PickUp();
        }

        //그립 버튼을 뗏을 때
        if (grip.GetStateUp(myHand.inputSource))
        {
            //오브젝트를 컨트롤러에서 떼어냄
            Drop();
        }
    }
    //부딫힌 오브젝트를 컨트롤러에 부착 시키는 함수
    public void PickUp()
    {
        currentRigidbody = GetNearestRigidBody();

        if (currentRigidbody == null) return; //오브젝트의 rigidbody유무 검사

        currentRigidbody.useGravity = false;  //오브젝트 중력 비활성화
        currentRigidbody.isKinematic = true;  //오브젝트 물리효과 비활성화

        currentRigidbody.transform.position = myTransform.position;  //오브젝트의 위치를 컨트롤러 위치로 초기화 시킴
        currentRigidbody.transform.parent = myTransform;    //오브젝트 차일드화 시키기 (부착)

    }
    //컨트롤러에 부착 된 오브젝트를 다시 해제하는 함수
    public void Drop()
    {
        if (currentRigidbody == null) return;

        currentRigidbody.useGravity = true;  //오브젝트 중력 활성화
        currentRigidbody.isKinematic = false;  //오브젝트 물리효과 활성화

        currentRigidbody.transform.parent = null;    //오브젝트 차일드화 해제 시키기

        currentRigidbody.velocity = myHand.GetVelocity(); //오브젝트에 현재 컨트롤러의 움직임 속도를 전달
        currentRigidbody.angularVelocity = myHand.GetAngularVelocity(); //오브젝트에 현재 컨트롤러의 회전 속도를 전달

        currentRigidbody = null;  //연결한 rigidbody변수를 초기화

    }

    //컨트롤러와 오브젝트 의에 충돌이 일어 났을 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            //충돌이 일어난 오브젝트 추가
            contactRigidbody.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }
    //컨트롤러와 오브젝트 간의 충돌이 끝났을 때
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            //충돌이 끝난 오브젝트 삭제
            contactRigidbody.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
    //컨트롤러와 부딫힌 오브젝트 중 가장 가까운 오브젝트 판별
    private Rigidbody GetNearestRigidBody()
    {
        Rigidbody nearestRigidBody = null;

        float minDistance = float.MaxValue;
        float distance = 0f;

        //contactRigidbody 리스트 안에 지정된 Rigidbody 컴포넌트 들을 검사
        foreach (Rigidbody rigi in contactRigidbody)
        {
            //컨트롤러와 rigi를 가지고 있는 오브젝트 사이의 거리 비교
            distance = (rigi.transform.position - myTransform.position).sqrMagnitude;

            //최소거리보다 현재 컨트롤러와 거리가 더 가까우면 최소거리로 변경
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRigidBody = rigi; //최종적으로 가장 가까운 rigidbody컴포넌트 저장
            }
        }
        return nearestRigidBody;  //가장 가까운 rigidboy반환
    }
}
