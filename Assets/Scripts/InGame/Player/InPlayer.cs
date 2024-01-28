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
    public NewBehaviourScript gameController;
    public FollowPlayer cameraBehaviour;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Animator m_Animator;
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
    private InputAction escape;
    HealthBarScript health;
    public AudioClip laserSound;
    public AudioClip jumpSound;

    public GameObject walkSound;



    [SerializeField]
    public Transform firePoint;
    public GameObject bulletPrefab;


    //public HealthBarBehaviour pHealth;
    //public float damage;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<NewBehaviourScript>();
        cameraBehaviour = GameObject.FindObjectOfType<FollowPlayer>();

        m_Animator = gameObject.GetComponent<Animator>();
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
        escape = MPI.currentActionMap.FindAction("escape");



        jump.started += Handle_JumpPerformed;
        jump.canceled += Handle_Jump_Canceled;

        move.started += Handle_MovePerformed;
        move.canceled += Handle_Move_Canceled;

        //.canceled += Handle_MoveCanceled;
        restart.performed += Handle_RestartPerformed;
        quit.performed += Handle_QuitPerformed;
        shoot.performed += Handle_ShootPerformed;

        //escape.started += Escape_started;
        escape.started += Handle_EscapePerformed;

        DisableControls();
    }

    private void Handle_EscapePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("This should go");
        cameraBehaviour.inGame = false;
        gameController.EnableControls();
        DisableControls();
    }

    private void Escape_started(InputAction.CallbackContext obj)
    {
        cameraBehaviour.inGame = false;
        gameController.DisableControls();
    }

    private void Handle_Jump_Canceled(InputAction.CallbackContext context)
    {
        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
    }

    private void Handle_Move_Canceled(InputAction.CallbackContext context)
    {
        walkSound.SetActive(false);
        Debug.Log("Pls");
        horizontal = 0f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        m_Animator.SetBool("run", false);
    }

    private void Handle_JumpPerformed(InputAction.CallbackContext context)
    {
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        m_Animator.SetTrigger("boing");
        Debug.Log("Read this");
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        /*if (context.canceled && rb.velocity.y > 0f)
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
        if (IsGrounded()) 
        {
            m_Animator.SetBool("grounded", true);
        }
        else
        {
            m_Animator.SetBool("grounded", false);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            pHealth.health
        }
    }*/
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
        m_Animator.SetTrigger("shoot");
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
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


       walkSound.SetActive(true);
    // Input is actively performed (button pressed or held)
    horizontal = context.ReadValue<Vector2>().x;
            Debug.Log("Moving!");
        m_Animator.SetBool("run", true);

        //else if (context.canceled)
        //{
        // Input has been released
        // rb.velocity = new Vector2(0, rb.velocity.y);
        //Debug.Log("Not moving anymore.");
        //}
    }

    public void DisableControls()
    {
        //Remove control when OnDestroy activates
        jump.started -= Handle_JumpPerformed;
        move.started -= Handle_MovePerformed;
        //move.canceled -= Handle_MoveCanceled;
        restart.performed -= Handle_RestartPerformed;
        quit.performed -= Handle_QuitPerformed;
        shoot.performed -= Handle_ShootPerformed;
        move.canceled -= Handle_Move_Canceled;
        escape.performed -= Escape_started;
    }

    public void EnableControls()
    {
        //Remove control when OnDestroy activates
        jump.started += Handle_JumpPerformed;
        move.started += Handle_MovePerformed;
        //move.canceled -= Handle_MoveCanceled;
        restart.performed += Handle_RestartPerformed;
        quit.performed += Handle_QuitPerformed;
        shoot.performed += Handle_ShootPerformed;
        move.canceled += Handle_Move_Canceled;
        escape.performed += Escape_started;
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
        jump.started -= Handle_JumpPerformed;
        move.started -= Handle_MovePerformed;
        //move.canceled -= Handle_MoveCanceled;
        restart.performed -= Handle_RestartPerformed;
        quit.performed -= Handle_QuitPerformed;
        shoot.performed -= Handle_ShootPerformed;
        move.canceled -= Handle_Move_Canceled;
    }
}

