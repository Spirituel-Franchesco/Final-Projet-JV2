using UnityEngine;

public class PAGun : Gun
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Camera _fpsCam; // Reference to the camera
    [SerializeField] private ParticleSystem _muzzleFlash; // 
    [SerializeField] private GameObject _impactEffect; // Reference to the impact effect prefab
    [SerializeField] private float _range = 100f; // Current ammo count
    [SerializeField] private float _impactForce = 30f; // Force applied to the object hit
    [SerializeField] private int _damage ; // Damage value for the bullets

    public override void Shoot()
    {
        if (!CanShoot())
        {
            AudioSource.PlayClipAtPoint(_emptyClip, transform.position);
            Debug.Log("Click! No ammo.");
            return;
        }

        _shootSound.Play();
        _muzzleFlash.Play();
        _currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out hit, _range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            AimPoint aimpoint = hit.transform.GetComponent<AimPoint>();
            if (aimpoint != null)
            {
                aimpoint.TakeDamage(_damage);
            }

            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            AudioSource.PlayClipAtPoint(_impactClip, hit.point);
        }
    }
}
