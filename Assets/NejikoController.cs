using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    public float gravity;
    public float speedZ;
    public float speedJump;

    void Start()
    {
        // 필요한 컴포넌트를 자동 취득
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 지상에 있을 경우에만 조작한다.
        if (controller.isGrounded)
        {
            // Input을 감지하여 앞으로 전진한다.
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }

            // 방향 전환
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            // 점프
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump");
            }
        }
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
}
    

