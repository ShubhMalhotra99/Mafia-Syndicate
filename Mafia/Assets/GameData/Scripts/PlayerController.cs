using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f; // the player's movement speed
    public float sprintSpeed = 10f; // the player's sprint speed
    public float crouchSpeed = 3f; // the player's crouch speed
    public float jumpForce = 8f; // the force applied when the player jumps
    public float gravityScale = 2f; // the gravity scale of the player
    public float groundDistance = 0.2f; // the distance to check for ground
    public LayerMask groundMask; // the layer mask for the ground
    public Transform groundCheck; // the transform representing the position of the ground check
    public float turnSmoothTime = 0.1f; // the smoothing time for turning
    public Transform cameraTransform; // the transform of the camera
    public float camTiltAngle = 20f; // the angle to tilt the camera when crouching

    private Rigidbody rb; // the Rigidbody component of the player
    private float turnSmoothVelocity; // the velocity for smoothing turning
    private bool isGrounded; // whether or not the player is grounded
    private bool isSprinting; // whether or not the player is sprinting
    private bool isCrouching; // whether or not the player is crouching

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // get the Rigidbody component
    }

    private void FixedUpdate()
    {
        // check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // only allow jumping if the player is grounded
        if (isGrounded)
        {
            // handle jumping
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // handle movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // only rotate the player if moving
        if (direction.magnitude >= 0.1f)
        {
            // calculate the target angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // smooth the turning
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // rotate the player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // move the player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (isSprinting)
            {
                rb.MovePosition(transform.position + moveDir.normalized * sprintSpeed * Time.deltaTime);
            }
            else if (isCrouching)
            {
                rb.MovePosition(transform.position + moveDir.normalized * crouchSpeed * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
            }
        }

        // handle sprinting and crouching
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            isCrouching = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isSprinting = false;
            isCrouching = true;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isSprinting = false;
            isCrouching = false;
        }
    }
}