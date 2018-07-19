using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ECursorMode
{
    Free,
    Tile
}

public class TileCursor : MonoBehaviour {

    [SerializeField] NavigationGrid m_Grid;
    [SerializeField] ECursorMode m_Mode;
    [SerializeField] float m_Speed = 3.0f;

    Vector2 m_OldMousePosition;

    public Node m_HighlightedNode;

	void Update ()
    {
        switch (m_Mode)
        {
            case ECursorMode.Free:

                if (!TryTranslateByMouseFree())
                {
                    TranslateByButtonsFree();
                }

                break;
            case ECursorMode.Tile:

                if (!TryTranslateByMouseTile())
                {
                    TranslateByButtonsTile();
                }

                break;
            default:
                break;
        }
    }

    bool TryTranslateByMouseTile()
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

    void TranslateByButtonsTile()
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

    bool TryTranslateByMouseFree()
    {
        Vector2 mousePosition = Input.mousePosition;

        Camera cam = Camera.main;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));
        mousePosition = new Vector2(point.x, point.y);

        if (mousePosition != m_OldMousePosition)
        {
            transform.position = mousePosition;
            m_OldMousePosition = mousePosition;
            return true;
        }
        return false;
    }

    void TranslateByButtonsFree()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movement != Vector2.zero)
        {
            transform.Translate(movement * m_Speed * Time.deltaTime, Space.Self);
        }

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 clampedWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Clamp01(viewPos.x), Mathf.Clamp01(viewPos.y), 0));
        transform.position = clampedWorldPos;
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
