using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    [SerializeField] Transform m_Target;
    [SerializeField] float m_MovementSpeed = 2.0f;
    [SerializeField] float m_TargetSearchInterval = 1.0f;

    Vector3 m_StartingPosition;
    Vector3[] m_Path;
    int m_TargetIndex;

    Traveler m_Traveler;

    float m_Timer;

    private void Awake()
    {
        m_StartingPosition = transform.position;
        m_Timer = m_TargetSearchInterval;
        m_Traveler = GetComponent<Traveler>();
    }

    private void Update()
    {
        if (!m_Traveler.m_ActiveTurn || m_Traveler.m_Moving)
        {
            return;
        }
        m_Traveler.TryToMoveToDestination(m_Target.position);
    }
}
