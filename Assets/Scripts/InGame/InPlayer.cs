using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public class InPlayer : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    //private bool isPlayerMoving;
    public PlayerInput MPI;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction jump;
    private InputAction shoot;

    [SerializeField]
    public Transform firePoint;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script starts");
        rb = GetComponent<Rigidbody2D>();
        MPI = GetComponent<PlayerInput>();
        MPI.currentActionMap.Enable();


        //Grabs all the player's inputs
        move = MPI.currentActionMap.FindAction("move");
        restart = MPI.currentActionMap.FindAction("restart");
        quit = MPI.currentActionMap.FindAction("quit");
        jump = MPI.currentActionMap.FindAction("jump");
        shoot = MPI.currentActionMap.FindAction("shoot");



        jump.started += Handle_Jump_Started;

        move.started += Handle_MovePerformed;
        move.canceled += Handle_Move_Canceled;

        //.canceled += Handle_MoveCanceled;
        restart.performed += Handle_RestartPerformed;
        quit.performed += Handle_QuitPerformed;
        shoot.performed += Handle_ShootPerformed;

    }

    private void Handle_Move_Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Pls");
        horizontal = 0f;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Handle_Jump_Started(InputAction.CallbackContext context)
    {
        Debug.Log("Read this");
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        /*if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        {
            if (!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }
        }
    }
    /*private void FixedUpdate()
    {
        if (isPlayerMoving)
        {
            //move the paddle
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * moveDirection * paddleSpeed, 0);
        }
        else
        {
            //stop the paddle
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }*/
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        /*Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;*/

        transform.Rotate(0f, 180f, 0f);
    }

    private void Handle_ShootPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("shoot");
        Shoot();
    }

    void Shoot ()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void Handle_QuitPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Quit");
    }

    private void Handle_RestartPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("restart");
    }

    /*private void Handle_MoveCanceled(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }*/

    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Ugh");

        
       
            // Input is actively performed (button pressed or held)
            horizontal = context.ReadValue<Vector2>().x;
            Debug.Log("Moving!");
        
        //else if (context.canceled)
        //{
            // Input has been released
           // rb.velocity = new Vector2(0, rb.velocity.y);
            //Debug.Log("Not moving anymore.");
        //}
    }


    /*private void Handle_Jump_Started(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }*/

    public void OnDestroy()
    {
        //Remove control when OnDestroy activates
        jump.started -= Handle_Jump_Started;
        move.started -= Handle_MovePerformed;
        //move.canceled -= Handle_MoveCanceled;
        restart.performed -= Handle_RestartPerformed;
        quit.performed -= Handle_QuitPerformed;
        shoot.performed -= Handle_ShootPerformed;
        move.canceled -= Handle_Move_Canceled;
    }
}

