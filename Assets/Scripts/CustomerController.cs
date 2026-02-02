using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class CustomerController : MonoBehaviour
{
    public GameObject target;
    public string acceptedItemTag = "coffee_cup";

    public int minCoffeeCount = 1;
    public int maxCoffeeCount = 4;

    public Canvas orderUI;
    public TextMeshProUGUI orderQuantity;

    int coffeeCount;
    public bool orderComplete = false;

    public ParticleSystem bloodParticles;


    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        coffeeCount = UnityEngine.Random.Range(minCoffeeCount, maxCoffeeCount + 1); // max is exclusive, +1 to include full range
        UpdateUI();
        orderUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }

        if (agent.transform.position.x == target.transform.position.x && // y position is inheriently different between the empty and ai
            agent.transform.position.z == target.transform.position.z)
        {
            orderUI.gameObject.SetActive(true);
        }

        orderUI.transform.LookAt(Camera.main.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colliding");

        if (collision.gameObject.TryGetComponent<PickupBehaviour>(out PickupBehaviour item))
        {
            if (item.id == acceptedItemTag && IsCupFull(item.gameObject))
            {
                coffeeCount -= 1;
                Destroy(collision.gameObject);
                CheckOrder();
            }
        }
    }

    bool IsCupFull(GameObject cup)
    {
        if(cup.TryGetComponent<FillableBehaviour>(out FillableBehaviour item))
        {
            return item.currentAmount >= item.maxAmount;
        }
        return false;
    }

    // when customer recieves a valid item, check if order is complete or not
    void CheckOrder()
    {
        if(coffeeCount <= 0)
        {
            orderComplete = true;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        orderQuantity.text = "x" + coffeeCount.ToString();  // e.g wants coffee x3
    }

}


