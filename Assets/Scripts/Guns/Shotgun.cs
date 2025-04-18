using UnityEngine;

public class Shotgun : Gun
{
    private void Awake()
    {
        gunName = "Shotgun";
        damage = 50;
        fireRate = 1.0f;
        maxAmmo = 5;
        ammo = maxAmmo;
    }

    public override void Shoot(Transform shootOrigin)
    {
        if (!CanShoot()) return;

        UseAmmo();
        Debug.Log("Shotgun tiré !");
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
