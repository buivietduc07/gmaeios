using UnityEngine;

public class Camera360 : MonoBehaviour
{
    public Transform target; // Kéo Player vào đây
    public Vector3 offset = new Vector3(0.8f, 2.5f, -5f); 
    public float sensitivity = 0.5f; 
    public float smoothSpeed = 15f;

    private float rotX = 0f;
    private float rotY = 0f;

    void Start()
    {
        // Lấy góc xoay hiện tại để bắt đầu mượt mà
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.y;
        rotY = rot.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. XỬ LÝ TRÊN PC (GIỮ CHUỘT TRÁI)
        if (Input.GetMouseButton(0))
        {
            // Chỉ xoay khi chuột ở nửa phải màn hình (tránh vướng Joystick)
            if (Input.mousePosition.x > Screen.width / 2)
            {
                rotX += Input.GetAxis("Mouse X") * sensitivity * 20f;
                rotY -= Input.GetAxis("Mouse Y") * sensitivity * 20f;
            }
        }

        // 2. XỬ LÝ TRÊN MOBILE (CẢM ỨNG)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2) // Nửa màn hình phải
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    rotX += touch.deltaPosition.x * sensitivity;
                    rotY -= touch.deltaPosition.y * sensitivity;
                }
            }
        }

        // Giới hạn góc nhìn lên/xuống để không bị lật camera
        rotY = Mathf.Clamp(rotY, -30f, 60f);

        // 3. CẬP NHẬT VỊ TRÍ VÀ XOAY
        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
        Vector3 desiredPosition = target.position + (rotation * offset);

        transform.rotation = rotation;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}