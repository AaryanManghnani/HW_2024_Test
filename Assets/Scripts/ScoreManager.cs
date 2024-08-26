using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager state;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        if (state == null)
        {
            state = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }
}
