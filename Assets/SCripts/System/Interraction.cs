using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // ��ȣ�ۿ� Ű
    public float interactionTime = 2f;         // ��ȣ�ۿ뿡 �ɸ��� �ð�
    public string interactionAnimation = "Interact"; // �ִϸ��̼� Ʈ���� �̸�

    public Slider progressSlider; // ��ȣ�ۿ� UI ����
    public GameObject uiCanvas;   // UI ��ü �׷� 

    private bool isPlayerInRange = false; 
    private bool isInteracting = false;
    private float timer = 0f;
    private Animator playerAnimator;

    private void Update()
    {
        if (isPlayerInRange && !isInteracting) // ĳ���Ͱ� ���� �ȿ� �ְ� ��ȣ�ۿ��� �ƴҶ�
        {
            if (Input.GetKeyDown(interactionKey)) // ��ȣ�ۿ� Ű�� ������
            {
                StartCoroutine(DoInteraction()); // ��ȣ�ۿ� �ڷ�ƾ ����
            }
        }
    }

    IEnumerator DoInteraction() //��ȣ�ۿ� �ڷ�ƾ 
    {
        isInteracting = true; 
        timer = 0f;
        uiCanvas.SetActive(true);

        playerAnimator.SetTrigger(interactionAnimation); // �ִϸ��̼� ����

        while (timer < interactionTime) 
        {
            timer += Time.deltaTime;
            progressSlider.value = timer / interactionTime; // ��ȣ�ۿ� ui ���� 
            yield return null;
        }

        uiCanvas.SetActive(false); // ��ȣ�ۿ� �Ϸ� �� ui ���� 
        isInteracting = false;

        OnInteractionComplete(); // ��ȣ�ۿ� ���� 
    }

    void OnInteractionComplete() // ��ȣ�ۿ� �� �Ϸ� ó��
    {
        GameObject player = GameObject.FindWithTag("Player");   
        Debug.Log("��ȣ�ۿ� �Ϸ�!");
        if (player != null)
        {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddItem(); // ������ 1�� �߰�
            }
        }
        Destroy(gameObject); // ��ȣ�ۿ� �� ������ ���� 
    }

    void OnTriggerEnter(Collider other) // ��ȣ�ۿ� ������ ������ 
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit(Collider other) // ������ ����� 
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            uiCanvas.SetActive(false);
        }
    }

}
