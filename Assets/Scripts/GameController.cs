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

    public Slider energyBar;
    public int energy;

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

        energy = 120;
        energyBar.value = energy;

        StartCoroutine(EnergyDrain());
    }

    private void moveHorizontal_Started(InputAction.CallbackContext context)
    {
        moveDirectionH = moveHorizontal.ReadValue<float>();
        if (moveDirectionH < 0)
        {
            player.transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            player.transform.Rotate(new Vector3(0, 0, -180));
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
            player.transform.Rotate(new Vector3(0, 0, -270));
        }
        else
        {
            player.transform.Rotate(new Vector3(0, 0, -90));
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
