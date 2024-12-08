using UnityEngine;

public class Chainsaw : WeaponBase
{
    [Header("Chainsaw Settings")]
    public Collider2D attackCollider;   // The trigger collider representing the blade area
    public AudioSource audioSource;     // Looping sound of the chainsaw
    public AudioClip chainsawSound;
    public int damagePerFrame = 7995;      // Damage applied each frame to enemies inside the blade

    private bool isActive = false;

    private void Start()
    {
        weaponName = "Chainsaw";
        maxAmmo = 100;    // Represents fuel.
        ammoCount = maxAmmo;
        fireRate = 0f; 
        isAutomatic = true; // Holding Fire1 keeps it active

        // Disable the attack collider initially
        if (attackCollider != null)
            attackCollider.enabled = false;
        
        // Setup audio
        if (audioSource != null && chainsawSound != null)
        {
            audioSource.loop = true;
            audioSource.clip = chainsawSound;
        }
    }

    public override void Fire()
    {
        if (!isActive)
        {
            // Activate chainsaw
            if (attackCollider != null)
                attackCollider.enabled = true;

            if (audioSource != null && !audioSource.isPlaying && chainsawSound != null)
                audioSource.Play();

            isActive = true;
        }
    }

    public void StopFire()
    {
        // Deactivate chainsaw
        if (attackCollider != null)
            attackCollider.enabled = false;

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        isActive = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Damage enemies continuously while chainsaw is active
        if (isActive && other.CompareTag("Zombie"))
        {
            ZombieController zombie = other.GetComponent<ZombieController>();
            if (zombie != null)
            {
                zombie.TakeDamage(damagePerFrame * 8);
            }
        }
    }

    public override void Reload()
    {
        // Optional: Refuel
        ammoCount = maxAmmo;
        Debug.Log($"{weaponName} refueled.");
    }
}