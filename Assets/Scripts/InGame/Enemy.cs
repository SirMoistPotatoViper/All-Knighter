using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;


    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }

    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Reset to the original color (assuming it's white)
        spriteRenderer.color = Color.white;
    }


    void Die ()
    {
        //make the animator bool trigger the death bool
        Destroy(gameObject);
    }
}
