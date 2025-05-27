using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public float detectRange = 5f;  // 추격 범위
    public float Stopdistance = 0.5f; // 추격 대상과 최소 거리
    public Transform target; // 추격 대상
    public float ChaseSpeed = 2.0f;

    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = ChaseSpeed;
        if(agent == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player"); // 찾는 대상의 태그 
            if(playerObj != null )
            {
                target = playerObj.transform;
            }
        }
        agent.stoppingDistance = Stopdistance;
    }

    private void Update()
    {
        if (target == null) return;
         float distanse =Vector3.Distance(transform.position, target.position);

        if (distanse < detectRange)
        {
            agent.isStopped = false;

            if (distanse > Stopdistance)
            {
                agent.SetDestination(target.position); // 묙표 위치까지 이동
            }
            else
            {
                agent.ResetPath(); // 다시 탐색
            }
        }
        else
        {
            agent.isStopped = true;
        }

    }
    private void OnDrawGizmosSelected() // scene 창에 범위 표시 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
