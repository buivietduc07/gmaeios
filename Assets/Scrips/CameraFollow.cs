using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // Chỉnh Z thấp xuống (ví dụ -10) để nhân vật bé lại
    public Vector3 offset = new Vector3(0.8f, 3.5f, -10f); 
    public float smoothSpeed = 15f;

    void FixedUpdate()
    {
        if (target == null) return;

        // Vị trí mục tiêu xa hơn để nhân vật trông nhỏ lại
        Vector3 desiredPosition = target.position + offset;
        
        // Di chuyển mượt mà
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        
        // Luôn nhìn vào giữa thân nhân vật
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}