using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

    public bool m_Populated;
    public bool m_Passable;
    public Vector3 m_WorldPosition;
    public int m_GridX;
    public int m_GridY;
    public int m_GCost;
    public int m_HCost;
    public Node m_Parent;

    int m_HeapIndex;

    public Node(bool _passable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        m_Passable = _passable;
        m_WorldPosition = _worldPosition;
        m_GridX = _gridX;
        m_GridY = _gridY;
    }

    public int m_FCost
    {
        get
        {
            return m_GCost + m_HCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return m_HeapIndex;
        }
        set
        {
            m_HeapIndex = value;
        }
    }

    public int CompareTo(Node _node)
    {
        int compareInt = m_FCost.CompareTo(_node.m_FCost);
        if (compareInt == 0)
        {
            compareInt = m_HCost.CompareTo(_node.m_HCost);
        }
        return -compareInt;
    }
}
