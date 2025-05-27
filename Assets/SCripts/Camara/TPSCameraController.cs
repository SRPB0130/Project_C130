using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    public Transform target;                // 캐릭터
    public Vector3 offset = new Vector3(0, 2, -4);
    public float mouseSensitivity = 3f;
    public float minPitch = -30f;           // 아래로 최대 각도
    public float maxPitch = 60f;            // 위로 최대 각도

    private float yaw = 0f;                 // 좌우 회전
    private float pitch = 10f;              // 상하 회전 (초기 각도)

    void Update()
    {
        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 좌우 회전 (캐릭터 Y 방향)
        yaw += mouseX;

        // 상하 회전 (Pitch 제한 포함)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // 회전 적용
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f); // 캐릭터 머리 위 살짝 보게
    }
}
