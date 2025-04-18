using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public int damage;
    public int ammo;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;

    protected float nextFireTime;

    public abstract void Shoot(Transform shootOrigin);

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log($"{gunName} rechargé !");
    }

    public bool CanShoot()
    {
        return Time.time >= nextFireTime && ammo > 0;
    }

    protected void UseAmmo()
    {
        ammo--;
        nextFireTime = Time.time + fireRate;
    }
}
