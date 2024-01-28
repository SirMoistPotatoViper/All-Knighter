using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherHuntBehaviour : MonoBehaviour
{
    public GameObject Mother;
    public GameObject MotherHunt;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(Mother.transform.position.x, Mother.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RealPlayer")
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mother.transform.position.x, Mother.transform.position.y);
    }
}
