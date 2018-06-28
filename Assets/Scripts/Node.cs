using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool m_Passable;
    public Vector3 m_WorldPosition;
    public int m_GridX;
    public int m_GridY;
    public int m_GCost;
    public int m_HCost;
    public Node m_Parent;

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
}
