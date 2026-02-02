using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameObject Agent;
    public GameObject target;

    public List<GameObject> spawnPoints = new List<GameObject> { };

    public AudioSource splatSource;
    public AudioSource specialSource;
    public ParticleSystem bloodParticles;

    Transform chosenSpawn;
    GameObject agentInstance;

    float bloodTimeStart;
    float destroyTimeStart;


    int tick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        agentInstance = Instantiate(Agent, chosenSpawn.position, Quaternion.identity);
        agentInstance.GetComponent<CustomerController>().target = target;

        bloodParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        tick += 1;

        if(tick % 4 == 0)
        {
            if(agentInstance.TryGetComponent<CustomerController>(out CustomerController customer))
            {
                if (customer.orderComplete)
                {
                    if(destroyTimeStart == -1)
                    {
                        destroyTimeStart = Time.time;
                    }

                    if (!specialSource.isPlaying)
                    {
                        specialSource.Play();
                    }
                    
                    
                    if (Time.time - destroyTimeStart >= 0.15f)
                    {
                        bloodParticles.Play();
                        
                        splatSource.Play();
                        Destroy(agentInstance);
                        bloodTimeStart = Time.time;
                        chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
                        agentInstance = Instantiate(Agent, chosenSpawn.position, Quaternion.identity);
                        agentInstance.GetComponent<CustomerController>().target = target;
                        destroyTimeStart = -1;
                    }
                }
            }
            
            if(Time.time - bloodTimeStart >= 0.3f)
            {
                bloodParticles.Stop();
            }

        }

    }
}
