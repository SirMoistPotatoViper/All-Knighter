using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaBehaviour : MonoBehaviour
{
    public NewBehaviourScript gameController;

    public bool inSafeZone;

    public MotherAI motherAI;

    // Start is called before the first frame update
    void Start()
    {
        motherAI = GameObject.FindObjectOfType<MotherAI>();
        gameController = GameObject.FindObjectOfType<NewBehaviourScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RealPlayer")
        {
            gameController.stealthMusic.SetActive(true);
            gameController.chaseMusic.SetActive(false);
            inSafeZone = true;
            motherAI.Mother.SetActive(true);
            motherAI.coneOfVision.GetComponent<CircleCollider2D>().enabled = true;
            motherAI.Mother.transform.position = new Vector3(motherAI.MotherHunt.transform.position.x, 
                motherAI.MotherHunt.transform.position.y);
            motherAI.MotherHunt.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
