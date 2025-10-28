using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class to assign weapon prefabs to the player game object
[System.Serializable]
public class WeaponLoadout
{
    public GameObject machineGunPrefab;
    public GameObject laserPrefab;
    public GameObject rocketPrefab; 
}

// Class to handle player controls and associated interactions
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    Camera cam;
    Rigidbody rb;

    [Header("Explosion Image")]
    public Sprite explosionSprite;
    public float spriteDuration = 0.333f; // Duration of the explosion image
    public float spriteScale = 0.667f; // Scale of the explosion

    [Header("Audio")]
    public AudioClip explosionSound;
    private AudioSource audioSource;

    [Header("Weapon Mount Points")]
    public Transform[] weaponSlots;

    [Header("Weapon Prefabs")]
    public WeaponLoadout weaponPrefabs;

    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        ApplyWeaponLoadout(); 
    }

    void Update()
    {
        HandleAimingMovement();
    }

    void HandleAimingMovement()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;

        // Set z-distance from the camera to the player plane
        mousePos.z = Mathf.Abs(cam.transform.position.z - transform.position.z);

        // Convert to world space
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // Move toward the mouse position
        worldPos.z = transform.position.z;
        Vector3 newPos = Vector3.Lerp(transform.position, worldPos, moveSpeed * Time.deltaTime);

        // Clamp movement within camera bounds
        Vector3 clamped = ClampToScreen(newPos);

        rb.MovePosition(clamped);
    }

    // Method to ensure the player stay withitn the screen boundaries 
    Vector3 ClampToScreen(Vector3 pos)
    {
        // Get the bottom-left and top-right corners of the screen in world coordinates
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - transform.position.z)));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z - transform.position.z)));

        // Determine half width/height based on collider bounds
        float halfWidth = 0f;
        float halfHeight = 0f;

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Bounds b = col.bounds;
            halfWidth = b.extents.x;
            halfHeight = b.extents.y;
        }

        // Clamp X and Y within camera bounds
        float x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        float y = Mathf.Clamp(pos.y, min.y + halfHeight, max.y - halfHeight);

        // Keep Z constant
        return new Vector3(x, y, pos.z);
    }

    // Method to handle collision with asteroid
    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Asteroid")) != 0)
        {
            // Collide with asteroid -> game over
            GameManager.Instance.PlayerCrashed();
            Vector3 impactPoint = transform.position;

            // Create a PlayerExplosion game object to handle the explosion effects 
            GameObject explosionObj = new GameObject("PlayerExplosion");
            PlayerExplosion explosion = explosionObj.AddComponent<PlayerExplosion>();
            explosion.Initialize(impactPoint, explosionSound, explosionSprite, spriteDuration, spriteScale);

            Destroy(gameObject);
        }
    }

    // Method to apply the desired weapon loadout when the game starts 
    void ApplyWeaponLoadout()
    {
        int slot0 = PlayerPrefs.GetInt("WeaponSlot0", 1);
        int slot1 = PlayerPrefs.GetInt("WeaponSlot1", 0);
        int slot2 = PlayerPrefs.GetInt("WeaponSlot2", 0);

        ApplyWeaponToSlot(0, slot0);
        ApplyWeaponToSlot(1, slot1);
        ApplyWeaponToSlot(2, slot2); 
    }

    // Helper method to apply the correct type of weapon to a specific slot
    void ApplyWeaponToSlot(int index, int weaponChoice)
    {
        Transform slot = weaponSlots[index];
        GameObject prefab = null;
        switch (weaponChoice)
        {
            case 1: prefab = weaponPrefabs.machineGunPrefab; break;
            case 2: prefab = weaponPrefabs.laserPrefab; break;
            case 3: prefab = weaponPrefabs.rocketPrefab; break; 
        }

        if (prefab != null)
        {
            Instantiate(prefab, slot.position, slot.rotation, slot); 
        }
    }
}
