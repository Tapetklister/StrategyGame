using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    [SerializeField] float m_Speed = 7.0f;

    private Vector2 m_Movement;

    private Vector2 m_OldMousePosition;
    private Vector2 m_ViewPos;
    private Vector2 m_ClampedWorldPos;

    void Update()
    {
        if (!TryTranslateByMouse())
        {
            TranslateByButtons();
        }
    }

    bool TryTranslateByMouse()
    {
        Vector2 mousePosition = Input.mousePosition;

        Camera cam = Camera.main;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));
        mousePosition = new Vector2(point.x, point.y);

        if (mousePosition != m_OldMousePosition)
        {
            transform.position = mousePosition;
            m_OldMousePosition = mousePosition;
            return true;
        }
        return false;
    }

    void TranslateByButtons()
    {
        m_Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (m_Movement != Vector2.zero)
        {
            transform.Translate(m_Movement * m_Speed * Time.deltaTime, Space.Self);
        }

        m_ViewPos = Camera.main.WorldToViewportPoint(transform.position);
        m_ClampedWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Clamp01(m_ViewPos.x), Mathf.Clamp01(m_ViewPos.y), 0));
        transform.position = m_ClampedWorldPos;
    }
}
