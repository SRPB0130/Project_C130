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

    public int attackDamage = 30;  // ���� ������
    public float attackCooldown = 0.8f; // ���� ��Ÿ��
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ��Ŭ��
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0) && !isAttacking) // ��Ŭ�� �׸��� �������� �ƴҶ�
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

        // ������ ���
        int damage = attackDamage;

        Invoke(nameof(ApplyDamage), 0.4f); // �ִϸ��̼ǿ� �°� ����
        Invoke(nameof(ResumeMovement), 0.7f);
        Invoke(nameof(ResumeMovement), attackCooldown); // ������ �� �̵� ���

    }

    void ResumeMovement()
    {
        agent.isStopped = false;
        isAttacking = false;
    }
    void RotateToMouse() // ���콺 �������� ĳ���� ȸ�� 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; // ������θ� ȸ�� 

            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                transform.rotation = rotation;
            }
        }
    }
    void ApplyDamage() // ���� �� ������ ���� 
    {
        Vector3 attackOrigin = transform.position + transform.forward * 1.5f; // ���� ���� ���
        Collider[] hits = Physics.OverlapSphere(attackOrigin, 1f); // ���� ���� �� Ž��

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
