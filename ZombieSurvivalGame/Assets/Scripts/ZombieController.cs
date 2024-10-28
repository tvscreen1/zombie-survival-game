using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float climbSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isClimbing = false;

    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;

    // Attack variables
    public int damageAmount = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    // Health variable
    public int health = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isClimbing)
        {
            rb.velocity = new Vector2(0f, climbSpeed);
        }
        else
        {
            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

                // Flip the zombie to face the player
                if (direction.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && facingRight)
                {
                    Flip();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isClimbing)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
            isGrounded = hit.collider != null;

            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(rb.velocity.x), 1f, LayerMask.GetMask("Obstacle"));
            if (obstacleHit.collider != null && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Zombie Collision Enter with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer(collision.gameObject);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer(collision.gameObject);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void AttackPlayer(GameObject playerObject)
    {
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Zombie attacked the player!");
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

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // New methods

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        // Destroy the zombie GameObject
        Destroy(gameObject);
    }
}