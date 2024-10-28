using UnityEngine;

public class Handgun : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        weaponName = "Handgun";
        maxAmmo = 12;
        ammoCount = maxAmmo;
        fireRate = 0.5f;
        isAutomatic = false;
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Set bullet's local scale to match weapon's local scale
            bullet.transform.localScale = transform.localScale;

            ammoCount--;
            Debug.Log("Handgun fired.");

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