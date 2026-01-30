using UnityEngine;
using UnityEngine.UI;

// checks for ray collisions on specific tags, updates crosshair
public class CrosshairEffects : MonoBehaviour
{

    public Sprite defaultCrosshair;
    public Sprite pickupCrosshair;
    public Image crosshairImage;
    public PickupInteraction playerInteraction;

    int tick = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tick += 1;

        if(tick % 2 == 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, playerInteraction.maxInteractRange) && 
                hit.transform.CompareTag(playerInteraction.pickupTag))
            {
                crosshairImage.sprite = pickupCrosshair;
            }
            else
            {
                crosshairImage.sprite = defaultCrosshair;
            }
        }
    }
}
