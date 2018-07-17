using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour {

    [SerializeField] float m_SpeedFactor = 1.4f;
    SpriteRenderer m_Renderer;
    CircleCollider2D m_Collider;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<CircleCollider2D>();
    }

    public float PickUp()
    {
        m_Renderer.enabled = false;
        m_Collider.enabled = false;
        return m_SpeedFactor;
    }
}
