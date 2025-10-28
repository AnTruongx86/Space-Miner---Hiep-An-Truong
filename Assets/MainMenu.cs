using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Hiep An Truong

// MainMenu class to handle the main menu buttons 
public class MainMenu : MonoBehaviour
{
    public Button Play;
    public Button Hangar;

    public void PlayGame()
    {
        SceneManager.LoadScene("__Scene_0");
    }

    public void GoToHangar()
    {
        SceneManager.LoadScene("Hangar");
    }
}
