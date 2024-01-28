using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergyDrink : MonoBehaviour
{
    public float spawnTime = 0f;

    public GameObject bottle;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("EnergyDrink(Clone)") == null)
        {
            spawnTime += 1 * Time.deltaTime;
            if(spawnTime >= 30)
            {
                SpawnEnergy();
            }
        }
    }

    void SpawnEnergy()
    {
        Instantiate(bottle, transform.position, Quaternion.identity);
        spawnTime = 0;
    }
}
