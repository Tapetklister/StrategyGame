using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMaster : MonoBehaviour {

    [SerializeField] Text m_ScoreText;
    [SerializeField] Text m_StockText;
    [SerializeField] Text m_GameOverScoreText;
    [SerializeField] GameObject m_GameOverPanel;

    private void Update()
    {
        if (m_GameOverPanel.active && Input.GetButtonDown("Jump"))
            SceneManager.LoadScene(0);
    }

    void SetStockText(int _stock)
    {
        m_StockText.text = "x " + _stock.ToString();
    }

    void SetScoreText(int _score)
    {
        m_ScoreText.text = "x " + _score.ToString();
    }

    void ShowGameOverScreen()
    {
        m_GameOverPanel.SetActive(true);
        m_GameOverScoreText.text = m_ScoreText.text;
    }
}
