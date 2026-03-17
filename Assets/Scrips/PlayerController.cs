using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public JoystickController joystick; // Đảm bảo ô này đã được kéo Joystick_BG vào
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate() {
    if (joystick != null && joystick.inputVector != Vector2.zero) {
        Vector3 moveDir = new Vector3(joystick.inputVector.x, 0, joystick.inputVector.y);
        // Di chuyển bằng MovePosition là chuẩn nhất để chống dựt
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        rb.rotation = Quaternion.LookRotation(moveDir);
    }
}
    }
