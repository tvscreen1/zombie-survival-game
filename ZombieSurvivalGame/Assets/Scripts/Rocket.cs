using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 50;
    public float explosionRadius = 3f;
    public GameObject explosionEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Move to the right relative to the rocket’s own orientation
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Explode and deal area damage
        Explode();
    }

    void Explode()
    {
        // Instantiate explosion effect if assigned
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Damage all enemies in explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Zombie"))
            {
                ZombieController zombie = nearbyObject.GetComponent<ZombieController>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
            }
        }

        // Destroy the rocket after explosion
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}