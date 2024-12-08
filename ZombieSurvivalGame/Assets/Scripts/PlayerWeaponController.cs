using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    // Assign these prefabs in the Inspector
    public GameObject handgunPrefab;
    public GameObject machineGunPrefab;
    public GameObject rocketLauncherPrefab;
    public GameObject chainsawPrefab;

    private WeaponBase currentWeapon;

    // Reference to the weapon mount point
    public Transform weaponMount;

    void Start()
    {
        // Start without a weapon
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
            // Check if the weapon is automatic or semi-automatic
            if (currentWeapon.isAutomatic)
            {
                // For automatic weapons (like Machine Gun or Chainsaw),
                // holding down Fire1 continuously calls Fire()
                if (Input.GetButton("Fire1"))
                {
                    currentWeapon.Fire();
                }
                
                // If the weapon is a Chainsaw and Fire1 is released, stop the chainsaw
                if (currentWeapon is Chainsaw && Input.GetButtonUp("Fire1"))
                {
                    ((Chainsaw)currentWeapon).StopFire();
                }
            }
            else
            {
                // For semi-automatic weapons (like Handgun and Rocket Launcher),
                // Fire only once per Fire1 press
                if (Input.GetButtonDown("Fire1"))
                {
                    currentWeapon.Fire();
                }
            }

            // Reload weapon if R is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentWeapon.Reload();
            }
        }
    }

    public void PickupWeapon(WeaponPickup.WeaponType weaponType)
    {
        // Destroy the currently equipped weapon if any
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject weaponPrefab = null;

        // Decide which weapon prefab to equip based on the weaponType
        switch (weaponType)
        {
            case WeaponPickup.WeaponType.Handgun:
                weaponPrefab = handgunPrefab;
                break;
            case WeaponPickup.WeaponType.MachineGun:
                weaponPrefab = machineGunPrefab;
                break;
            case WeaponPickup.WeaponType.RocketLauncher:
                weaponPrefab = rocketLauncherPrefab; // Here we handle the Rocket Launcher
                break;
            case WeaponPickup.WeaponType.Chainsaw:
                weaponPrefab = chainsawPrefab;
                break;
        }

        if (weaponPrefab != null)
        {
            // Instantiate the chosen weapon prefab
            GameObject weaponInstance = Instantiate(weaponPrefab);

            // Parent the weapon to the weaponMount, which should be at the player's hand
            weaponInstance.transform.SetParent(weaponMount);

            // Reset local transform so the weapon’s handle lines up with weaponMount (0,0)
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
            weaponInstance.transform.localScale = Vector3.one;

            // Set currentWeapon to the newly equipped weapon
            currentWeapon = weaponInstance.GetComponent<WeaponBase>();
            currentWeapon.ammoCount = currentWeapon.maxAmmo;
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