using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class that contains the working logic of a rocket 
public class Rocket : MonoBehaviour
{
    [Header("Explosion Image")]
    public Sprite explosionSprite;
    public float spriteDuration = 0.333f; // Duration of the explosion image
    public float spriteScale = 0.2f; // Scale of the explosion

    [Header("Explosion Sound")]
    public AudioClip explosionSound;

    public LayerMask asteroidLayer;

    float speed;
    float aoeRadius;
    float aoeDamage;
    public float lifeTime = 5f;

    Rigidbody rb;
    AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // If not impacting with asteroids, automatically destroy the rocket after a while to conserve system resources
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(float spd, float radius, float aoe)
    {
        speed = spd;
        aoeRadius = radius;
        aoeDamage = aoe; 
    }

    // Calculate forward traveling trajectory 
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    // Handle collision with asteroid 
    void OnTriggerEnter(Collider other)
    {
        // Only explode when hitting an asteroid
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Asteroid")) != 0)
        {
            // Invoke a helper class RocketExplosion to handle damage calculation and effects 
            Vector3 impactPoint = transform.position;
            GameObject explosionObj = new GameObject("RocketExplosion");
            RocketExplosion explosion = explosionObj.AddComponent<RocketExplosion>();
            explosion.Initialize(impactPoint, aoeRadius, aoeDamage, explosionSound, explosionSprite, spriteDuration, spriteScale);
            Destroy(gameObject); // destroy the rocket itself
        }
    }
}
