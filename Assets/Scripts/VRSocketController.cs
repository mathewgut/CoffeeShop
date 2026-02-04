using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class VRSocketController : MonoBehaviour
{
    public enum SocketType
    {
        Fuel,
        Hold
    }

    [SerializeField] private SocketType _socketType;

    public PickupInteraction playerInteraction;

    public List<string> allowedInputs = new List<string> { };

    bool hasItem = false;
    bool inBoundry = false;

    GameObject inBoundryItem;
    public GameObject attachedItem;

    public float currentFuel = 0;
    public float maxFuel = 9;

    public AudioSource onFillSound;

    // Update is called once per frame
    void Update()
    {
        if (inBoundry && !hasItem && inBoundryItem != null && inBoundryItem.GetComponent<PickupBehaviour>().wontAttatch == false)
        {
            Debug.Log("attach");
            AttachItem();
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

        if(other.gameObject == attachedItem)
        {
            DetachItem(() => { });
        }

        /*
        if (other.gameObject.CompareTag(playerInteraction.pickupTag) &&
            other.gameObject.GetComponent<PickupBehaviour>().wontAttatch == true)
        {
            other.gameObject.GetComponent<PickupBehaviour>().wontAttatch = false;
        }
        */

    }

    void AttachItem()
    {
        attachedItem = inBoundryItem;

        // gets the prefab reference from the object and checks it against the prefab within the allowed
        if (allowedInputs.Count > 0 && allowedInputs.Contains(attachedItem.GetComponent<PickupBehaviour>().id) || allowedInputs.Count == 0)
        {

            // since i am not creating my own models, sometimes the orientations can be weird, set a default in script and check if default exists
            if (attachedItem.TryGetComponent<FillableBehaviour>(out FillableBehaviour fillableScript))
            {
                attachedItem.transform.rotation = Quaternion.Euler(fillableScript.upright);
            }
            else
            {
                attachedItem.transform.rotation = Quaternion.identity;
            }

            
            attachedItem.transform.position = transform.position;
            attachedItem.GetComponent<Rigidbody>().isKinematic = true;
            hasItem = true;
            AttachAction();
        }
        else
        {
            attachedItem = null;
        }
    }
    void DetachItem(System.Action? onDetatch = null)
    {
        hasItem = false;
        attachedItem.GetComponent<PickupBehaviour>().wontAttatch = false;
        onDetatch();
        attachedItem = null;
    }

    void AttachAction()
    {
        if (_socketType == SocketType.Fuel)
        {
            float itemFuel = attachedItem.GetComponent<FuelItem>().fuelAmount;

            if (currentFuel < maxFuel)
            {
                currentFuel = Mathf.Clamp(itemFuel + currentFuel, 0, maxFuel);
            }

            onFillSound.Play();

            Debug.Log("on detach");
            DetachItem(() => {
                
                Destroy(attachedItem);
                inBoundryItem = null;
                inBoundry = false;
            });
        }
    }

}
