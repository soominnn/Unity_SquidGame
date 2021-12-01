using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFlashLight : MonoBehaviour
{
    Transform playerPoint;
    Vector3 offset;
    public int light_num = 3;

    HandLightUI HandlightManager;

    void Start()
    {
        //손전등을 끈 상태로 시작
        this.GetComponent<Light>().enabled = false;
        playerPoint = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerPoint.position;
        HandlightManager = GameObject.Find("Handlight").GetComponent<HandLightUI>();
    }

    void Update()
    {
        //Player 위치에서 offset 만큼 떨어진 지점에 위치하도록 설정
        transform.position = playerPoint.position + offset;

        //마우스 왼쪽 버튼 입력
        if (Input.GetMouseButtonDown(1))
        {
            //손전등 개수가 양수면 손전등 켜기
            if (light_num > 0 && light_num <= 3)
            {
                this.GetComponent<Light>().enabled = true;
            }

            //손전등 아이템 수 줄이고 UI 호출
            light_num -= 1;
            HandlightManager.SetHandlight(light_num);
        }

        //Player가 움직이는 키보드 입력을 받으면 손전등을 끔
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
        {
            this.GetComponent<Light>().enabled = false;
        }
    }
}