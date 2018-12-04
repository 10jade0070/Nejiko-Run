using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const int LaneWidth = 1.0f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;
    public float speedZ;
    public float speedX; // 수평 방향 속도의 파라미터
    public float speedJump;
    public float accelerationZ; // 전진 가속도의 파라미터

    void Start()
    {
        // 필요한 컴포넌트를 자동 취득
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 디버그용
        if (Input.GetkeyDown("left")) MoveToleft();
        if (Input.GetkeyDown("right")) MoveToRight();
        if (Input.GetkeyDown("space")) Jump();

        // 서서히 가속하여 Z방향으로 계속 전진시킨다
        float acceleratedZ = moveDirection.z + (acceleratedZ * Time.deltaTime); // 전진 속도 계산
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        // X 방향은 목표의 포지션까지의 차등 비율로 속도를 계산
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth; // 수직 이동의 계산
        moveDirection.x = ratioX * speedX;

           
        // 중력만큼의 힘을 매 프레임에 추가
        moveDirection.y -= gravity * Time.deltaTime;

        // 이동 실행
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        // 이동 후 접지하고 있으면 Y 방향의 속도는 리셋한다
        if (controller.isGrounded) moveDirection.y = 0;

        // 속도가 0 이상이면 달리고 있는 플래그를 true로 한다
        animator.SetBool("run", moveDirection.z > 0.0f);        
    }

    // 왼쪽 차선으로 이동을 시작
    public void MoveToLeft ()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;       // 목표 차선변경
    }

    // 오른쪽 차선으로 이동을 시작
    public void MoveToRight ()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;       // 목표 차선변경 
    }

    public void Jump ()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;
            // 점프 트리거를 설정
            animator.SetTrigger("jump");
        }
    }

}
    

