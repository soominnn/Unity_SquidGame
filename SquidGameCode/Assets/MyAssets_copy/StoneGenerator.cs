using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{

    public GameObject StonePrefeb;  //game object 선언
    public GameObject player;
    public GameObject stone;
    public Vector3 pos;
    public int stone_num = 3;

    BeanUI beanManager;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        beanManager = GameObject.Find("Bean").GetComponent<BeanUI>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position + new Vector3(0, 0.5f, 0); //stone 오브젝트 생성 좌표

        Quaternion rot = Quaternion.Euler(-30f, 0f, 0f); //ray 방향 회전 위한 쿼터니언

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //마우스 클릭 위치 구하기
        Vector3 shooting = ray.direction; //ray의 방향구하기
        shooting = shooting.normalized;
        shooting = rot * shooting; //회전

        if (Input.GetMouseButtonDown(0)) //마우스 클릭하면
        {
            if (stone_num > 0 && stone_num <= 3) //구슬 개수는 3개로 제한
            {
                //플레이어 위치에서 게임 오브젝트 생성
                stone = Instantiate(StonePrefeb, pos, Quaternion.identity) as GameObject;
                stone.tag = "stone"; //태그 붙임
                stone.transform.parent = player.transform; //플레이어 종속 아이템으로 함
                //stone controller의 함수로 구슬 던지기
                stone.GetComponent<StoneController>().Shoot(shooting);
            }

            stone_num -= 1;
            beanManager.SetBean(stone_num);
        }
    }
}