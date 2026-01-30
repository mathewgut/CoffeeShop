using UnityEngine;
using UnityEngine.UI;

public class CoffeeFillBehaviour : MonoBehaviour
{
    public SocketController fuelSocket;
    public SocketController fillSocket;
    public float timeToDispense = 5f;
    public Slider fuelUI;

    float fuel;
    float dispenseAmount = 1;
     
    float dispenseRate;
    float fixedDispenseRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dispenseRate =  dispenseAmount / timeToDispense; // amount per second
        fuelUI.maxValue = fillSocket.maxFuel;
        fixedDispenseRate = dispenseRate / 50f; // fixed update is called 50 times per second
    }

    // Update is called once per frame
    void Update()
    {
        fuel = fuelSocket.currentFuel;
        fuelUI.value = fuel;

        
    }

    void FixedUpdate()
    {
        fuelUI.transform.LookAt(Camera.main.transform);

        if (fillSocket.attachedItem)
        {
            Debug.Log("attatched");
            FillDrink();
        }
    }


    void FillDrink()
    {
        FillableBehaviour toFill = fillSocket.attachedItem.GetComponent<FillableBehaviour>();

        if (toFill.currentAmount < toFill.maxAmount && fuel >= fixedDispenseRate)
        {
            Debug.Log("Filling");
            toFill.currentAmount += fixedDispenseRate;
            fuelSocket.currentFuel -= fixedDispenseRate;
        }
        else
        {
            Debug.Log("Not filling");

        }


    }

}
