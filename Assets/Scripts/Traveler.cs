using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour {

    [SerializeField] TileCursor m_Cursor;
    [SerializeField] float m_MovementSpeed = 2.0f;
    [SerializeField] float m_MovementRange = 50.0f;

    Vector3[] m_ReachableArea;
    Vector3[] m_Path;
    int m_TargetIndex;

    private void Update()
    {
        PathRequestSingleton.RequestReachableArea(transform.position, m_MovementRange, OnReachableAreaFound);
    }

    public void TryToMoveToDestination()
    {
        PathRequestSingleton.RequestPath(transform.position, m_Cursor.m_HighlightedNode.m_WorldPosition, OnPathFound);
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

    void OnReachableAreaFound(Vector3[] _reachableArea, bool _reachableAreaFound)
    {
        if (_reachableAreaFound)
        {
            m_ReachableArea = _reachableArea;
        }
    }

    IEnumerator MoveAlongPath()
    {
        if (m_Path.Length > 0)
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
    }

    public void OnDrawGizmos()
    {
        if (m_Path != null)
        {
            for (int i = m_TargetIndex; i < m_Path.Length; i++)
            {
                Gizmos.color = new Color(0.0f, 0.0f, 0.8f, 0.5f);
                Gizmos.DrawCube(m_Path[i], Vector3.one);
            }
        }

        if (m_ReachableArea != null)
        {
            for ( int i = 0; i < m_ReachableArea.Length; i++)
            {
                Gizmos.color = new Color(0.8f, 0.0f, 0.8f, 0.8f);
                Gizmos.DrawCube(m_ReachableArea[i], Vector3.one);
            }
        }
    }
}
