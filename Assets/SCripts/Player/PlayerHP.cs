using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100; // 최대 체력
    public int currentHP; 

    public Slider HPBar; // UI 체력바

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPUI(); // UI 업데이트
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount; // 체력 감소 
        currentHP = Mathf.Clamp(currentHP, 0 , maxHP); // 범위는 0 부터 최대 체력
        UpdateHPUI();

        if (currentHP < 0) // 체력이 0 밑으로 내려가면
        {
            Die(); // 죽음
        }

    }
    void Die()
    {
        // 게임 정지 후 종료 UI추가
    }

    public void Headl(int amount) 
    { 
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }
    void UpdateHPUI()
    {
        if(HPBar != null)
        {
            HPBar.value = (float)currentHP / maxHP; // 슬라이더 바를 0 와 1 사이 값으로 조정
        }
    }

}
