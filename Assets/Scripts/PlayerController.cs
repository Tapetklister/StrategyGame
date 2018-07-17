using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_MovementSpeed = 5.0f;

    float m_OrigMovementSpeed;
    Vector2 m_Movement;
    SpriteAnimator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<SpriteAnimator>();
        m_OrigMovementSpeed = m_MovementSpeed;
    }

    public void IncreaseMovementSpeedForDuration(float _speedFactor, float _duration)
    {
        m_MovementSpeed *= _speedFactor;
        m_Animator.SetSpriteColor(Color.yellow);
        Invoke("ResetMovementSpeed", _duration);
    }

    private void FixedUpdate()
    {
        transform.Translate(
            m_Movement.x * m_MovementSpeed * Time.deltaTime,
            m_Movement.y * m_MovementSpeed * Time.deltaTime,
            0.0f
        );

        if (m_Movement != Vector2.zero)
        {
            m_Animator.RunAnimation();
        }
    }

    void Update()
    {
        m_Movement = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
    }

    void ResetMovementSpeed()
    {
        m_Animator.SetSpriteColor(Color.white);
        m_MovementSpeed = m_OrigMovementSpeed;
    }
}
