using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum WeaponType { Handgun, MachineGun, RocketLauncher, Chainsaw }
    public WeaponType weaponType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponController playerWeaponController = other.GetComponent<PlayerWeaponController>();
            if (playerWeaponController != null)
            {
                playerWeaponController.PickupWeapon(weaponType);
                Debug.Log($"Player picked up: {weaponType}");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("PlayerWeaponController not found on player.");
            }
        }
    }
}