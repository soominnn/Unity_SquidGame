using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLightGlass : MonoBehaviour
{
    //public GameObject player;
    public Material unbreakable;
    public Material window;
    public GameObject flash_item;

    void Start()
    {
        //일반유리 material와 강화유리 material을 불러서 일반유리 material로 지정한다.
        window = Resources.Load("Window", typeof(Material)) as Material;
        unbreakable = Resources.Load("Unbreakable glass", typeof(Material)) as Material;
        GetComponent<Renderer>().material = window;

        //손전등 아이템과 동기화 위해 오브젝트 생성
        flash_item = GameObject.FindGameObjectWithTag("light"); 
    }

    void OnTriggerStay(Collider col)
    {
        //지정한 범위(collider)에 Player가 들어오고 손전등이 켜진 상태일 때
        //강화유리 material로 변환
        if (col.tag == "Player" && flash_item.GetComponent<Light>().enabled == true)
        {
            GetComponent<Renderer>().material = unbreakable;
        }

        //손전등 꺼진 상태일 때 일반유리 material로 전환
        else if (flash_item.GetComponent<Light>().enabled == false)
        {
            GetComponent<Renderer>().material = window;
        }
    }
}
