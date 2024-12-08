using UnityEngine;

public class RocketLauncher : WeaponBase
{
    public GameObject rocketPrefab;
    public Transform firePoint;
    public AudioSource audioSource;
    public AudioClip rocketFireSound;

    private void Start()
    {
        weaponName = "Rocket Launcher";
        maxAmmo = 5;
        ammoCount = maxAmmo;
        fireRate = 2f;   // Slower fire rate so player can’t spam rockets quickly
        isAutomatic = false;

        // Optional checks:
        if (audioSource == null)
            Debug.LogWarning($"{weaponName}: No AudioSource assigned!");
        if (rocketFireSound == null)
            Debug.LogWarning($"{weaponName}: No rocketFireSound assigned!");
        if (rocketPrefab == null)
            Debug.LogWarning($"{weaponName}: No rocketPrefab assigned!");
        if (firePoint == null)
            Debug.LogWarning($"{weaponName}: No firePoint assigned!");
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            // Instantiate the rocket at the firePoint
            Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

            ammoCount--;

            // Play rocket firing sound
            if (audioSource != null && rocketFireSound != null)
            {
                audioSource.PlayOneShot(rocketFireSound);
            }

            canFire = false;
            Invoke(nameof(ResetFire), fireRate);
        }
        else if (ammoCount <= 0)
        {
            Debug.Log("Out of rockets! Reload or pick up ammo.");
        }
    }

    private void ResetFire()
    {
        canFire = true;
    }
}