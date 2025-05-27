using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 3f;

    [Header("점프 및 중력")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;          // y축 속도 (중력, 점프)
    private bool isGrounded;           // 바닥에 있는지 여부

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 마우스로 캐릭터 회전 (좌우만)
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * mouseSensitivity, 0);

        // 2. 이동 입력 (캐릭터 로컬 방향 기준)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * h + transform.forward * v;

        // 3. 지면 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 지면에 붙이기
        }

        // 4. 점프 입력
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 5. 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // 6. 최종 이동 적용
        Vector3 finalMove = moveDir * moveSpeed + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);

        // 7. 애니메이션 연동
        animator.SetFloat("Speed", moveDir.magnitude);
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
        animator.SetBool("IsGrounded", isGrounded);
    }
}