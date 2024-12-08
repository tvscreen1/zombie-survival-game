using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement speeds
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float climbSpeed = 3f;

    // Components and states
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isClimbing = false;
    private bool facingRight = true; // Tracks facing direction

    // Input variables
    private float moveInput;
    private float climbInput;
    private bool jump;

    // Ground check variables
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    // Reference to Weapon Mount
    private Transform weaponMount;
    private PlayerWeaponController weaponController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get reference to WeaponMount
        weaponController = GetComponent<PlayerWeaponController>();
        if (weaponController != null)
        {
            weaponMount = weaponController.weaponMount;
        }
        else
        {
            Debug.LogWarning("PlayerWeaponController not found on player.");
        }
    }

    void Update()
    {
        // Collect horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isClimbing)
        {
            // Collect vertical input for climbing
            climbInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            climbInput = 0f;

            // Check for jump input
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jump = true;
            }
        }

        // Flip sprite based on movement direction
        FlipSprite();
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isClimbing)
        {
            // Vertical climbing movement
            rb.velocity = new Vector2(0f, climbInput * climbSpeed);
            rb.gravityScale = 0f; // Disable gravity while climbing
        }
        else
        {
            rb.gravityScale = 1f; // Ensure gravity is enabled when not climbing

            // Horizontal movement
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // Jumping
            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jump = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Climable"))
        {
            isClimbing = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climable"))
        {
            isClimbing = false;
            rb.gravityScale = 1f;
        }
    }

    // Method to flip the sprite based on movement direction
    void FlipSprite()
    {
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        // Flip the player's local scale on the X axis
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // Prevent flipping for the KillTrackerCanvas
        Transform canvasParent = transform.Find("KillTrackerCanvas"); // Adjust name if different
        if (canvasParent != null)
        {
            Vector3 canvasScale = canvasParent.localScale;
            canvasScale.x = Mathf.Abs(canvasScale.x); // Ensure scale remains positive
            canvasParent.localScale = canvasScale;
        }
    }
}