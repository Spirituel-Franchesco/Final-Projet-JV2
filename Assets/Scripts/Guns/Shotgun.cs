using UnityEngine;

public class ShotGun : Gun
{
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private int pelletCount = 6;
    //[SerializeField] private float spreadAngle = 10f;
    //[SerializeField] private float bulletSpeed = 10f;
    //[SerializeField] private AudioSource shootSound;

    //public override void Shoot()
    //{
    //    for (int i = 0; i < pelletCount; i++)
    //    {
    //        Quaternion spread = Quaternion.Euler(
    //            Random.Range(-spreadAngle, spreadAngle),
    //            Random.Range(-spreadAngle, spreadAngle),
    //            0
    //        );

    //        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spread);
    //        Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    //            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    //        }

    //        Projectile bulletScript = bullet.GetComponent<Projectile>();
    //        if (bulletScript != null)
    //        {
    //            bulletScript.damage = damage;
    //        }
    //    }

    //    shootSound?.Play();
    //}

    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private int pelletCount = 6;
    //[SerializeField] private float spreadAngle = 10f;
    //[SerializeField] private AudioSource shootSound;


    public float damage = 100f; // Damage value for the bullets
    public float range = 100f; // Current ammo count
    public float impactForce = 30f; // Force applied to the object hit

    public Camera fpsCam; // Reference to the camera
    public ParticleSystem muzzleFlash; // 
    public GameObject impactEffect; // Reference to the impact effect prefab

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource shootSound;



    public override void Shoot()
    {
        //if (!canShoot || ammo <= 0) return;

        //for (int i = 0; i < pelletCount; i++)
        //{
        //    Quaternion spread = Quaternion.Euler(
        //        Random.Range(-spreadAngle, spreadAngle),
        //        Random.Range(-spreadAngle, spreadAngle),
        //        0
        //    );

        //    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spread);
        //    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 25f, ForceMode.Impulse);
        //}

        //if (shootSound != null)
        //    shootSound.Play();

        ////shootSound?.Play();
        ////PlayShootAnimation();
        //ammo--;

        //canShoot = false;
        //Invoke(nameof(ResetShoot), shootCooldown);


        muzzleFlash.Play(); // Play the muzzle flash effect


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            // Apply damage to the object hit
            AimPoint aimpoint = hit.transform.GetComponent<AimPoint>();
            if (aimpoint != null)
            {
                aimpoint.TakeDamage(damage);
            }

            // Check if the object hit has a Rigidbody and apply force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate the impact effect
            Destroy(impactGO, 2f); // Destroy the impact effect after 2 seconds
        }
    }

    private void ResetShoot() => canShoot = true;
}
