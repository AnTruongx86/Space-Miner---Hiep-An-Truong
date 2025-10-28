using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class that handles the working logic of spawning asteroids 
public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 1.0f;
    public float horizontalSpread = 7.0f;
    public float minSpeed = 1.5f;
    public float maxSpeed = 4f;
    public int baseHp = 3;
    public float hpIncreasePerMinute = 1.0f;

    Camera cam;
    float timer;

    void Start()
    {
        cam = Camera.main;
        timer = 0f; 
    }

    // Spawn an asteroid after a spwanInterval 
    void Update() {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer -= spawnInterval;
            SpawnAsteroid(); 
        }
    }

    // Method to spawn asteroid 
    void SpawnAsteroid()
    {
        // Spawn at random x along top of camera view
        float zDist = -cam.transform.position.z;
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, zDist));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, zDist));
        float spawnX = Random.Range(topLeft.x, topRight.x);
        Vector3 spawnPos = new Vector3(spawnX, topLeft.y + 1f, 0); // spawn at slightly above the top of the screen

        GameObject go = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        // random horizontal drift to either left or right
        float angle = Random.Range(-30f, 30f);
        float speed = Random.Range(minSpeed, maxSpeed);

        Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
        Vector2 velocity = dir.normalized * speed;

        // Increasing hp over time to make the game more difficult 
        int extraHp = Mathf.FloorToInt((Time.timeSinceLevelLoad / 60f) * hpIncreasePerMinute);
        int hp = baseHp + extraHp;

        Asteroid a = go.GetComponent<Asteroid>();
        a.Initialize(hp, velocity);
        
    }
}
