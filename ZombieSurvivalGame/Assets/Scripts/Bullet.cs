using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Zombie"))
        {
            ZombieController Zombie = hitInfo.GetComponent<ZombieController>();
            if (Zombie != null)
            {
                Zombie.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
