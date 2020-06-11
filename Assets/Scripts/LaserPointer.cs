using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;

    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;  //트리거

    public float maxDistance = 20.0f; //라인의 최대 거리

    // 레이캐스트를 위한 변수 선언
    private RaycastHit hit;
    // 컨트롤러의 Transfrom 컴포넌트를 저장 할 변수
    private Transform tr;


    void Start()
    {
        tr = GetComponent<Transform>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        CreateLineRenderer();

    }

    void Update()
    {
        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            // 라인의 끝점의 위치를 레이캐스팅한 지점의 좌표로 변경
            line.SetPosition(1, new Vector3(0, 0, hit.distance));


            // 트리거 버튼을 클릭했을 경우에 클릭 이벤트를 발생시킴
            if (trigger.GetStateDown(hand))
            {
                ExecuteEvents.Execute(hit.collider.gameObject
                    , new PointerEventData(EventSystem.current)
                    , ExecuteEvents.pointerClickHandler);

                line.material.color = Color.gray;
            }
            if(trigger.GetStateUp(hand))
            {
                line.material.color = Color.black;
            }

        }
        else  //충돌 안했을 때
        {
            line.SetPosition(1, new Vector3(0, 0, maxDistance));
            line.material.color = Color.black;
        }
    }



    private void CreateLineRenderer()
    {
        // LineRender생성
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.receiveShadows = false;

        // 시작과 끝점의 위치 설정
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(0, 0, maxDistance));

        // 라인의 너비 설정
        line.startWidth = 0.03f;
        line.endWidth = 0.005f;

        // 라인의 머터리얼 및 색상 설정
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = Color.black;
    }
}
