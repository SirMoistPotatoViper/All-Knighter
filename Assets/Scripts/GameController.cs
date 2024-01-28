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

    public GameObject player;
    public GameObject mother;

    public GameObject childUp;
    public GameObject childDown;
    public GameObject childLeft;
    public GameObject childRight;

    public Slider energyBar;
    public int energy;

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

        energy = 120;
        energyBar.value = energy;

        StartCoroutine(EnergyDrain());
    }

    private void moveHorizontal_Started(InputAction.CallbackContext context)
    {
        moveDirectionH = moveHorizontal.ReadValue<float>();
        if (moveDirectionH < 0)
        {
            childLeft.SetActive(true);
            childRight.SetActive(false);
            childUp.SetActive(false);
            childDown.SetActive(false);
        }
        else if (moveDirectionH > 0)
        {
            childLeft.SetActive(false);
            childRight.SetActive(true);
            childUp.SetActive(false);
            childDown.SetActive(false);
        }
    }
    private void moveHorizontal_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionH = 0;
    }

    private void moveVertical_Started(InputAction.CallbackContext context)
    {
        moveDirectionV = moveVertical.ReadValue<float>();
        if (moveDirectionV < 0)
        {
            childLeft.SetActive(false);
            childRight.SetActive(false);
            childUp.SetActive(false);
            childDown.SetActive(true);
        }
        else if (moveDirectionV > 0)
        {
            childLeft.SetActive(false);
            childRight.SetActive(false);
            childUp.SetActive(true);
            childDown.SetActive(false);
        }
    }

    private void moveVertical_Canceled(InputAction.CallbackContext context)
    {
        moveDirectionV = 0;
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
        
    }
}
