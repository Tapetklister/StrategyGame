using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopulator : MonoBehaviour {

    [SerializeField] Pickup m_PelletPrefab;
    [SerializeField] NavigationGrid m_Grid;
    [SerializeField] GameObject[] m_Powerups;
    [SerializeField] Follow[] m_Enemies;

    List<GameObject> m_NonStaticObjects;
    Vector3[] m_StartingPositions;
    List<Pickup> m_Pellets;

    private void Start()
    {
        m_NonStaticObjects = new List<GameObject>();
        m_Pellets = new List<Pickup>();

        for (int i = 0; i < m_Powerups.Length; i++)
        {
            m_NonStaticObjects.Add(m_Powerups[i]);
        }

        for (int i = 0; i < m_Enemies.Length; i++)
        {
            m_NonStaticObjects.Add(m_Enemies[i].gameObject);
        }

        m_StartingPositions = new Vector3[m_NonStaticObjects.Count];

        for (int i = 0; i < m_NonStaticObjects.Count; i++)
        {
            m_StartingPositions[i] = m_NonStaticObjects[i].transform.position;
        }

        for (int x = 0; x < m_Grid.m_GridSizeX; x++)
        {
            for (int y = 0; y < m_Grid.m_GridSizeY; y++)
            {
                if (m_Grid.m_NavGrid[x, y].m_Passable)
                {
                    bool pelletShouldSpawn = true;

                    for (int i = 0; i < m_NonStaticObjects.Count; i++)
                    {
                        if (m_Grid.m_NavGrid[x, y].m_WorldPosition == m_NonStaticObjects[i].transform.position)
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

    void SpawnPellet(Vector3 _worldPosition)
    {
        Pickup pellet = (Pickup)Instantiate(m_PelletPrefab);
        pellet.transform.position = _worldPosition;
        m_Pellets.Add(pellet);
    }

    void RespawnObjects()
    {

        for (int i = 0; i < m_NonStaticObjects.Count; i++)
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

    void RespawnEnemies()
    {
        for (int i = 0; i < m_Enemies.Length; i++)
        {
            m_Enemies[i].Respawn();
        }
    }

    void RespawnPickups()
    {
        for (int i = 0; i < m_Powerups.Length; i++)
        {
            SpriteRenderer spriteRenderer = m_Powerups[i].GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = true;
        }
    }
}
