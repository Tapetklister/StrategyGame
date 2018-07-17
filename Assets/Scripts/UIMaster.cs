using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIMaster : MonoBehaviour {

    [SerializeField] Text m_ScoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetScoreText(int _score)
    {
        m_ScoreText.text = _score.ToString();
    }
}
