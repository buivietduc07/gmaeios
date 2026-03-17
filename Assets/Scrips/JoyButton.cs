using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background; // Kéo Joystick_BG vào đây
    public RectTransform handle;     // Kéo cái hình vuông trắng vào đây
    public Vector2 inputVector;      // Hướng để Player đọc

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
        {
            // Tính toán vị trí của hình vuông trắng
            pos.x = (pos.x / background.sizeDelta.x);
            pos.y = (pos.y / background.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Di chuyển hình vuông trắng theo tay
            handle.anchoredPosition = new Vector2(inputVector.x * (background.sizeDelta.x / 2), inputVector.y * (background.sizeDelta.y / 2));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero; // Thả tay ra là hình vuông về giữa
    }
}