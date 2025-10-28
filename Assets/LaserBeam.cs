using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class that contains the working logic of a laser beam
public class LaserBeam : MonoBehaviour
{
    LineRenderer line;
    float duration;
    float maxDistance;
    int damage;
    LayerMask asteroidLayer;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Initialize(float dist, int dmg, float dur, LayerMask mask)
    {
        maxDistance = dist;
        damage = dmg;
        duration = dur;
        asteroidLayer = mask;

        Fire(); // automatically called Fire() when initialized
    }

    // Method to calculate damage and display the beam 
    void Fire()
    {
        Vector3 start = transform.position;
        Vector3 end = start + transform.up * maxDistance;

        // Detect all asteroid in line path
        RaycastHit[] hits = Physics.RaycastAll(start, transform.up, maxDistance, asteroidLayer);
        foreach (var hit in hits)
        {
            Asteroid asteroid = hit.collider.GetComponent<Asteroid>();
            asteroid.TakeDamage(damage); // deal damage to all asteroid in the path
        }

        // Visual beam
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        StartCoroutine(Expires()); 
    }

    IEnumerator Expires() // automatically destroy itself after a set duration
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
