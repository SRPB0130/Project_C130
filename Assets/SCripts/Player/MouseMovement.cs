using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera cam;
    private Animator animator;

    private bool isAttacking = false;

    public int attackDamage = 30;  // 공격 데미지
    public float attackCooldown = 0.8f; // 공격 쿨타임
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0) && !isAttacking) // 좌클릭 그리고 공격중이 아닐때
        {
            RotateToMouse();
            Attack();
        }

        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Horizontal", localVelocity.x);
        animator.SetFloat("Vertical", localVelocity.z);
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        agent.isStopped = true;

        // 데미지 계산
        int damage = attackDamage;

        Invoke(nameof(ApplyDamage), 0.4f); // 애니메이션에 맞게 조절
        Invoke(nameof(ResumeMovement), 0.7f);
        Invoke(nameof(ResumeMovement), attackCooldown); // 딜레이 후 이동 허용

    }

    void ResumeMovement()
    {
        agent.isStopped = false;
        isAttacking = false;
    }
    void RotateToMouse() // 마우스 방향으로 캐릭터 회전 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; // 평면으로만 회전 

            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                transform.rotation = rotation;
            }
        }
    }
    void ApplyDamage() // 공격 시 데미지 적용 
    {
        Vector3 attackOrigin = transform.position + transform.forward * 1.5f; // 공격 범위 계산
        Collider[] hits = Physics.OverlapSphere(attackOrigin, 1f); // 공격 범위 적 탐색

        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
