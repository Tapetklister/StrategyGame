using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    PathRequestSingleton m_PathRequestHandler;
    NavigationGrid m_Grid;

    void Awake()
    {
        m_PathRequestHandler = GetComponent<PathRequestSingleton>();
        m_Grid = GetComponent<NavigationGrid>();
    }

    public void StartFindPath(Vector3 _startPos, Vector3 _endPos)
    {
        StartCoroutine(FindPath(_startPos, _endPos));
    }

    public void StartFindReachableArea(Vector3 _startPos, float _range)
    {
        StartCoroutine(FindReachableArea(_startPos, _range));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 endPos)
    {
        Vector3[] nodePositions = new Vector3[0];
        bool pathFound = false;

        Node startNode = m_Grid.NodeFromWorldPoint(startPos);
        Node endNode = m_Grid.NodeFromWorldPoint(endPos);

        if (startNode.m_Passable && endNode.m_Passable)
        {
            Heap<Node> frontier = new Heap<Node>(m_Grid.NumberOfNodes);
            HashSet<Node> explored = new HashSet<Node>();
            frontier.AddItem(startNode);

            while (frontier.Count > 0)
            {
                Node node = frontier.FetchFirst();
                explored.Add(node);

                if (node == endNode)
                {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in m_Grid.GetNeighbours(node))
                {
                    if (!neighbour.m_Passable || explored.Contains(neighbour))
                        continue;

                    int movementCost = node.m_GCost + GetDistance(node, neighbour) + neighbour.m_MovementPenaly;

                    if (movementCost < neighbour.m_GCost || !frontier.ContainsItem(neighbour))
                    {
                        neighbour.m_GCost = movementCost;
                        neighbour.m_HCost = GetDistance(neighbour, endNode);
                        neighbour.m_Parent = node;

                        if (!frontier.ContainsItem(neighbour))
                        {
                            frontier.AddItem(neighbour);
                            frontier.UpdateItem(neighbour);
                        }

                    }
                }
            }
        }

        yield return null;

        if (pathFound)
        {
            nodePositions = RetracePath(startNode, endNode);
        }
        m_PathRequestHandler.FinishedSearchingForPath(nodePositions, pathFound);
    }

    IEnumerator FindReachableArea(Vector3 _startPos, float _range)
    {
        Vector3[] nodePositions = new Vector3[0];
        bool reachableAreaFound = false;

        Node startNode = m_Grid.NodeFromWorldPoint(_startPos);

        yield return null;
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node node = endNode;

        while (node != startNode)
        {
            path.Add(node);
            node = node.m_Parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> _path)
    {
        if (_path.Count <= 0)
        {
            return new Vector3[0];
        }

        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;
        waypoints.Add(_path[0].m_WorldPosition);
        
        for (int i = 1; i < _path.Count; i++)
        {
            Vector2 newDirection = new Vector2(_path[i - 1].m_GridX - _path[i].m_GridX, _path[i - 1].m_GridY - _path[i].m_GridY);
            if (newDirection != oldDirection)
            {
                waypoints.Add(_path[i-1].m_WorldPosition);
                waypoints.Add(_path[i].m_WorldPosition);
            }
            oldDirection = newDirection;
        }
        waypoints.Add(_path[_path.Count - 1].m_WorldPosition);
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.m_GridX - nodeB.m_GridX);
        int distanceY = Mathf.Abs(nodeB.m_GridY - nodeB.m_GridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    
}
