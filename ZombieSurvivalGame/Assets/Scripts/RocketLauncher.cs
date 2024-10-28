using UnityEngine;

public class RocketLauncher : WeaponBase
{
    public GameObject rocketPrefab;
    public Transform firePoint;

    private void Start()
    {
        weaponName = "Rocket Launcher";
        maxAmmo = 5;
        ammoCount = maxAmmo;
        fireRate = 1.5f;
        isAutomatic = false;
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
            ammoCount--;
            Debug.Log("Rocket Launcher fired.");

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