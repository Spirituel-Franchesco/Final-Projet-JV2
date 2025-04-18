using UnityEngine;

public class Pistol : Gun
{
    private void Awake()
    {
        gunName = "Pistol";
        damage = 10;
        fireRate = 0.5f;
        maxAmmo = 10;
        ammo = maxAmmo;
    }

    public override void Shoot(Transform shootOrigin)
    {
        if (!CanShoot()) return;

        UseAmmo();
        Debug.Log("Pistol tiré !");
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
