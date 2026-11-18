// -----------------------------------------------------------------------------------
// 파일명       : FreeCamera.cs
// 설명         : 컨베이어 벨트 구동
// 작성자       : 이민하
// 작성일       : 2026-11-16
// -----------------------------------------------------------------------------------
// >> 주요 기능
// - 마우스 활용 카메라 조작 기능
// -----------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCamera : MonoBehaviour
{
    // 이동 속도 (m/s)
    public float moveSpeed = 3f;
    // 마우스 감도 (마우스 1픽셀당 회전 각도)
    public float Sensitivity = 0.15f;
    // 마우스 감도 (마우스 1픽셀당 판 이동)
    public float panSpeed = 0.001f;

    // 좌우 각도, 위 아래 각도
    private float yaw;
    private float pitch;
    
    // 마우스 클릭 Mode 구분
    enum CameraMode { None, Rotate, Move, Zoom }
    private CameraMode mode = CameraMode.None;

    void Awake()
    {
        // 카메라 각도를 yaw, pitch에 담기
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        // 0에서 360 사이 값을 -180에서 180 사이로 바꾸기 (350도 = -10도)
        if (pitch > 180f)
            pitch -= 360f;
    }

    void Update()
    {   
        // 버튼 상태 읽기
        bool left = Mouse.current.leftButton.isPressed;
        bool right = Mouse.current.rightButton.isPressed;

        // 줌 상태 읽기
        bool zoom = Mouse.current.scroll.IsActuated();
        Debug.Log(Mouse.current.scroll.IsActuated());

        // 마우스 오른쪽 버튼을 누르고 있을 때만 시점 돌리기
        if (!Mouse.current.leftButton.isPressed && Mouse.current.rightButton.isPressed)
        {
            cameraRotate();
        }

        if (Mouse.current.leftButton.isPressed && Mouse.current.rightButton.isPressed)
        {
            cameraMove();
        }


    }

    void cameraRotate()
    {
        // 마우스 이동량 읽기
        Vector2 delta = Mouse.current.delta.ReadValue();
        
        // yaw, pitch에 더하기 (pitch는 -89에서 89 사이로 제한)
        yaw += delta.x * Sensitivity;
        pitch -= delta.y * Sensitivity;
        Mathf.Clamp(pitch, -89f, 89f);

        // 카메라 회전에 적용
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void cameraMove()
    {
        // 마우스 이동량 읽기
        Vector2 delta = Mouse.current.delta.ReadValue();

        // 화면 기준 오른쪽, 윗쪽 방향으로 이동량 만들기
        Vector3 move = (transform.right * delta.x + transform.up * delta.y) * panSpeed;

        // 카메라 position에 적용
        transform.position -= move;
    }
}
