using UnityEngine;

public class AssaultRifle : Gun
{
    public float range = 100f; // Port�e du tir
    public Camera playerCamera; // Cam�ra du joueur

    protected override void HandleShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            // Si on touche un ennemi, applique des d�g�ts
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
            }
        }
    }
}