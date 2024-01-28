using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f; //How long till fall
    public float destroyDelay = 2f; //How long till vanish

    [SerializeField] private Rigidbody2D rb; //references platforms rigidbody
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay); //Wait fall delay seconds
        rb.bodyType = RigidbodyType2D.Dynamic; //Make it fall fall fall
        Destroy(gameObject, destroyDelay); //Delete after destroy delay
    }
    
}
