using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAGun : Gun
{
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private float bulletSpeed = 10f;
    //[SerializeField] private AudioSource shootSound;

    //public override void Shoot()
    //{
    //    if (bulletPrefab && firePoint)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //        Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    //            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
    //        }
    //        Projectile bulletScript = bullet.GetComponent<Projectile>();
    //        if (bulletScript != null)
    //        {
    //            bulletScript.damage = damage;
    //        }
    //    }

    //    //shootSound?.Play();
    //}

    public float damage = 5f; // Damage value for the bullets
    public float range = 100f; // Current ammo count
    public float impactForce = 30f; // Force applied to the object hit

    //public int maxxAmmo = 10; // Current ammo count
    //public int currentAmmo ; // Maximum ammo count
    //public float reloadTime = 1f; // Time taken to reload

    public Camera fpsCam; // Reference to the camera
    public ParticleSystem muzzleFlash; // 
    public GameObject impactEffect; // Reference to the impact effect prefab

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource shootSound;

  

    private void Update()
    {
        if (currentAmmo <= 0) 
        {
            Reload();
            return; // Reload if ammo is empty
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    

    public override void Shoot()
    {
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
