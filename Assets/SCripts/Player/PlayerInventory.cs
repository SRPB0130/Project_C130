using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int itemCount = 0;
    public Text itemCountText; // UI 연결

    private void Start()
    {
        UpdateItemUI(); // 게임 시작시 아이템UI 초기화
    }

    public void AddItem(int amount = 1)
    {
        itemCount += amount;
        UpdateItemUI(); // 아이템 먹을때마다 ui 업데이트
    }

    void UpdateItemUI()
    {
        if (itemCountText != null)
            itemCountText.text = "x " + itemCount.ToString(); // 아이템 x 0 으로 표현됨
    }
}
