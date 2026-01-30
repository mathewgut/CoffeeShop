using UnityEngine;

public class SocketController : MonoBehaviour
{
    public PickupInteraction playerInteraction;
    public GameObject player;

    bool hasItem = false;
    bool inBoundry = false;

    GameObject inBoundryItem;
    GameObject attachedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (inBoundry && !hasItem && inBoundryItem.GetComponent<PickupBehaviour>().wontAttatch == false)
        {
            AttachItem();
        }

        if (hasItem && attachedItem.transform.parent == player.transform)
        {
            DetachItem();
        }
    }

    // COLLIDERS AND FUNCTIONS //

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerInteraction.pickupTag))
        {
            inBoundryItem = other.gameObject;
            inBoundry = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == inBoundryItem)
        {
            inBoundryItem = null;
            inBoundry = false;
        }

        if (other.gameObject.CompareTag(playerInteraction.pickupTag) && 
            other.gameObject.GetComponent<PickupBehaviour>().wontAttatch == true)
        {
            other.gameObject.GetComponent<PickupBehaviour>().wontAttatch = false;
        }
    }

    bool PlayerHoldingItem (GameObject item)
    {
        return item.transform.parent == player;
    }

    void AttachItem() {
        attachedItem = inBoundryItem;

        if (playerInteraction.heldItem)
        {
            playerInteraction.DropItem(() =>
            {
                attachedItem.transform.position = transform.position;
                attachedItem.transform.SetParent(transform);
            });
        }
        else
        {
            attachedItem.transform.position = transform.position;
            attachedItem.transform.SetParent(transform);
        }

        attachedItem.GetComponent<Rigidbody>().isKinematic = true;
        
        hasItem = true;
    }
    void DetachItem ()
    {
        hasItem = false;
        attachedItem.GetComponent<PickupBehaviour>().wontAttatch = true;
        attachedItem = null;
    }

}
