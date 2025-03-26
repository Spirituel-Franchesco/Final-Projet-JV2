using UnityEngine;

public class Shotgun : Gun
{
    public float spreadAngle = 30f; // Angle de dispersion
    public int pellets = 8; // Nombre de projectiles
    public float range = 50f; // Portée réduite
    public Camera playerCamera;

    protected override void HandleShoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = playerCamera.transform.forward;
            direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * direction;

            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, direction, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    //enemy.TakeDamage(damage / pellets); // Dégâts divisés entre les projectiles
                }
            }
        }
    }
}