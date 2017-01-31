using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private Text m_scoreText;
    private int m_score;

    // Use this for initialization
    void Start()
    {
        m_scoreText = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        m_score += points;
        m_scoreText.text = m_score.ToString();
    }

    public void Reset()
    {
        m_score = 0;
        m_scoreText.text = m_score.ToString();
    }
}
