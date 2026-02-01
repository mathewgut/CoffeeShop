using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    public GameObject target;
    public string acceptedItemTag;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
