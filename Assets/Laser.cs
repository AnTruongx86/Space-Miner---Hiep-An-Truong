using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Laser class that attached to the Laser prefab, contains the working logic 
public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserPrefab;
    public float fireRate = 1.0f;
    public float beamDuration = 0.333f;
    public float maxDistance = 20f;
    public int damage = 5;
    public LayerMask asteroidLayer;

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
        int level = PlayerPrefs.GetInt("LaserLevel", 1);
        damage *= level; 
    }

    // Calculating cooldown and fire
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            FireLaser();
            cooldown = fireRate; 
        }
    }

    // When FireLaser() is called, create a LaserBeam game object and plays a firing sound
    void FireLaser()
    {
        GameObject beam = Instantiate(laserPrefab, transform.position, transform.rotation);
        LaserBeam laser = beam.GetComponent<LaserBeam>();

        laser.Initialize(maxDistance, damage, beamDuration, asteroidLayer);
        audioSource.PlayOneShot(fireSound);
    }
}
