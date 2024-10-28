using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    // Weapon prefabs
    public GameObject handgunPrefab;
    public GameObject machineGunPrefab;
    public GameObject rocketLauncherPrefab;
    public GameObject chainsawPrefab;

    private WeaponBase currentWeapon;

    // Reference to the weapon mount point
    public Transform weaponMount;

    void Start()
    {
        // Optionally, start without a weapon or with a default weapon
        currentWeapon = null;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (currentWeapon != null)
        {
            // Fire weapon
            if (currentWeapon.isAutomatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    currentWeapon.Fire();
                }
                if (currentWeapon is Chainsaw && Input.GetButtonUp("Fire1"))
                {
                    ((Chainsaw)currentWeapon).StopFire();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    currentWeapon.Fire();
                }
            }

            // Reload weapon
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentWeapon.Reload();
            }
        }
    }

    public void PickupWeapon(WeaponPickup.WeaponType weaponType)
    {
        // Remove current weapon if any
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        // Instantiate new weapon
        GameObject weaponPrefab = null;
        switch (weaponType)
        {
            case WeaponPickup.WeaponType.Handgun:
                weaponPrefab = handgunPrefab;
                break;
            case WeaponPickup.WeaponType.MachineGun:
                weaponPrefab = machineGunPrefab;
                break;
            case WeaponPickup.WeaponType.RocketLauncher:
                weaponPrefab = rocketLauncherPrefab;
                break;
            case WeaponPickup.WeaponType.Chainsaw:
                weaponPrefab = chainsawPrefab;
                break;
        }

        if (weaponPrefab != null)
        {
            // Instantiate the weapon
            GameObject weaponInstance = Instantiate(weaponPrefab);

            // Parent the weapon to the WeaponMount
            weaponInstance.transform.SetParent(weaponMount);

            // Reset local position and rotation
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;

            // Adjust local scale if necessary
            weaponInstance.transform.localScale = Vector3.one;

            // Assign currentWeapon
            currentWeapon = weaponInstance.GetComponent<WeaponBase>();
            currentWeapon.ammoCount = currentWeapon.maxAmmo; // Start with full ammo
            Debug.Log($"Picked up {currentWeapon.weaponName}");
        }
    }

    public void AddAmmo(int amount)
    {
        if (currentWeapon != null)
        {
            currentWeapon.ammoCount += amount;
            if (currentWeapon.ammoCount > currentWeapon.maxAmmo)
            {
                currentWeapon.ammoCount = currentWeapon.maxAmmo;
            }
            Debug.Log($"Ammo replenished. Current ammo: {currentWeapon.ammoCount}");
        }
    }
}