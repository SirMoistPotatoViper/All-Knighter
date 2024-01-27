using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float impactEffectDuration = 0.5f;
    //public LineRenderer lineRenderer;

    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        spawnPoint = transform;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null ) 
        {
            enemy.TakeDamage(damage);
        }

        GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impact, impactEffectDuration);
        //lineRenderer.SetPosition(0, spawnPoint.position);
        //lineRenderer.SetPosition(1, spawnPoint.position + spawnPoint.right * 100);


        Destroy(gameObject);
    }


}
