using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPickupType
{
    PELLET,
    SPEED
}

public class Pickup : MonoBehaviour {

    [SerializeField] float m_Value = 1.0f;
    [SerializeField] float m_Duration;

    public EPickupType m_Type;

    SpriteRenderer m_Renderer;
    CircleCollider2D m_Collider;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<CircleCollider2D>();
    }

    public void PickUp(out float _value, out float _duration)
    {
        m_Renderer.enabled = false;
        m_Collider.enabled = false;
        _value = m_Value;
        _duration = m_Duration;
    }
}
