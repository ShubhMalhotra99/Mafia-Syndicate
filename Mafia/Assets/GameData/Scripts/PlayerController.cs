using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Camera playerCamera;

    public bool IsGrounded;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void FixedUpdate()
    { 
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;
        
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (vertical * cameraForward + horizontal * playerCamera.transform.right).normalized;
        
        rb.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        
        transform.rotation = Quaternion.LookRotation(cameraForward);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            IsGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}