using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCamera : MonoBehaviour
{
    // 이동 속도 (m/s)
    public float moveSpeed = 3f;
    // 마우스 감도 (마우스 1픽셀당 회전 각도)
    public float Sensitivity = 0.15f;

    // 좌우 각도, 위 아래 각도
    private float yaw;
    private float pitch;
    private float mouseYaw;
    private float mousePitch;

    void Awake()
    {
        // 지금 카메라 각도를 yaw, pitch에 담기
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        // 0에서 360 사이 값을 -180에서 180 사이로 바꾸기 (350도 = -10도)
        if (pitch > 180f)
            pitch -= 360f;
    }

    void Update()
    {
        // 마우스 오른쪽 버튼을 누르고 있을 때만 시점 돌리기
        if (Mouse.current.rightButton.isPressed)
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
    }
}
