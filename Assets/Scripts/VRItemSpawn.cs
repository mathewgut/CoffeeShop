// assumes spawn item is a pickup type interactable

using UnityEngine;

public class VRItemSpawn : MonoBehaviour
{
    public GameObject spawnItem;
    GameObject currentItem;

    public Vector3 spawnPos;
    string spawnID;

    PickupBehaviour pickupScript;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnItem();
        pickupScript = spawnItem.GetComponent<PickupBehaviour>();
        spawnID = pickupScript.id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PickupBehaviour>(out PickupBehaviour exitScript))
        {
            if (exitScript.id == spawnID)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.transform.SetParent(null);
                currentItem = null;

                SpawnItem();
            }
        }
    }



    void SpawnItem ()
    {
        currentItem = Instantiate(spawnItem, transform.position, Quaternion.identity);
        //currentItem.transform.localPosition = Vector3.zero;
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
        

    }

}
