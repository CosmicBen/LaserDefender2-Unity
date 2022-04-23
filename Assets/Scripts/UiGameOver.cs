using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        if (scoreKeeper)
        {
            scoreText.text = "You Scored\n" + scoreKeeper.GetScore().ToString("000000000");
        }
        else
        {
            scoreText.text = "You Scored\n" + "000000000";
        }
    }
}
