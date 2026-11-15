// -----------------------------------------------------------------------------------
// 파일명       : ConveyorBelt.cs
// 설명         : 컨베이어 벨트 구동
// 작성자       : 이민하
// 작성일       : 2026-11-15
// -----------------------------------------------------------------------------------
// >> 주요 기능
// - 벨트 위 물체를 마찰로 이동
// - isRunning으로 구동과 정지 (나중에 PLC 모터 출력과 연결)
// -----------------------------------------------------------------------------------

using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    // 벨트 속도 (m/s)
    public float speed = 0.5f;
    // 구동 여부
    public bool isRunning = true;

    private Rigidbody rb;

    void Awake()
    {
        // 같은 오브젝트의 Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 정지 상태면 아무것도 하지 않기
        if (!isRunning)
            return;

        // 이번 계산 한 번에 움직일 거리 구하기
        Vector3 move = speed * Time.fixedDeltaTime * transform.right;

        // 판을 그만큼 뒤로 순간 이동
        rb.position -= move;

        // 판을 원래 자리까지 앞으로 이동
        rb.MovePosition(rb.position + move);
    }
}