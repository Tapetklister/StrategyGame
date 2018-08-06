using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    static Dictionary<string, List<Traveler>> m_Units = new Dictionary<string, List<Traveler>>();
    static Queue<string> m_TurnKey = new Queue<string>();
    static Queue<Traveler> m_TurnTeam = new Queue<Traveler>();
	
	void Update ()
    {
		if (m_TurnTeam.Count == 0)
        {
            InitTeamQueue();
        }
	}

    static void InitTeamQueue()
    {
        List<Traveler> teamList = m_Units[m_TurnKey.Peek()];

        foreach(Traveler unit in teamList)
        {
            m_TurnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    public static void StartTurn()
    {
        if (m_TurnTeam.Count > 0)
        {
            m_TurnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        Traveler unit = m_TurnTeam.Dequeue();
        unit.EndTurn();
        
        if (m_TurnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = m_TurnKey.Dequeue();
            m_TurnKey.Enqueue(team);
            InitTeamQueue();
        }
    }

    public static void AddUnit(Traveler _unit)
    {
        List<Traveler> list;

        if (!m_Units.ContainsKey(_unit.tag))
        {
            list = new List<Traveler>();
            m_Units[_unit.tag] = list;

            if (!m_TurnKey.Contains(_unit.tag))
            {
                m_TurnKey.Enqueue(_unit.tag);
            }
        }
        else
        {
            list = m_Units[_unit.tag];
        }

        list.Add(_unit);
    }
}
