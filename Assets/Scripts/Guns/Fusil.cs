using UnityEngine;

public class Fusil : Gun
{
    //[SerializeField] private GameObject rocketPrefab;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private AudioSource shootSound;


    public float damage = 40f; // Damage value for the bullets
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

        //GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        //rocket.GetComponent<Rigidbody>().AddForce(firePoint.forward * 30f, ForceMode.Impulse);

        //if (shootSound != null)
        //    shootSound.Play();

        //// shootSound?.Play();
        ////PlayShootAnimation();
        //ammo--;
        //canShoot = false;
        //Invoke(nameof(ResetShoot), shootCooldown);



        muzzleFlash.Play(); // Play the muzzle flash effect

        currentAmmo--; // Decrease ammo count

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
