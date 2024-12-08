using UnityEngine;

public class Handgun : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Add these two fields
    public AudioSource audioSource;  // Assign in the Inspector
    public AudioClip fireSound;      // Assign in the Inspector

    private void Start()
    {
        weaponName = "Handgun";
        maxAmmo = 44;
        ammoCount = maxAmmo;
        fireRate = 0.5f;
        isAutomatic = false;

        // Warn if audioSource or fireSound is not assigned
        if (audioSource == null)
            Debug.LogWarning($"{weaponName}: AudioSource not assigned!");
        if (fireSound == null)
            Debug.LogWarning($"{weaponName}: Fire sound not assigned!");
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.localScale = transform.localScale;

            ammoCount--;
            Debug.Log("Handgun fired.");

            // Play the fire sound
            if (audioSource != null && fireSound != null)
            {
                //  Slight pitch variation for more realism
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(fireSound);
                audioSource.pitch = 1f; // reset after playing
            }

            // Implement fire rate
            canFire = false;
            Invoke(nameof(ResetFire), fireRate);
        }
    }

    private void ResetFire()
    {
        canFire = true;
    }
}