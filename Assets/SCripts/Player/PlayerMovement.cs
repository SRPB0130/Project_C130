using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D, ←/→
        float vertical = Input.GetAxis("Vertical");     // W/S, ↑/↓

        Vector3 move = new Vector3(horizontal, 0, vertical);
        float speed = move.magnitude;

        // 실제 이동
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // 애니메이션 파라미터 설정
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.z);
        animator.SetFloat("Speed", speed);
    }
}


