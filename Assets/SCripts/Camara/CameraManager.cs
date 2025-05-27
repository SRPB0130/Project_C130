using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMamager: MonoBehaviour
{
    public Transform target; // Ä³¸¯ÅÍ Transform
    public Vector3 offset = new Vector3(0.2f, 1.5f, -3.5f);
    public float rotationSpeed = 3f;

    float yaw = 0f;

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;

        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
