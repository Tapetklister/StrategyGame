using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIMaster : MonoBehaviour {

    [SerializeField] Text m_ScoreText;
    [SerializeField] Text m_StockText;
    [SerializeField] GameObject m_GameOverPanel;

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
    }
}
