using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestSingleton : MonoBehaviour {

    static PathRequestSingleton m_Instance;

    Queue<PathRequest> m_PathRequestQueue = new Queue<PathRequest>();
    PathRequest m_CurrentRequest;
    Pathfinding m_Pathfinding;
    bool m_IsSearchingForPath;

    Queue<ReachableAreaRequest> m_ReachableAreaRequestQueue = new Queue<ReachableAreaRequest>();
    ReachableAreaRequest m_CurrentReachableAreaRequest;
    bool m_IsSearchingForReachableArea;

    private void Awake()
    {
        m_Instance = this;
        m_Pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 _startPos, Vector3 _endPos, Action<Vector3[], bool> _callback)
    {
        PathRequest request = new PathRequest(_startPos, _endPos, _callback);
        m_Instance.m_PathRequestQueue.Enqueue(request);
        m_Instance.TryFindNextPath();
    }

    public static void RequestReachableArea(Vector3 _startPos, float _range, Action<Vector3[], bool> _callback)
    {
        ReachableAreaRequest request = new ReachableAreaRequest(_startPos, _range, _callback);
        m_Instance.m_ReachableAreaRequestQueue.Enqueue(request);
        m_Instance.TryDefineReachableArea();
    }

    public void FinishedSearchingForPath(Vector3[] _path, bool _success)
    {
        m_CurrentRequest.m_Callback(_path, _success);
        m_IsSearchingForPath = false;
        TryFindNextPath();
    }

    void TryFindNextPath()
    {
        if (!m_IsSearchingForPath && m_PathRequestQueue.Count > 0)
        {
            m_CurrentRequest = m_PathRequestQueue.Dequeue();
            m_IsSearchingForPath = true;
            m_Pathfinding.StartFindPath(m_CurrentRequest.m_StartPos, m_CurrentRequest.m_EndPos);
        }
    }

    void TryDefineReachableArea()
    {
        if (!m_IsSearchingForReachableArea && m_ReachableAreaRequestQueue.Count > 0)
        {
            m_CurrentReachableAreaRequest = m_ReachableAreaRequestQueue.Dequeue();
            m_IsSearchingForReachableArea = true;
            m_Pathfinding.StartFindReachableArea(m_CurrentReachableAreaRequest.m_StartPos, m_CurrentReachableAreaRequest.m_Range);
        }
    }

    struct PathRequest
    {
        public Vector3 m_StartPos;
        public Vector3 m_EndPos;
        public Action<Vector3[], bool> m_Callback;

        public PathRequest(Vector3 _startPos, Vector3 _endPos, Action<Vector3[], bool> _callback)
        {
            m_StartPos = _startPos;
            m_EndPos = _endPos;
            m_Callback = _callback;
        }
    }

    struct ReachableAreaRequest
    {
        public Vector3 m_StartPos;
        public float m_Range;
        public Action<Vector3[], bool> m_Callback;

        public ReachableAreaRequest(Vector3 _startPos, float _range, Action<Vector3[], bool> _callback)
        {
            m_StartPos = _startPos;
            m_Range = _range;
            m_Callback = _callback;
        }
    }
}
