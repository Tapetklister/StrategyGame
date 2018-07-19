using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_MovementSpeed = 5.0f;

    Traveler m_Traveler;

    private void Awake()
    {
        m_Traveler = GetComponent<Traveler>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            m_Traveler.TryToMoveToDestination();
        }
    }
}
