using UnityEngine;

public abstract class TestGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public int ammo = 10;
    public int maxAmmo = 10;
    public float shootCooldown = 0.5f;
    public int price = 0;
    protected bool canShoot = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot && ammo > 0)
            {
                Shoot();
                ammo--;
                canShoot = false;
                Invoke(nameof(ResetShoot), shootCooldown);
            }
        }
    }

    protected void ResetShoot() => canShoot = true;

    public abstract void Shoot();

    public virtual void Reload()
    {
        ammo = maxAmmo;
    }
}
