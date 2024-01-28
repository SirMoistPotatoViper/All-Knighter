using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class MotherHuntBehaviour : MonoBehaviour
{
    public GameObject Mother;
    public GameObject MotherHunt;
    public GameObject Player;

    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(-3.484f, 0.434f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RealPlayer")
        {
            Player.transform.position = spawnPoint;

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mother.transform.position.x, Mother.transform.position.y);
    }
}
