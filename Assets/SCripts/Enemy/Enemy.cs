using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth = 100; // 최대 체력
    public int currentHealth; // 현재 체력

    public GameObject damageTextPrefab;


    private void Start()
    {
        currentHealth = MaxHealth; // 게임 실행시 현재 체력을 최대 체력으로 조정
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // 데미지 텍스트 출력
        if (damageTextPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * 2f;
            GameObject text = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
            text.GetComponent<DamageText>().Show(damage); 
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
