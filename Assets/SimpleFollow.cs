using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;
    public GameObject target;
    public float followSpeed;
    
    void Start()
    {
        diff = target.transform.position - transform.position; // 추적거리 계산
    }

    
    void LateUpdate()
    {
        // 선형 보간 함수(Lerp)에 의한 유연한 움직임 구현
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed
            );
    }
}
