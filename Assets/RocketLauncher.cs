using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// RocketLauncher class that attached to RocketLauncher prefab, contains the working logic 
public class RocketLauncher : MonoBehaviour
{
    [Header("Rocket Settings")]
    public GameObject rocketPrefab;
    public float fireRate = 1.5f;
    public float speed = 15f;
    public float aoeRadius = 3f;
    public float aoeDamage = 20f;

    private float cooldown;

    [Header("Audio")]
    public AudioClip fireSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // When the game start, get the upgrade level to calculate damage 
    void Start()
    {
        int level = PlayerPrefs.GetInt("RocketLevel", 1);
        aoeDamage *= level; 
    }

    // Calculating cooldown and fire
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            FireRocket();
            cooldown = fireRate; 
        }
    }

    // When FireRocket() is called, create a rocket game object and plays a firing sound
    void FireRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
        Rocket rocketScript = rocket.GetComponent<Rocket>();
        rocketScript.Initialize(speed, aoeRadius, aoeDamage);
        audioSource.PlayOneShot(fireSound);
    }
}
