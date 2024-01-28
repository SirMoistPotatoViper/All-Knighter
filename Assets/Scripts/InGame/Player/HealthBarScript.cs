using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.UIElements;

public class HealthBarScript : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    public Animator m_Animator;
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;

    public float regenerationRate = 0.5f;

    [SerializeField] private float invulnerbilityDuration;
    [SerializeField] private int numberOfFlashes;


    public AudioClip deathSound;
    public AudioClip hurtSound;

    public Transform respawnPosition;
    public Transform defaultRespawnPosition;
    public Transform checkpoint1;
    public Transform checkpoint2;
    // Set this in the Inspector to the desired respawn point

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPosition = defaultRespawnPosition;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Debug.Log("Player Dead");
            m_Animator.SetTrigger("dead");
            m_Animator.SetBool("deadStill", true);
            health = maxHealth;
            //m_Animator.SetBool("deadStill", true);
            //Die();
            // Wait for a second, then respawn the player
            Invoke("Respawn", 1f);
        }
        if (health < maxHealth)
        {
            health += regenerationRate * Time.deltaTime;
            // Clamp the health to the maximum value
            health = Mathf.Clamp(health, 0, maxHealth);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Ebullet"))
        {
            Debug.Log("Hit with bullet");
            TakeDamage();
            //StartCoroutine(FlashRed());
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            // Set the respawn position based on the checkpoint
            if (collision.gameObject.name == "Checkpoint1")
            {
                respawnPosition = checkpoint1;
            }
            else if (collision.gameObject.name == "Checkpoint2")
            {
                respawnPosition = checkpoint2;
            }
        }
    }
    public void TakeDamage()
    {
        AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        StartCoroutine(FlashRed());
        
        health -= 20;
        StartCoroutine(Invulnerability());
    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        // Reset to the original color (assuming it's white)
        spriteRenderer.color = Color.white;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        //invulenerable duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerbilityDuration / numberOfFlashes * 2);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invulnerbilityDuration / numberOfFlashes * 2);
        }
        Physics2D.IgnoreLayerCollision(10, 9, false);
    }
}