using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;
    public int ammoCount;
    public int maxAmmo;
    public float fireRate;
    public bool isAutomatic;

    protected bool canFire = true;

    public abstract void Fire();

    public virtual void Reload()
    {
        ammoCount = maxAmmo;
        Debug.Log($"{weaponName} reloaded.");
    }
}