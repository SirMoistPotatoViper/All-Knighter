using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    public Animator m_Animator;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;

    public Transform respawnPosition; // Set this in the Inspector to the desired respawn point

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            Debug.Log("Player Dead");
            m_Animator.SetTrigger("dead");
            m_Animator.SetBool("deadStill", true);
            health = maxHealth;
            //m_Animator.SetBool("deadStill", true);
            //Die();
            // Wait for a second, then respawn the player
            Invoke("Respawn", 1f);
        }
    }
    /*void Die()
    {
        m_Animator.SetBool("deadStill", true);
        //Enable dead bool
    }*/

    void Respawn()
    {
        m_Animator.SetBool("deadStill", false);
        // Respawn the player at the specified position
        transform.position = respawnPosition.position;
        //m_Animator.SetBool("dead", false);
        // Reset health to full upon respawn
        health = maxHealth;
    }

    public void TakeDamage()
    {
        StartCoroutine(FlashRed());
        health -= 20;
    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Reset to the original color (assuming it's white)
        spriteRenderer.color = Color.white;
    }
}