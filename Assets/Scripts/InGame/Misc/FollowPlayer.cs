using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; //This script is comically simple, it just makes the attached object (camera) follow the player.
    public Vector3 offset;
    //private Vector3 offset = new Vector3(0f, 0f, -10f);
    // private float smoothTime = 0.10f;
    //private Vector3 velocity = Vector3.zero;

    //[SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPosition = target.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player 
    }
}
