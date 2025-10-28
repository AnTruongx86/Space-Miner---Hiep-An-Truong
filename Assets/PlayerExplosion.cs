using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Hiep An Truong

// A helper class that handles the sound and visual effects when the player collides with an asteroid 
public class PlayerExplosion : MonoBehaviour
{
    public void Initialize(Vector3 position, AudioClip sound, Sprite explosionSprite, float spriteDuration, float spriteScale)
    {
        transform.position = position; // position of the collision
        StartCoroutine(Explosion(sound, explosionSprite, spriteDuration, spriteScale)); // then call an explosion at that position
    }

    IEnumerator Explosion(AudioClip sound, Sprite explosionSprite, float spriteDuration, float spriteScale)
    {
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

        // Wait for the sound to finish
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
    }

    IEnumerator EndVisual(SpriteRenderer sr, float duration) // End the explosion visual 
    {
        yield return new WaitForSeconds(duration);
        Destroy(sr.gameObject);
    }
}
