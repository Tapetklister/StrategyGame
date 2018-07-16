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
}
