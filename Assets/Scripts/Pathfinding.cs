using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    [SerializeField] Transform m_Seeker;
    [SerializeField] Transform m_Target;

    NavigationGrid m_Grid;

    void Awake()
    {
        m_Grid = GetComponent<NavigationGrid>();
    }
	
	void Update ()
    {
        FindPath(m_Seeker.position, m_Target.position);
	}

    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = m_Grid.NodeFromWorldPoint(startPos);
        Node endNode = m_Grid.NodeFromWorldPoint(endPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].m_FCost < node.m_FCost || openSet[i].m_FCost == node.m_FCost)
                {
                    if (openSet[i].m_HCost < node.m_HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in m_Grid.GetNeighbours(node))
            {
                if (!neighbour.m_Passable || closedSet.Contains(neighbour))
                    continue;

                int movementCost = node.m_GCost + GetDistance(node, neighbour);

                if (movementCost < neighbour.m_GCost || !openSet.Contains(neighbour))
                {
                    neighbour.m_GCost = movementCost;
                    neighbour.m_HCost = GetDistance(neighbour, endNode);
                    neighbour.m_Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node node = endNode;

        while (node != startNode)
        {
            path.Add(node);
            node = node.m_Parent;
        }

        path.Reverse();
        m_Grid.m_Path = path;
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
