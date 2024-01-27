using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    void Start()
    {
        
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        //Debug.Log("Hm");

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null) 
        {
            Debug.Log("Get here");
            colInfo.GetComponent<HealthBarScript>().TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
