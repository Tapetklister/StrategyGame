using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float m_RespawnDelay = 3.0f;
    [SerializeField] int m_Stock = 3;

    public int m_Score;
    
    CircleCollider2D m_Collider;
    SpriteRenderer m_SpriteRenderer;
    PlayerController m_Controller;
    Vector3 m_RespawnPoint;
    int m_CurrentStock;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<PlayerController>();
        m_RespawnPoint = transform.position;
        m_Collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Pickup"))
        {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            if (pickup.m_Type == EPickupType.PELLET)
            {
                float score;
                float dur;
                pickup.PickUp(out score, out dur);
                m_Score += (int)score;
                SendMessageUpwards("SetScoreText", m_Score);
            }
            else if (pickup.m_Type == EPickupType.SPEED)
            {
                float speedFactor;
                float dur;
                pickup.PickUp(out speedFactor, out dur);

                m_Controller.IncreaseMovementSpeedForDuration(speedFactor, dur);
            }
        }
    }

    void Die()
    {
        m_SpriteRenderer.enabled = false;
        m_Controller.enabled = false;
        m_Stock--;
        m_Collider.enabled = false;

        if (m_Stock <= 0)
        {
            SendMessageUpwards("ShowGameOverScreen");
        }
        else
        {
            Invoke("Respawn", m_RespawnDelay);
        }
    }

    void Respawn()
    {
        m_Collider.enabled = true;
        transform.position = m_RespawnPoint;
        m_SpriteRenderer.enabled = true;
        m_Controller.enabled = true;
        SendMessageUpwards("SetStockText", m_Stock);
        SendMessageUpwards("RespawnObjects");
    }
}
