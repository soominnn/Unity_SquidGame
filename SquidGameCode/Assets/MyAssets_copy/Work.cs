using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    //환경 설정
    public float speed = 1.0F;
    public float jumpSpeed = 0.001F;
    public float gravity = 0.001F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 start_pos = new Vector3(0, 0.5f, 0);

    //hpUI 가져오기
    HpUI hpManager;
    private int hp = 5;

    //플레이어가 게임 탈락했을 경우 판별 변수
    private bool dead = false;

    private void Start()
    {
        hpManager = GameObject.Find("Hp").GetComponent<HpUI>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Animator mAvartar = GetComponent<Animator>();

        if (controller.isGrounded)
        {
            //점프 기능 구현
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
                mAvartar.SetTrigger("Jump");
            }

            //쉬프트 점프 기능 구현
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed * 2;
                    mAvartar.SetTrigger("Jump");
                }
            }

            //구슬 던지는 모션 구현
            if (Input.GetMouseButtonDown(0))
            {
                mAvartar.SetTrigger("Throw");
            }

            //방향키에 따르는 캐릭터의 모션 변경(W,A,D는 걷는 모션)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                mAvartar.SetBool("MoveAction0", true);

            }
            else
            {
                mAvartar.SetBool("MoveAction0", false);

            }
            //S는 뒤로 걷는 모션
            if (Input.GetKey(KeyCode.S))
            {
                mAvartar.SetBool("BackMotion", true);
            }
            else
            {
                mAvartar.SetBool("BackMotion", false);
            }

        }

        //중력으로 캐릭터가 y축을 따라 내려오도록 구현
        moveDirection.y -= gravity * Time.deltaTime * 2;
        controller.Move(moveDirection * Time.deltaTime / 20);

        float move_side = 0f;
        float move_forth = 0f;

        //방향키에 따르는 캐릭터의 이동거리 구현
        if (Input.GetKey(KeyCode.W))
        {
            move_forth += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move_forth -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move_side -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move_side += 1f;
        }

        //R키를 누를 시 처음 자리로 부활
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = start_pos;
        }

        transform.Translate(new Vector3(move_side, 0f, move_forth) * Time.deltaTime * 5);

    }

    void OnCollisionEnter(Collision hit)
    {

        //플레이어가 바닥에 떨어졌을 시 hp 감소
        if (hit.collider.CompareTag("deadFloor"))
        {
            dead = true;
        }

        if (hit.collider.CompareTag("startFloor") && dead == true)
        {
            hp -= 1;
            if (hp <= 0)
                GameManager.instance.GameOver();
            hpManager.SetHp(hp);

            dead = false;
        }

    }

    //플레이어와 충돌 물체 처리, 물체를 밀 때 사용
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //플레이어가 바닥에 떨어졌을 시 처음 자리로 부활
        if (hit.collider.CompareTag("deadFloor"))
        {
            transform.position = start_pos;
        }

        //게임 클리어 시
        if (hit.collider.CompareTag("endFloor"))
        {
            GameManager.instance.GameClear();
        }

    }
}