using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Created by Hiep An Truong

// Hangar class to handle the weapon loadout system
public class Hangar : MonoBehaviour
{
    // Three dropdown menus that correspond to the 3 weapon slots of the player's ship 
    [Header("Dropdowns")]
    public TMP_Dropdown weaponSlot0Dropdown;
    public TMP_Dropdown weaponSlot1Dropdown;
    public TMP_Dropdown weaponSlot2Dropdown;
    public Button returnToMainMenu;

    void Start()
    {
        // Initialize dropdowns from saved values
        weaponSlot0Dropdown.value = PlayerPrefs.GetInt("WeaponSlot0", 0);
        weaponSlot1Dropdown.value = PlayerPrefs.GetInt("WeaponSlot1", 0);
        weaponSlot2Dropdown.value = PlayerPrefs.GetInt("WeaponSlot2", 0);

        // Add listerners for auto-save
        weaponSlot0Dropdown.onValueChanged.AddListener((val) => SaveWeaponSelection(0, val));
        weaponSlot1Dropdown.onValueChanged.AddListener((val) => SaveWeaponSelection(1, val));
        weaponSlot2Dropdown.onValueChanged.AddListener((val) => SaveWeaponSelection(2, val));
    }

    void SaveWeaponSelection(int slot, int value)
    {
        string key = $"WeaponSlot{slot}";
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
