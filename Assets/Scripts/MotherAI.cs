using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using System.Runtime.InteropServices;
using Random = UnityEngine.Random;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class MotherAI : MonoBehaviour
{
    List<Transform> waypoints = new List<Transform>();
    public Transform target;
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public Transform pointE;
    public Transform pointF;
    public Transform pointG;
    public Transform pointH;
    public Transform pointI;

    public GameObject coneOfVision;
    public GameObject MotherHunt;

    public int nextPoint;

    public float speed;
    public float nextWaypointDistance;

    public Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        SetWaypoints();

        speed = 500;
        nextWaypointDistance = 0.5f;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RealPlayer")
        {
            Vector2 raycastOrigin = gameObject.transform.position + new Vector3(0.1f, 0.1f, 0);
            Vector2 raycastDirection = (player.transform.position - gameObject.transform.position).normalized;

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            coneOfVision.GetComponent<CircleCollider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection);
            Debug.DrawRay(raycastOrigin, raycastDirection, Color.white);

            if (hit.collider.tag == "RealPlayer")
            {
                MotherHunt.gameObject.SetActive(true);
                MotherHunt.transform.position = gameObject.transform.position;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(Echolocation());
            }
            //MotherHunt.gameObject.SetActive(true);
            //gameObject.GetComponent<CircleCollider2D>().enabled = true;
            //coneOfVision.GetComponent<CircleCollider2D>().enabled = true;
            //gameObject.SetActive(false);
        }
    }

    IEnumerator Echolocation()
    {
        coneOfVision.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        coneOfVision.GetComponent<CircleCollider2D>().enabled=true;
    }

    void SetWaypoints()
    {
        waypoints.Add(pointA);
        waypoints.Add(pointB);
        waypoints.Add(pointC);
        waypoints.Add(pointD);
        waypoints.Add(pointE);
        waypoints.Add(pointF);
        waypoints.Add(pointG);
        waypoints.Add(pointH);
        waypoints.Add(pointI);
    }

    void NewTarget()
    {
        nextPoint = Random.Range(-0, 9);
        target = waypoints[nextPoint];
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            NewTarget();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        transform.right = direction;


        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
