using UnityEngine;

// This script is for 2D movement with drag and without physics
// You can use this script for 2D platformer games and 2D top down games
// made by sillylix 2025 - https://www.sillylix.com/

public class PlayerMovement2D : MonoBehaviour
{
    // --- Player Movement Settings ---
    [Header("Movement Settings")]
    [Space(10)]
    [SerializeField] public bool horizontalMovementNeeded = true;
    [SerializeField] public bool verticalMovementNeeded = false;
    [SerializeField] private float playerSpeed = 5f;

    [Header("Rigidbody Movement Settings")]
    [Space(10)]
    [Tooltip("Enable this if you want to use Rigidbody for movement.")]
    [SerializeField] private bool useRigidbodyForMovement = true;

    // --- Jump Settings ---
    [Header("Jump Settings")]
    [Space(10)]
    [Tooltip("Enable this if you want the player to jump.")]
    [SerializeField] private bool jumpNeeded = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float jumpDetectorLength = 1.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool drawJumpDetector = false;

    // --- Private Variables ---
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded;
    private Rigidbody2D rb2d;


    void Start()
    {
        // get the rigidbody2d component
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;

    }

    void Update()
    {
        // check if we need to use physics for movement or not
        if (useRigidbodyForMovement)
        {
            MovementWithPhysics();
        }
        else
        {
            MovementWithoutPhysics();
        }

        // check if we need to jump or not
        if (jumpNeeded)
        {
            // set the gravity on and jump if we are grounded
            rb2d.gravityScale = 1;
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // check if we are grounded or not
    private void FixedUpdate()
    {
        // raycast for detecting the ground
        Ray ray = new Ray(transform.position + Vector3.up * -0.11f, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, jumpDetectorLength, groundLayer);

        // draw the jump detactor if on unity editor & drawJumpDetactor is true
#if UNITY_EDITOR
        if (drawJumpDetector)
        {
            Debug.DrawRay(ray.origin, ray.direction * jumpDetectorLength, Color.red);
        }
#endif
        // set verible
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void MovementWithPhysics()
    {
        // use physics for movement and cheack if we need to move horizontally or vertically
        if (horizontalMovementNeeded )
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rb2d.linearVelocity = new Vector2(horizontalInput * playerSpeed, rb2d.linearVelocity.y);
        }
        if (verticalMovementNeeded)
        {
            rb2d.gravityScale = 0;

            verticalInput = Input.GetAxis("Vertical");
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, verticalInput * playerSpeed);
        }
    }

    void MovementWithoutPhysics()
    {
        // use transform for movement and cheack if we need to move horizontally or vertically

        if (horizontalMovementNeeded)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * horizontalInput * playerSpeed * Time.deltaTime);
        }
        if (verticalMovementNeeded)
        {
            rb2d.gravityScale = 0;

            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * verticalInput * playerSpeed * Time.deltaTime);
        }
    }

}
