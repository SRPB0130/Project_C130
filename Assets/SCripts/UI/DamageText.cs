using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{
    public float floatSpeed = 2f; // 데미지 글씨 올라가는 속도
    public float duration = 1f; // 데미지 글씨 남아있는 시간

    private TextMeshPro text; // 연결할 데미지 글씨

     void Awake()
    {
        text = GetComponent<TextMeshPro>(); 
    }

    public void Show(int damage)
    {
        text.text = damage.ToString(); // 데미지를 숫자로 표시 
        Destroy(gameObject, duration); // 시간 지가면 오브젝트 파괴
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime; // 오브젝트가 위 방향으로 올라감 
        transform.LookAt(Camera.main.transform); // 카메라를 바라보도록 

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward); // 카메라와 동일한 방향을 바라보도록 
    }
}
