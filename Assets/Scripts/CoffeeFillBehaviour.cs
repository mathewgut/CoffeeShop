using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CoffeeFillBehaviour : MonoBehaviour
{
    public SocketController fuelSocket;
    public SocketController fillSocket;
    public float timeToDispense = 5f;
    public Slider fuelUI;
    public bool isFilling = false;

    public GameObject particleContainer;
    public AudioSource fillingSound;

    float fuel;
    float dispenseAmount = 1;

    float dispenseRate;
    float fixedDispenseRate;
    ParticleSystem particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dispenseRate = dispenseAmount / timeToDispense; // amount per second
        fuelUI.maxValue = fillSocket.maxFuel;
        fixedDispenseRate = dispenseRate / 50f; // fixed update is called 50 times per second
        particles = particleContainer.GetComponent<ParticleSystem>();
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
            FillDrink();
        }
        else
        {
            isFilling = false;
            particles.enableEmission = false;

            if (!isFilling && fillingSound.isPlaying)
            {
                fillingSound.Stop();
            }
        }


    }


    void FillDrink()
    {
        FillableBehaviour toFill = fillSocket.attachedItem.GetComponent<FillableBehaviour>();

        if (toFill.currentAmount < toFill.maxAmount && fuel >= fixedDispenseRate)
        {
            toFill.currentAmount += fixedDispenseRate;
            fuelSocket.currentFuel -= fixedDispenseRate;
            isFilling = true;
            particles.enableEmission = true;

        }
        else
        {
            isFilling = false;
            particles.enableEmission = false;
        }

        if(isFilling && !fillingSound.isPlaying)
        {
            fillingSound.Play();
        }
        else if (!isFilling && fillingSound.isPlaying)
        {
            fillingSound.Stop();
        }

    }
}


