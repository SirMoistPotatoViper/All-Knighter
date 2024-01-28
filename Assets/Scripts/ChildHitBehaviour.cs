using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class ChildHitBeviour : MonoBehaviour
{
    public MotherAI motherAI;
    public NewBehaviourScript gameController;

    public GameObject Mother;
    public GameObject MotherHunt;
    public GameObject Player;

    public GameObject momScare;

    public GameObject tab;

    public AudioClip momScream;

    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        motherAI = GameObject.FindObjectOfType<MotherAI>();
        gameController = GameObject.FindObjectOfType<NewBehaviourScript>();

        spawnPoint = new Vector3(-3.484f, 0.434f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EvilMother")
        {
            StartCoroutine(MomJumpscare());

            Player.transform.position = spawnPoint;

            gameController.energy = gameController.energy - 50;

            gameController.stealthMusic.SetActive(true);
            gameController.chaseMusic.SetActive(false);
            motherAI.Mother.SetActive(true);
            motherAI.coneOfVision.GetComponent<CircleCollider2D>().enabled = true;
            motherAI.Mother.transform.position = new Vector3(motherAI.MotherHunt.transform.position.x,
                motherAI.MotherHunt.transform.position.y);
            motherAI.MotherHunt.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Computer")
        {
            gameController.PlayGame();
            tab.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Computer")
        {
            gameController.DisableGame();
            tab.SetActive(false);
        }
    }

    IEnumerator MomJumpscare() 
    {
        momScare.SetActive(true);
        AudioSource.PlayClipAtPoint(momScream, transform.position);
        yield return new WaitForSeconds(1);
        momScare.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
