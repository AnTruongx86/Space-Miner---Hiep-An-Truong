using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Created by Hiep An Truong

// A class that handle the weapon upgrade logic 
public class WeaponUpgradeManager : MonoBehaviour
{
    [Header("Weapon Upgrade UI")]
    public TextMeshProUGUI machineGunLevelText;
    public TextMeshProUGUI machineGunCostText;
    public Button machineGunUpgradeButton;

    public TextMeshProUGUI laserLevelText;
    public TextMeshProUGUI laserCostText;
    public Button laserUpgradeButton;

    public TextMeshProUGUI rocketLevelText;
    public TextMeshProUGUI rocketCostText;
    public Button rocketUpgradeButton;

    [Header("Player Stats UI")]
    public TextMeshProUGUI totalScoreText;

    [Header("Upgrade Config")]
    public int baseUpgradeCost = 100;
    public int costMultiplier = 2;

    [Tooltip("Check to reset all upgrade data in PlayerPrefs")]
    public bool resetUpgradeNow = false;

    private void OnDrawGizmos()
    {
        if (resetUpgradeNow)
        {
            resetUpgradeNow = false;
            PlayerPrefs.SetInt("MachineGunLevel", 1);
            PlayerPrefs.SetInt("LaserLevel", 1);
            PlayerPrefs.SetInt("RocketLevel", 1);
            PlayerPrefs.SetInt("TotalScore", 0);
            Debug.LogWarning("All upgrades and total score resetted to defaults"); 
        }
    }

    void Start()
    {
        machineGunUpgradeButton.onClick.AddListener(() => UpgradeWeapon("MachineGunLevel"));
        laserUpgradeButton.onClick.AddListener (() => UpgradeWeapon("LaserLevel"));
        rocketUpgradeButton.onClick.AddListener(() => UpgradeWeapon("RocketLevel"));
        UpdateUI();
    }

    // Method to upgrade a weapon 
    void UpgradeWeapon(string weaponKey)
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        int currentLevel = PlayerPrefs.GetInt(weaponKey, 1);

        int cost = baseUpgradeCost * currentLevel * costMultiplier;

        if (totalScore >= cost)
        {
            totalScore -= cost;
            PlayerPrefs.SetInt("TotalScore", totalScore);
            PlayerPrefs.SetInt(weaponKey, currentLevel + 1);
            PlayerPrefs.Save();
        }
        UpdateUI(); 
    }

    // Method to update the level and cost text when a weapon is upgraded 
    void UpdateUI()
    {
        int mgLevel = PlayerPrefs.GetInt("MachineGunLevel", 1);
        int laserLevel = PlayerPrefs.GetInt("LaserLevel", 1);
        int rocketLevel = PlayerPrefs.GetInt("RocketLevel", 1);
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        machineGunLevelText.text = $"Level: {mgLevel}";
        laserLevelText.text = $"Level: {laserLevel}";
        rocketLevelText.text = $"Level: {rocketLevel}";

        machineGunCostText.text = $"Cost: {baseUpgradeCost * mgLevel * costMultiplier}";
        laserCostText.text = $"Cost: {baseUpgradeCost * laserLevel * costMultiplier}";
        rocketCostText.text = $"Cost: {baseUpgradeCost * rocketLevel * costMultiplier}";

        totalScoreText.text = $"Total Points: {totalScore}";
    }
}
