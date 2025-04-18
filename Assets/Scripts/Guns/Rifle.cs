using UnityEngine;

public class Rifle : Gun
{
    private void Awake()
    {
        gunName = "Rifle";
        damage = 30;
        fireRate = 0.2f;
        maxAmmo = 30;
        ammo = maxAmmo;
    }

    public override void Shoot(Transform shootOrigin)
    {
        if (!CanShoot()) return;

        UseAmmo();
        Debug.Log("Rifle tiré !");
        RaycastHit hit;
        if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out hit, 100f))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
            }
        }
    }
}
