using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // 상호작용 키
    public float interactionTime = 2f;         // 상호작용에 걸리는 시간
    public string interactionAnimation = "Interact"; // 애니메이션 트리거 이름

    public Slider progressSlider; // 상호작용 UI 연결
    public GameObject uiCanvas;   // UI 전체 그룹 

    private bool isPlayerInRange = false; 
    private bool isInteracting = false;
    private float timer = 0f;
    private Animator playerAnimator;

    private void Update()
    {
        if (isPlayerInRange && !isInteracting) // 캐릭터가 범위 안에 있고 상호작용이 아닐때
        {
            if (Input.GetKeyDown(interactionKey)) // 상호작용 키를 누르면
            {
                StartCoroutine(DoInteraction()); // 상호작용 코루틴 시작
            }
        }
    }

    IEnumerator DoInteraction() //상호작용 코루틴 
    {
        isInteracting = true; 
        timer = 0f;
        uiCanvas.SetActive(true);

        playerAnimator.SetTrigger(interactionAnimation); // 애니메이션 실행

        while (timer < interactionTime) 
        {
            timer += Time.deltaTime;
            progressSlider.value = timer / interactionTime; // 상호작용 ui 진행 
            yield return null;
        }

        uiCanvas.SetActive(false); // 상호작용 완료 후 ui 끄기 
        isInteracting = false;

        OnInteractionComplete(); // 상호작용 종료 
    }

    void OnInteractionComplete() // 상호작용 후 완료 처리
    {
        GameObject player = GameObject.FindWithTag("Player");   
        Debug.Log("상호작용 완료!");
        if (player != null)
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddItem(); // 아이템 1개 추가
            }
        }
        Destroy(gameObject); // 상호작용 후 아이템 제거 
    }

    void OnTriggerEnter(Collider other) // 상호작용 범위로 들어오면 
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit(Collider other) // 범위를 벗어나면 
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            uiCanvas.SetActive(false);
        }
    }

}
