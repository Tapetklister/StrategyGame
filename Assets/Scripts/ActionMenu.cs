using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour {

    private void Awake()
    {
        ActionMenuManager.Register(this);
        Hide();
    }

    private void OnDestroy()
    {
        ActionMenuManager.Unregister(this);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
