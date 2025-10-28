using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class that contains the working logic of machine gun's bullets 
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifeTime = 3f;

    Rigidbody rb;

    public void Initialize(float spd, int dmg)
    {
        speed = spd;
        damage = dmg;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // If not impacting with asteroids, automatically destroy the bullet after a while to conserve system resources
        Destroy(gameObject, lifeTime); 
    }

    // Calculate forward firing trajectory 
    void Start()
    {   
        rb.velocity = transform.up * speed; 
    }

    // Handle collision with asteroid 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Asteroid a = other.GetComponent<Asteroid>();
            a.TakeDamage(damage); // Cause the asteroid to take damage
            Destroy(gameObject); // Then destroy this bullet game object 
        }
    }
}
