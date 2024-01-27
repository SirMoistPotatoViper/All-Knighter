using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public PlayerInput playerControls;
    private InputAction moveVertical;
    private InputAction moveHorizontal;

    public GameObject player;
    public GameObject mother;

    public float moveDirectionV;
    public float moveDirectionH;

    // Start is called before the first frame update
    void Start()
    {
        playerControls.currentActionMap.Enable();
        moveVertical = playerControls.currentActionMap.FindAction("MoveVertical");
        moveVertical.started += moveVertical_Started;
        moveVertical.canceled += moveVertical_Canceled;
        moveHorizontal = playerControls.currentActionMap.FindAction("MoveHorizontal");
        moveHorizontal.started += moveHorizontal_Started;
        moveHorizontal.canceled += moveHorizontal_Canceled;
    }

    private void moveHorizontal_Started(InputAction.CallbackContext context)
    {
        moveDirectionH = moveHorizontal.ReadValue<float>();
    }
    private void moveHorizontal_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionH = 0;
    }

    private void moveVertical_Started(InputAction.CallbackContext context)
    {
        moveDirectionV = moveVertical.ReadValue<float>();
    }

    private void moveVertical_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionV = 0;
    }

    private void FixedUpdate()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(7 * moveDirectionH, 7 * moveDirectionV); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
