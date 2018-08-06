using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] TileCursor m_Cursor;

    Traveler m_Traveler;

    private void Awake()
    {
        m_Traveler = GetComponent<Traveler>();
    }

    void Update()
    {
        if (!m_Traveler.m_ActiveTurn)
        {
            return;
        }

        if (Input.GetButtonDown("Select"))
        {
            if (m_Cursor.m_HighlightedNode.m_WorldPosition == transform.position)
            {
                ActionMenuManager.Show("ActionMenu");
            }

            m_Traveler.TryToMoveToDestination(m_Cursor.m_HighlightedNode.m_WorldPosition);
        }
    }
}
