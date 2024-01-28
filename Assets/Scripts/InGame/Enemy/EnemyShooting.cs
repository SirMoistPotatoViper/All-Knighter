using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    Animator m_Animator;
    public AudioClip enemyShootSound;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 10)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

        
    }

    void shoot()
    {
        AudioSource.PlayClipAtPoint(enemyShootSound, transform.position);
        m_Animator.SetTrigger("shoot");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
