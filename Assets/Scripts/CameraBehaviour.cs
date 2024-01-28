using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public NewBehaviourScript gameController;
    public InPlayer inPlayer;

    public bool inGame;

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
            gameObject.transform.position = new Vector3(gameController.shooterCharacter.transform.position.x,
                gameController.shooterCharacter.transform.position.y);
        }
        else if (!inGame)
        {
            gameObject.transform.position = new Vector3(gameController.player.transform.position.x,
                gameController.player.transform.position.y);
        }
    }
}
