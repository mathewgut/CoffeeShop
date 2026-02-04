using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameObject Agent;
    public GameObject target;

    public List<GameObject> spawnPoints = new List<GameObject> { };

    public AudioSource splatSource;
    public AudioSource specialSource;
    public AudioSource speakSource;

    public ParticleSystem bloodParticles;

    public List<AudioClip> exitSounds = new List<AudioClip> { };

    Transform chosenSpawn;
    GameObject agentInstance;

    float bloodTimeStart;
    float destroyTimeStart;
    float explosionInterval;


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

        if (tick % 4 != 0)
        {
            return;
        }

        if(agentInstance.TryGetComponent<CustomerController>(out CustomerController customer))
        {
            speakSource.transform.position = agentInstance.transform.position;

            if (customer.orderComplete)
            {
                if(destroyTimeStart == -1)
                {
                    destroyTimeStart = Time.time;
                }

                if (explosionInterval == -1)
                {
                    explosionInterval = UnityEngine.Random.Range(1, 1.45f);
                }

                if (!speakSource.isPlaying)
                {
                    speakSource.clip = RandomExitSound();
                    speakSource.Play();
                        
                }

                // the burst sound should come ever so slightly before the rest of the effects
                if (!specialSource.isPlaying && Time.time - destroyTimeStart >= (explosionInterval - 0.1f))
                {
                    specialSource.Play();

                }

                if (Time.time - destroyTimeStart >= explosionInterval)
                {
                        
                    bloodParticles.Play();
                    splatSource.Play();
                    speakSource.Stop();

                    Destroy(agentInstance);

                    bloodTimeStart = Time.time;
                    chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
                    agentInstance = Instantiate(Agent, chosenSpawn.position, Quaternion.identity);
                    agentInstance.GetComponent<CustomerController>().target = target;
                    destroyTimeStart = -1;

                    explosionInterval = -1;
                }
            }
        }
            
            if(Time.time - bloodTimeStart >= 0.3f)
            {
                bloodParticles.Stop();
            }

        }

    
    AudioClip RandomExitSound()
    {
        return exitSounds[UnityEngine.Random.Range(0, exitSounds.Count)];
    }


}
