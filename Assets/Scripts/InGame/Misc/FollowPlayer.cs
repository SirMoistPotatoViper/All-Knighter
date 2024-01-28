using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public NewBehaviourScript gameController;
    public InPlayer inPlayer;

    public bool inGame;

    public Transform player; //This script is comically simple, it just makes the attached object (camera) follow the player.
    public Transform playerIRL;
    public Vector3 offset;
    //private Vector3 offset = new Vector3(0f, 0f, -10f);
    // private float smoothTime = 0.10f;
    //private Vector3 velocity = Vector3.zero;

    //[SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<NewBehaviourScript>();
        inPlayer = GameObject.FindObjectOfType<InPlayer>();

        inGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            //Vector3 targetPosition = target.position + offset;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player 
        }
        else if (!inGame)
        {
            //Vector3 targetPosition = target.position + offset;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.position = new Vector3(playerIRL.position.x + offset.x, playerIRL.position.y + offset.y, offset.z); // Camera follows the player 
        }
    }
}
