using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// manages all pickup/interactable object interactions relative to player input
public class PickupInteraction : MonoBehaviour
{
    public string pickupTag;
    public float maxInteractRange;
    public GameObject Player;
    public float HoldDistance = 5f;
    public float throwForce = 15f;

    public GameObject heldItem = null;
    Camera MainCamera;

    RaycastHit hit;

    float tick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (tick % 4 == 0)
        {

            if (Input.GetKeyDown(KeyCode.E) && !heldItem)
            {
                if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, maxInteractRange) &&
                    hit.transform.CompareTag(pickupTag))
                {
                    PickupItem();
                }
                else if (hit.transform.CompareTag("SpawnSource"))
                {
                    SpawnSourceBehaviour sourceScript = hit.transform.GetComponent<SpawnSourceBehaviour>();

                    if (!sourceScript.maxSpawnFlag) {
                        InstantiateAndHold(sourceScript.sourceItem);
                        sourceScript.AddSpawn(sourceScript.sourceItem);
                    }
                }

            }
            else if (Input.GetKeyDown(KeyCode.E) && heldItem)
            {
                DropItem(() => { });
            }

            if (heldItem && Input.GetMouseButtonDown(0))
            {
                ThrowHeldItem();
            }
            else if (heldItem)
            {
                UpdateHeldPosition();
            }
        }
    }

    void PickupItem() 
    {
        if (!heldItem)
        {
            heldItem = hit.transform.gameObject;
            heldItem.transform.SetParent(Player.transform);
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }
    public void DropItem(System.Action ? onDrop = null)
    {
        if (heldItem)
        {
            heldItem.transform.SetParent(null);
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            onDrop();
            heldItem = null;
            
        }
    }

    void UpdateHeldPosition()
    {
        Vector3 targetPos = MainCamera.transform.position + (Camera.main.transform.forward * HoldDistance);

        heldItem.transform.position = targetPos;
    }

    void ThrowHeldItem()
    {
        DropItem(() => heldItem.GetComponent<Rigidbody>().AddForce(MainCamera.transform.forward * throwForce));
    }

    void InstantiateAndHold (GameObject item)
    {
        if (!heldItem)
        {
            heldItem = Instantiate(item, Camera.main.transform);
            heldItem.transform.SetParent(Player.transform);
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}
