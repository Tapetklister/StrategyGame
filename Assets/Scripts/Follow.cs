using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    [SerializeField] Transform m_Target;
    [SerializeField] float m_MovementSpeed = 2.0f;
    [SerializeField] float m_TargetSearchInterval = 1.0f;

    public bool m_IsActive = true;

    Vector3 m_StartingPosition;
    Vector3[] m_Path;
    int m_TargetIndex;

    float m_Timer;

    private void Awake()
    {
        m_StartingPosition = transform.position;
        m_Timer = m_TargetSearchInterval;
    }

    private void Update()
    {
        if (!m_IsActive)
            return;

        m_Timer += Time.deltaTime;

        if (m_Timer >= m_TargetSearchInterval)
        {
            m_Timer -= m_TargetSearchInterval;
            PathRequestSingleton.RequestPath(transform.position, m_Target.position, OnPathFound);
        }
    }

    public void Respawn()
    {
        transform.position = m_StartingPosition;
        m_IsActive = true;
    }

    public void OnPathFound(Vector3[] _newPath, bool _pathFound)
    {
        if (_pathFound)
        {
            m_Path = _newPath;
            m_TargetIndex = 0;
            StopCoroutine("MoveAlongPath");
            StartCoroutine("MoveAlongPath");
        }
    }

    IEnumerator MoveAlongPath()
    {
        Vector3 currentWaypoint = m_Path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                m_TargetIndex++;
                if (m_TargetIndex >= m_Path.Length)
                {
                    yield break;
                }
                currentWaypoint = m_Path[m_TargetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, m_MovementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_IsActive = false;
    }
}
