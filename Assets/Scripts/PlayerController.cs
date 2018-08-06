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
            m_Traveler.TryToMoveToDestination(m_Cursor.m_HighlightedNode.m_WorldPosition);
        }
    }
}
