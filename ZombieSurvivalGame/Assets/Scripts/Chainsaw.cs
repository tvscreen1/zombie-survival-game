using UnityEngine;

public class Chainsaw : WeaponBase
{
    public Collider2D attackCollider;

    private void Start()
    {
        weaponName = "Chainsaw";
        maxAmmo = 100; // Represents fuel or durability
        ammoCount = maxAmmo;
        fireRate = 0f; // Immediate action
        isAutomatic = true;

        if (attackCollider == null)
        {
            Debug.LogWarning("Attack Collider not assigned on Chainsaw.");
        }

        // Ensure the attack collider is disabled initially
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    public override void Fire()
    {
        if (ammoCount > 0)
        {
            // Enable attack collider to damage enemies
            if (attackCollider != null)
            {
                attackCollider.enabled = true;
                Debug.Log("Chainsaw activated.");
            }

            // Decrease ammo over time
            ammoCount--;
        }
        else
        {
            Debug.Log("Chainsaw is out of fuel!");
            StopFire();
        }
    }

    public void StopFire()
    {
        // Disable attack collider
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
            Debug.Log("Chainsaw deactivated.");
        }
    }
}