using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public PlayerInput playerControls;
    private InputAction moveVertical;
    private InputAction moveHorizontal;

    public AudioClip footsteps;

    public Animator animator;

    public GameObject player;
    public GameObject childGFX;
    public GameObject mother;

    public Slider energyBar;
    public int energy;

    public string directionFacing;

    public float moveDirectionV;
    public float moveDirectionH;

    public GameObject eepy1;
    public GameObject eepy2;


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

        animator = childGFX.GetComponent<Animator>();

        directionFacing = "Up";

        energy = 120;
        energyBar.value = energy;

        StartCoroutine(EnergyDrain());
    }

    private void moveHorizontal_Started(InputAction.CallbackContext context)
    {
        animator.SetBool("Walking", true); 
        moveDirectionH = moveHorizontal.ReadValue<float>();
        if (moveDirectionH < 0)
        {
            if (directionFacing == "Up") {childGFX.transform.Rotate(0, 0, 90); directionFacing = "Left"; }
            if (directionFacing == "Right") {childGFX.transform.Rotate(0, 0, 180); directionFacing = "Left"; }
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Left"; }
        }
        else if (moveDirectionH > 0)
        {
            if (directionFacing == "Up") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Right"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, -180); directionFacing = "Right"; }
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Right"; }
        }
    }
    private void moveHorizontal_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionH = 0;
        animator.SetBool("Walking", false);
        if (moveDirectionV < 0)
        {
            animator.SetBool("Walking", true);
            if (directionFacing == "Up") { childGFX.transform.Rotate(0, 0, 180); directionFacing = "Down"; }
            if (directionFacing == "Right") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Down"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Down"; }
        }
        else if (moveDirectionV > 0)
        {
            animator.SetBool("Walking", true);
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, -180); directionFacing = "Up"; }
            if (directionFacing == "Right") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Up"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Up"; }
        }
    }

    private void moveVertical_Started(InputAction.CallbackContext context)
    {
        animator.SetBool("Walking", true);
        moveDirectionV = moveVertical.ReadValue<float>();
        if (moveDirectionV < 0)
        {
            if (directionFacing == "Up") { childGFX.transform.Rotate(0, 0, 180); directionFacing = "Down"; }
            if (directionFacing == "Right") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Down"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Down"; }
        }   
        else if (moveDirectionV > 0)
        {
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, -180); directionFacing = "Up"; }
            if (directionFacing == "Right") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Up"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Up"; }
        }
    }

    private void moveVertical_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionV = 0;
        animator.SetBool("Walking", false);
        if (moveDirectionH < 0)
        {
            animator.SetBool("Walking", true);
            if (directionFacing == "Up") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Left"; }
            if (directionFacing == "Right") { childGFX.transform.Rotate(0, 0, 180); directionFacing = "Left"; }
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Left"; }
        }
        else if (moveDirectionH > 0)
        {
            animator.SetBool("Walking", true);
            if (directionFacing == "Up") { childGFX.transform.Rotate(0, 0, -90); directionFacing = "Right"; }
            if (directionFacing == "Left") { childGFX.transform.Rotate(0, 0, -180); directionFacing = "Right"; }
            if (directionFacing == "Down") { childGFX.transform.Rotate(0, 0, 90); directionFacing = "Right"; }
        }
    }

    IEnumerator EnergyDrain()
    {
        energy--;
        energyBar.value = energy;
        yield return new WaitForSeconds(1);
        EndGame();
    }

    void EndGame()
    {
        if (energy > 0) 
        {
            StartCoroutine(EnergyDrain());
        }
        else if (energy <= 0)
        {
            //insert end game here
        }
    }

    private void FixedUpdate()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(7 * moveDirectionH, 7 * moveDirectionV); 
    }

    // Update is called once per frame
    void Update()
    {
        if (energy < 30)
        {
            eepy2.SetActive(true);
        }
        else if (energy <= 60)
        {
            eepy1.SetActive(true);
        }
        else
        {
            eepy1.SetActive(false); eepy2.SetActive(false);
        }
    }
}
