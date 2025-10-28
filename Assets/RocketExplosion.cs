using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// Class that handles the damage calculation and effects when a rocket collies with an asteroid 
public class RocketExplosion : MonoBehaviour
{
    public void Initialize(Vector3 position, float radius, float damage, AudioClip sound, Sprite explosionSprite, float spriteDuration, float spriteScale)
    {
        transform.position = position;
        StartCoroutine(Explosion(radius, damage, sound, explosionSprite, spriteDuration, spriteScale));
    }

    IEnumerator Explosion(float radius, float damage, AudioClip sound, Sprite explosionSprite, float spriteDuration, float spriteScale)
    {
        // Apply aoe damage
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Asteroid"));
        foreach(var hit in hits)
        {
            Asteroid asteroid = hit.GetComponent<Asteroid>();
            asteroid.TakeDamage((int)damage); 
        }

        // Create visual explosion sprite
        GameObject visual = new GameObject("ExplosionSprite");
        visual.transform.position = transform.position + new Vector3(0, 0, -1f);
        visual.transform.localScale = Vector3.one * spriteScale;

        SpriteRenderer sr = visual.AddComponent<SpriteRenderer>();
        sr.sprite = explosionSprite;
        sr.sortingOrder = 10;
        StartCoroutine(EndVisual(sr, spriteDuration));

        // Play explosion sound
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.Play();

        // Wait for the sound to finish, then destroy the game object
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
    }

    IEnumerator EndVisual(SpriteRenderer sr, float duration) // End the explosion visual 
    {
        yield return new WaitForSeconds(duration);
        Destroy(sr.gameObject);
    }
}
