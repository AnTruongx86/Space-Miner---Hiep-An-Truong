using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Created by Hiep An Truong

// Class to store and manager the player's score 
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText; 
    int score = 0;
    int highScore = 1000; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // A checkbox to allow resetting the high score 
    [Tooltip("Check this box to reset the high score in PlayerPrefs")]
    public bool resetHighScoreNow = false;
    private void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            PlayerPrefs.Save();
            Debug.LogWarning("HighScore resetted in PlayerPrefs");
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
        UpdateHighScoreText(); 
    }

    // Method to calculate score additions 
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // Update total cumulative score, to be spent in upgrades 
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        totalScore += amount;
        PlayerPrefs.SetInt("TotalScore", totalScore);

        // Update high score if necessary
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        PlayerPrefs.Save();
        UpdateHighScoreText(); 
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); 
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString(); 
        }
    }
}
