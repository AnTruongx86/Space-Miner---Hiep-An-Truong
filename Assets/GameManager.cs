using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

// Created by Hiep An Truong

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI gameOverText;
    public Button returnToMainMenu; 

    bool isGameOver = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    // Hide the gameOverText and returnToMainMenu when the game starts
    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); 
        }
        if (returnToMainMenu != null)
        {
            returnToMainMenu.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            return; 
        }
    }

    // Method to call when the player crashed onto an asteroid 
    public void PlayerCrashed()
    {
        if (isGameOver)
        {
            return; 
        }
        isGameOver = true;

        // Show the gameOverText and returnToMainMenu
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER"; 
        }
        if (returnToMainMenu != null)
        {
            returnToMainMenu.gameObject.SetActive(true);
        }

        // Stop asteroid spawning
        var spawner = FindObjectOfType<AsteroidSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false; 
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
