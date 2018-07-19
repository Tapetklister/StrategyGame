using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCursor : MonoBehaviour {

    [SerializeField] NavigationGrid m_Grid;

    Vector2 m_OldMousePosition;

    Node m_HighlightedNode;

	void Update ()
    {
        if (!TryTranslateByMouse())
        {
            TranslateByButtons();
        }
    }

    bool TryTranslateByMouse()
    {
        Vector2 mousePosition = Input.mousePosition;

        Camera cam = Camera.main;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));
        mousePosition = new Vector2(point.x, point.y);

        if (mousePosition != m_OldMousePosition)
        {
            m_HighlightedNode = m_Grid.NodeFromWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0.0f));
            m_OldMousePosition = mousePosition;
            return true;
        }
        return false;
    }

    void TranslateByButtons()
    {
        int movementX = 0;
        int movementY = 0;

        if (Input.GetKeyDown("d"))
        {
            movementX = 1;
        }

        if (Input.GetKeyDown("a"))
        {
            movementX = -1;
        }

        if (Input.GetKeyDown("w"))
        {
            movementY = 1; ;
        }

        if (Input.GetKeyDown("s"))
        {
            movementY = -1;
        }

        if (movementX != 0 || movementY != 0)
        {
            if ((m_HighlightedNode.m_GridX <= 0 && movementX < 0) || m_HighlightedNode.m_GridX >= m_Grid.m_GridSizeX - 1 && movementX > 0)
            {
                movementX = 0;
            }

            if ((m_HighlightedNode.m_GridY <= 0 && movementY < 0) || m_HighlightedNode.m_GridY >= m_Grid.m_GridSizeY - 1 && movementY > 0)
            {
                movementY = 0;
            }

            m_HighlightedNode = m_Grid.m_NavGrid[m_HighlightedNode.m_GridX + movementX, m_HighlightedNode.m_GridY + movementY];
        }
        
    }

    private void OnDrawGizmos()
    {
        if (m_HighlightedNode != null)
        {
            Gizmos.color = (m_HighlightedNode.m_Passable) ? Color.green : Color.red;
            Gizmos.DrawCube(m_HighlightedNode.m_WorldPosition, new Vector3(1.0f, 1, 1.0f));
        }
        
    }
}
