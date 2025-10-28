using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// MachineGun class that attached to the MachineGun prefab, contains thw working logic
public class MachineGun : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float fireRate = 0.1f; 
    public float bulletSpeed = 20f;
    public int damage = 1;
    float cooldown;

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
        int level = PlayerPrefs.GetInt("MachineGunLevel", 1);
        damage *= level; 
    }

    // Calculating cooldown and fire
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            Fire();
            cooldown = fireRate; 
        }
    }

    // When Fire() is called, create a bullet game object and plays a shooting sound
    void Fire()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.Initialize(bulletSpeed, damage); 
        audioSource.PlayOneShot(fireSound);
    }
}
