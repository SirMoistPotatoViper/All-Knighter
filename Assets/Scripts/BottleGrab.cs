using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGrab : MonoBehaviour
{
    GameObject gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("RealPlayer") == true)
        {
            gc.GetComponent<NewBehaviourScript>().energy = 100;
            Destroy(gameObject);
        }
    }
}
