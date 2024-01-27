using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MotherBehaviour : MonoBehaviour
{
    public GameObject motherPointA;
    public GameObject motherPointB;
    public int NextPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator MotherPatrol()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
