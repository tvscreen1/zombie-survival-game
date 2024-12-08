using UnityEngine;

public class MachineGun : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource audioSource;
    public AudioClip fireSound;

    private void Start()
    {
        weaponName = "Machine Gun";
        maxAmmo = 30;
        ammoCount = maxAmmo;
        fireRate = 0.1f;
        isAutomatic = true;

        if (audioSource == null)
            Debug.LogWarning($"{weaponName}: AudioSource not assigned!");
        if (fireSound == null)
            Debug.LogWarning($"{weaponName}: Fire sound not assigned!");
        if (bulletPrefab == null)
            Debug.LogWarning($"{weaponName}: Bullet prefab not assigned!");
        if (firePoint == null)
            Debug.LogWarning($"{weaponName}: FirePoint not assigned!");
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            ammoCount--;

            // Play the fire sound
            if (audioSource != null && fireSound != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(fireSound);
                audioSource.pitch = 1f; 
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