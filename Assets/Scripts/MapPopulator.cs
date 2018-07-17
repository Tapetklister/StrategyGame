﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopulator : MonoBehaviour {

    [SerializeField] Pellet m_Pellet;
    [SerializeField] NavigationGrid m_Grid;
    [SerializeField] Transform[] m_NonStaticObjects;

    Vector3[] m_StartingPositions;
    Pellet[] m_Pellets;
    int m_PelletIndex;

    private void Start()
    {
        m_StartingPositions = new Vector3[m_NonStaticObjects.Length];

        for (int i = 0; i < m_NonStaticObjects.Length; i++)
        {
            m_StartingPositions[i] = m_NonStaticObjects[i].transform.position;
        }

        m_Pellets = new Pellet[m_Grid.m_GridSizeX * m_Grid.m_GridSizeY - m_NonStaticObjects.Length];

        for (int x = 0; x < m_Grid.m_GridSizeX; x++)
        {
            for (int y = 0; y < m_Grid.m_GridSizeY; y++)
            {
                if (m_Grid.m_NavGrid[x, y].m_Passable)
                {
                    bool pelletShouldSpawn = true;

                    for (int i = 0; i < m_NonStaticObjects.Length; i++)
                    {
                        if (m_Grid.m_NavGrid[x, y].m_WorldPosition == m_NonStaticObjects[i].position)
                        {
                            pelletShouldSpawn = false;
                        }
                    }
                    if (pelletShouldSpawn)
                        SpawnPellet(m_Grid.m_NavGrid[x, y].m_WorldPosition);
                }
            }
        }

    }

    void SpawnPellet(Vector3 worldPosition)
    {
        m_Pellets[m_PelletIndex] = (Pellet)Instantiate(m_Pellet);
        m_Pellets[m_PelletIndex].transform.position = worldPosition;
        m_PelletIndex++;
    }

    void RespawnObjects()
    {
        for (int i = 0; i < m_NonStaticObjects.Length; i++)
        {
            m_NonStaticObjects[i].gameObject.SetActive(true);

            Follow follow = m_NonStaticObjects[i].GetComponent<Follow>();

            if (follow != null)
            {
                follow.Respawn();
            }

            m_NonStaticObjects[i].transform.position = m_StartingPositions[i];
        }
    }
}
