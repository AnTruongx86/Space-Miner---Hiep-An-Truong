using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

// Created by Hiep An Truong

// Class that contains the characteristics of asteroids 
public class Asteroid : MonoBehaviour
{
    public int hp;
    public TextMeshProUGUI HPText; 

    Rigidbody rb;

    // A check to make sure that the asteroid can only be "destroyed" once,
    // to prevent double scoring 
    bool isDestroyed = false; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Initialize(int hpValue, Vector2 velocity)
    {
        hp = hpValue;
        rb.velocity = velocity;
        UpdateHPText();
    }

    public void TakeDamage(int dmg)
    {
        if (isDestroyed)
        {
            return; 
        }

        // Reduce the asteroid's hp by the dmg amount and update the hp text
        hp -= dmg;
        UpdateHPText(); 

        // Asteroid is destroyed when it has no hp remaining
        if (hp <= 0)
        {
            isDestroyed = true; 
            ScoreManager.Instance.AddScore(100); // Grant the player points when destroyed 
            Destroy(gameObject);
        }
    }

    void UpdateHPText()
    {
        HPText.text = hp.ToString();
    }

    // Handle collision with the player's ship 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.PlayerCrashed(); 
        }
    }

    // Automatically despawns when going past the bottom of the screen, to save system resources 
    private void Update()
    {
        if (transform.position.y < -20f)
        {
            Destroy(this.gameObject); 
        }
    }
}
