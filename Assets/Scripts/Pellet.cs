using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour {

    [SerializeField] int m_ScoreAwarded = 10;

    SpriteRenderer m_Renderer;
    CircleCollider2D m_Collider;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<CircleCollider2D>();
    }

    public int PickUp()
    {
        m_Renderer.enabled = false;
        m_Collider.enabled = false;
        return m_ScoreAwarded;
    }
}
