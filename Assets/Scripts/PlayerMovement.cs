using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    float initialSpeed;
    float sprintSpeed;
    public float jumpForce = 2f;
    public float sprintFactor = 1.5f;
    public bool isSprinting;
    float initialFov;
    public float sprintFov = 110f;


    // gives us flexibility to mess with gravity localized to the player
    public float gravity = -9.81f;

    private CharacterController cc;
    private Vector3 velocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        initialSpeed = speed;
        sprintSpeed = speed * sprintFactor;
        initialFov = Camera.main.fieldOfView;

    }

    void Update()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        cc.Move(move * speed * Time.deltaTime);

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            isSprinting = true;
            speed = sprintSpeed;
            Camera.main.fieldOfView = sprintFov;
        }
        else
        {
            isSprinting = false;
            speed = initialSpeed;
            Camera.main.fieldOfView = initialFov;

        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}          
