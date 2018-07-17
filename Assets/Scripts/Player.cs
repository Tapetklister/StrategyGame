using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float m_RespawnDelay = 3.0f;
    [SerializeField] int m_Stock = 3;

    public int m_Score;

    SpriteRenderer m_SpriteRenderer;
    PlayerController m_Controller;
    Vector3 m_RespawnPoint;
    int m_CurrentStock;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<PlayerController>();
        m_RespawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Pellet"))
        {
            Pellet pellet = collision.gameObject.GetComponent<Pellet>();
            m_Score += pellet.PickUp();
            SendMessageUpwards("SetScoreText", m_Score);
        }
    }

    void Die()
    {
        m_SpriteRenderer.enabled = false;
        m_Controller.enabled = false;
        m_Stock--;

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
        transform.position = m_RespawnPoint;
        m_SpriteRenderer.enabled = true;
        m_Controller.enabled = true;
        SendMessageUpwards("SetStockText", m_Stock);
    }
}
