using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator m_Animator;
    public int health = 100;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;
    public float deathEffectDuration = 0.8f;


    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            m_Animator.SetTrigger("dead");
            deathEffect.SetActive(true);
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


    void Die()
    {
        //deathEffect.SetActive(true);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(effect, deathEffectDuration);


        //make the animator bool trigger the death bool
        //yield return new WaitForSeconds(1);
        Destroy(gameObject, deathEffectDuration);
    }
}
