using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {

    [SerializeField] float m_SpriteChangeInterval;
    [SerializeField] Sprite[] m_Sprites;

    SpriteRenderer m_SpriteRenderer;
    float m_Timer;
    int m_SpriteIndex;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RunAnimation()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SpriteChangeInterval)
        {
            m_Timer -= m_SpriteChangeInterval;
            m_SpriteIndex++;
            if (m_SpriteIndex > m_Sprites.Length - 1)
            {
                m_SpriteIndex = 0;
            }
            m_SpriteRenderer.sprite = m_Sprites[m_SpriteIndex];
        }
    }

    public void SetSpriteColor(Color _color)
    {
        m_SpriteRenderer.color = _color;
    }
}
