using UnityEngine;

public class PAGun : Gun
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioSource _shootSound;

    public Camera _fpsCam; // Reference to the camera
    public ParticleSystem _muzzleFlash; // 
    public GameObject _impactEffect; // Reference to the impact effect prefab

    public int _damage ; // Damage value for the bullets
    public float _range = 100f; // Current ammo count
    public float _impactForce = 30f; // Force applied to the object hit

    public override void Shoot()
    {
        if (_currentAmmo <= 0) { _canShoot = false; } // Check if the gun can shoot

        else { 

        RaycastHit hit;
        if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out hit, _range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            // Apply _damage to the object hit
            AimPoint aimpoint = hit.transform.GetComponent<AimPoint>();
            if (aimpoint != null)
            {
                aimpoint.TakeDamage(_damage);
            }

            // Check if the object hit has a Rigidbody and apply force
            if (hit.rigidbody != null)
            {
                //hit.rigidbody.AddForce(-hit.normal * _impactForce);
            }

            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate the impact effect
            Destroy(impactGO, 2f); // Destroy the impact effect after 2 seconds
        }
        

        };

        _muzzleFlash.Play(); // Play the muzzle flash effect

        _currentAmmo--; // Decrease ammo count
    }

    private void ResetShoot() => _canShoot = true;
}
