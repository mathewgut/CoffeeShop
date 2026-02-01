using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    public GameObject Agent;
    public GameObject target;

    public List<GameObject> spawnPoints = new List<GameObject> { };

    Transform chosenSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        GameObject agentInstance = Instantiate(Agent,chosenSpawn);
        agentInstance.GetComponent<CustomerController>().target = target;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
