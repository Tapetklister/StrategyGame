using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] bool m_Active;
    [SerializeField] float m_ActionPoints;
    [SerializeField] float m_MovementCost;

    SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
}
