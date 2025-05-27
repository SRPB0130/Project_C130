using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamara : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    public Vector3 offset = new Vector3(0, 10, 0); // 머리 위에서 수직
    public float tiltAngle = 75f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset; 

        transform.rotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }
}
