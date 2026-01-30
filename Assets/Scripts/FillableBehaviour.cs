using UnityEngine;
using UnityEngine.UI;

public class FillableBehaviour : MonoBehaviour
{
    public float maxAmount = 1.0f;
    public float currentAmount = 0f;
    public Slider fillBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fillBar.maxValue = maxAmount;
    }

    // Update is called once per frame
    void Update()
    {
        fillBar.value = currentAmount;
    }

    void FixedUpdate()
    {
        fillBar.transform.LookAt(Camera.main.transform);
    }
}

