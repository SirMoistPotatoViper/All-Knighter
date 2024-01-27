using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject enterCom;
    public Camera topCam;
    public Camera sideCam;

    float w = 1.84f;
    float h = 1.035f;

    public bool comChild = false;
    public bool inGame = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player.png");
        topCam.enabled = true;
        sideCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= 2 && comChild != true)
        {
            enterCom.SetActive(true);
        }
        else
        {
            enterCom.SetActive(false);
        }

        if (enterCom.active == true && Input.GetKeyDown("tab"))
        {
            comChild = true;
        }

        if(comChild == true)
        {
            Zoom();
            inGame = true;
        }

        if(Input.GetKeyDown("q"))
        {
            GoBack();
        }
    }

    void Zoom()
    {
        enterCom.SetActive(false);
        topCam.transform.parent = this.transform;
        topCam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);

        if (comChild == true && topCam.orthographicSize > (w / topCam.pixelWidth * topCam.pixelHeight) / 2)
        {
            topCam.orthographicSize -= 5.5f * Time.deltaTime;
        }
        SwitchCam();
    }

    void SwitchCam()
    {
        if(comChild == true && topCam.orthographicSize <= (w / topCam.pixelWidth * topCam.pixelHeight) / 2)
        {
            sideCam.enabled = true;
            topCam.enabled = false;
        }
    }

    void GoBack()
    {
        if (inGame == true)
        {
            inGame = false;
            sideCam.enabled = false;
            topCam.enabled = true;
            comChild = false;
            topCam.transform.parent = GameObject.Find("player.png").transform;
            topCam.transform.position = new Vector3(GameObject.Find("player.png").transform.position.x, GameObject.Find("player.png").transform.position.y, -10);
            topCam.orthographicSize = 5;
        }
    }
}
