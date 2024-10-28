using UnityEngine;

public class MachineGun : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        weaponName = "Machine Gun";
        maxAmmo = 30;
        ammoCount = maxAmmo;
        fireRate = 0.1f;
        isAutomatic = true;
    }

    public override void Fire()
    {
        if (ammoCount > 0 && canFire)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ammoCount--;
            Debug.Log("Machine Gun fired.");

            canFire = false;
            Invoke(nameof(ResetFire), fireRate);
        }
    }

    private void ResetFire()
    {
        canFire = true;
    }
}