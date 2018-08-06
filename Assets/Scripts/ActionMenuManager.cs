using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum EMenuState
{
    Main
}

public class ActionMenuManager : MonoBehaviour {

    private static Dictionary<string, ActionMenu> m_ActionMenu = new Dictionary<string, ActionMenu>();

    public static void Register(ActionMenu _actionMenu)
    {
        if (_actionMenu != null && !m_ActionMenu.ContainsKey(_actionMenu.name))
        {
            m_ActionMenu.Add(_actionMenu.name, _actionMenu);
        }
    }

    public static void Unregister(ActionMenu _actionMenu)
    {
        if (_actionMenu != null)
        {
            m_ActionMenu.Remove(_actionMenu.name);
        }
    }

    public static void Show(string _menuName)
    {
        ActionMenu result = null;
        if (m_ActionMenu.TryGetValue(_menuName, out result) && !result.gameObject.activeSelf)
        {
            result.Show();
        }
    }

    public static void Hide(string _menuName)
    {
        ActionMenu result = null;
        if (!m_ActionMenu.TryGetValue(_menuName, out result) && result.gameObject.activeSelf)
        {
            result.Hide();
        }
    }
}
