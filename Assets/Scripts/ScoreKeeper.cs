using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int m_score;

    private Text m_scoreText;

    // Use this for initialization
    void Start()
    {
        m_scoreText = GetComponent<Text>();
        Reset();
        m_scoreText.text = m_score.ToString();
    }

    public void Score(int points)
    {
        m_score += points;
        m_scoreText.text = m_score.ToString();
    }

    public static void Reset()
    {
        m_score = 0;
    }
}
